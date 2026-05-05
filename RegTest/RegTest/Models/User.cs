using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegTest.Models
{
    public class User(string login, string password)
    {
        public Guid Uuid { get; init; } = Guid.NewGuid();
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
    }
}
