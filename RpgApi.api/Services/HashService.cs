using Konscious.Security.Cryptography;
using System.Text;

namespace RpgApi.api.Services
{
    public class HashService
    {
        public byte[] HashPassword(string password, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            using var argon2 = new Argon2id(passwordBytes);

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.MemorySize = 1024 * 64;
            argon2.Iterations = 4;
            return argon2.GetBytes(32);
        }

        public bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            var passwordHashBytes = hash;
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                MemorySize = 1024 * 64,
                Iterations = 4
            };

            var newHash = argon2.GetBytes(32);
            return newHash.SequenceEqual(passwordHashBytes);
        }

        public byte[] CreateSalt()
        {
            var buffer = new byte[32];
            var rng = new Random();
            rng.NextBytes(buffer);
            return buffer;
        }
    }
}


