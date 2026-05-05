using RegTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegTest.DataBase
{
    public interface IDataBase
    {
        public List<User> GetUsers();
        void AddUser(User user);
    }
}
