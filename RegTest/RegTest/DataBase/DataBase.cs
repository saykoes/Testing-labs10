using RegTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegTest.DataBase
{
    internal class DataBase : IDataBase
    {
        private readonly List<User> Users = new List<User> 
        { 
            new User("admin","masked"), 
            new User("moderator", "masked"), 
            new User("user123", "ereremaskedr"), 
            new User("root", "emaskedrerer") 
        };

        public List<User> GetUsers() => Users;
        public void AddUser(User user) { Users.Add(user); }
    }
}
