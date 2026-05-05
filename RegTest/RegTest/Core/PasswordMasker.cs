using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RegTest.Core
{
    internal class PasswordMasker
    {
        // SHA256 hash (same output for input every time)
        public static string MaskPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "empty";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").Substring(0, 8) + "...";
            }
        }
    }
}
