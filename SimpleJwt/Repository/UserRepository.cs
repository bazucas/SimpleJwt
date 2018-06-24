using SimpleJwt.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleJwt.Repository
{
    public class UserRepository
    {
        public List<User> TestUsers;

        public UserRepository()
        {
            TestUsers = new List<User>
            {
                new User {Username = "Test1", Password = "Pass1"},
                new User {Username = "Test2", Password = "Pass2"}
            };
        }

        public User GetUser(string username)
        {
            try
            {
                return TestUsers.First(user => user.Username.Equals(username));
            }
            catch
            {
                return null;
            }
        }
    }
}