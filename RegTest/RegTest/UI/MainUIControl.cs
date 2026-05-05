using RegTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegTest.UI
{
    internal class MainUIControl
    {
        public void Start(Regestrator reg) 
        {
            Console.WriteLine("--- Sign Up as a new User! ---");
            Console.Write("Login / Email / Phone): ");
            string? login = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();
            Console.Write("Confirm password: ");
            string? confirmPassword = Console.ReadLine();

            reg.Register(login, password, confirmPassword);
        }
    }
}
