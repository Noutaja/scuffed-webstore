using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScuffedWebstore.Service.src.Shared
{
    public class PasswordHandler
    {
        public static (byte[] salt, byte[] password) HashPassword(string plainPassword)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] salt = hmac.Key;
            byte[] hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));

            return (salt, hashedPassword);
        }

        public static bool VerifyPassword(string plainPassword, byte[] hashedPassword, byte[] salt)
        {
            HMACSHA256 hmac = new HMACSHA256(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword)) == hashedPassword;
        }
    }
}