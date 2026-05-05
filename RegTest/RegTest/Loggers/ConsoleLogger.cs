using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegTest.Log
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, bool isError = false)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string level = isError ? "ERROR" : "Log";

            string fullMessage = $"[{timestamp}][{level}] {message}";

            if (isError) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(fullMessage);
            Console.ResetColor();
        }
    }
}
