using System.Collections.Generic;
using System.Linq;
using Shop.Models;

namespace Shop.Repositories
{
    public static class UserRepository
    {

        public static User Get(string username , string password) {

            username = username.ToLower();
            password = password.ToLower();

            var users = new List<User>(){
                new User() { Id = 1, Username = "gilberto", Password = "123", Role="manager" },
                new User() { Id = 2, Username = "joselia", Password = "321", Role ="employee" }

            };

            return users.Where(u => u.Username.ToLower() == username 
                                 && u.Password.ToLower() == password)
                        .FirstOrDefault();
        }
    }
}