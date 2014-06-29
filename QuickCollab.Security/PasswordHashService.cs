using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace QuickCollab.Security
{
    public class PasswordHashService
    {
        private Random _random;

        public PasswordHashService()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        public string SaltedPassword(string password, string salt)
        {
            SHA256 sha = new SHA256Managed();

            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password + salt));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }

        public string GetNewSalt()
        {
            StringBuilder builder = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
            {
                builder.Append((char)_random.Next(33, 126));
            }

            return builder.ToString();
        }
    }
}
