using RegTest.DataBase;
using RegTest.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegTest.Core
{
    public class Regestrator (ILogger logger, IDataBase dbContext)
    {
        private readonly ILogger logger = logger;
        private bool RegisterValidate(string? login, string? password, string? confirmPassword)
        {
            // Null check
            if (string.IsNullOrWhiteSpace(login))
            {
                logger.Log("Login is empty", true); return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                logger.Log("Password is empty", true); return false;
            }
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                logger.Log("Password confirm is empty", true); return false;
            }

            // Format check (and classification)
            bool isPhone = Regex.IsMatch(login, @"^\+\d-\d{3}-\d{3}-\d{4}$");
            bool isEmail = Regex.IsMatch(login, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            bool isSimpleText = Regex.IsMatch(login, @"^[a-zA-Z0-9_]{5,}$");

            if (!isPhone && !isEmail && !isSimpleText)
            {
                if (Regex.IsMatch(login, @"^[a-zA-Z0-9_]+$") && login.Length < 5)
                    logger.Log($"Login '{login}' must be 5 chars min", true);
                logger.Log($"Invalid login '{login}' format (only: +x-xxx-xxx-xxxx, email, latin 5+ chars)", true);
                return false;
            }

            // Login already existing
            if (dbContext.GetUsers().Select(u => u.Login).Contains(login.ToLower()))
            {
                logger.Log($"Login '{login}' already exists", true); return false;
            }

            // Confirm passwrod Matching
            if (password != confirmPassword)
            {
                logger.Log("Password confirm doesn't match password", true); return false;
            }

            // Password Length
            if (password.Length < 7)
            {
                logger.Log("Password too short. Min 7 chars", true); return false;
            }

            // Password (only cyryllic? (nonsense! who does that???), digits, special characters)
            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                logger.Log("No lowercase latin char in password", true); return false;
            }
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                logger.Log("No uppercase latin char in password", true); return false;
            }
            if (!Regex.IsMatch(password, @"\d"))
            {
                logger.Log("No digits in password", true); return false;
            }
            if (!Regex.IsMatch(password, @"[^\w\s]|_"))
            {
                logger.Log("No special character in password", true); return false;
            }

            // Everything okay
            logger.Log("Register Validated!");
            return true;
        }

        private void AddToDB(string login, string password)
        {
            string maskedPassword = PasswordMasker.MaskPassword(password);
            dbContext.AddUser(new Models.User(login.ToLower(), maskedPassword));
            logger.Log($"User registered! login '{login}', password hash '{maskedPassword}'");
        }

        public bool Register(string? login, string? password, string? confirmPassword)
        {
            if (!RegisterValidate(login,password,confirmPassword)) return false;
            AddToDB(login!, password!);
            return true;
        }
    }
}
