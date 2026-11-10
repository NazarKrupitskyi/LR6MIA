using System.Security.Cryptography;
using System.Text;

namespace LR_3.TokenProccesing
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string password, string hash);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

    }
}
