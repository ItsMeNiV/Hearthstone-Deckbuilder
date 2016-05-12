using System;
using System.Security.Cryptography;

namespace Hearthstone_Deckbuilder.Database.NSCryptoHandler
{
    public class CryptoHandler
    {
        public static string HashPassword(string plainTextPassword, byte[] passwordSalt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(plainTextPassword, passwordSalt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] passwordHash = new byte[36];
            Array.Copy(passwordSalt, 0, passwordHash, 0, 16);
            Array.Copy(hash, 0, passwordHash, 16, 20);
            return Convert.ToBase64String(passwordHash);
        }

        public static byte[] GeneratePasswordSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }
    }
}