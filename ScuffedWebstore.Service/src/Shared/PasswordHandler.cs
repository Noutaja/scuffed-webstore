using System.Security.Cryptography;
using System.Text;

namespace ScuffedWebstore.Service.src.Shared;
public class PasswordHandler
{
    public static (byte[] salt, string password) HashPassword(string plainPassword)
    {
        HMACSHA256 hmac = new HMACSHA256();
        byte[] salt = hmac.Key;
        string hashedPassword = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword)));

        return (salt, hashedPassword);
    }

    public static bool VerifyPassword(string plainPassword, string hashedPassword, byte[] salt)
    {
        HMACSHA256 hmac = new HMACSHA256(salt);
        Console.WriteLine(BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword))));
        Console.WriteLine(hashedPassword);
        Console.WriteLine(BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword))) == hashedPassword);
        return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword))) == hashedPassword;
    }
}