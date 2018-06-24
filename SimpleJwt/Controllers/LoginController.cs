using SimpleJwt.Models;
using SimpleJwt.Repository;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleJwt.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(User user)
        {
            var u = new UserRepository().GetUser(user.Username);

            if (u == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "The user was not found.");

            var credentials = u.Password.Equals(user.Password);

            if (!credentials) return Request.CreateResponse(HttpStatusCode.Forbidden,
                "The username/password combination was wrong.");

            return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(user.Username));
        }

        [HttpGet]
        public HttpResponseMessage Validate(string token, string username)
        {
            var exists = new UserRepository().GetUser(username) != null;

            if (!exists) return Request.CreateResponse(HttpStatusCode.NotFound, "The user was not found.");

            var tokenUsername = TokenManager.ValidateToken(token);

            return Request.CreateResponse(username.Equals(tokenUsername) ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}