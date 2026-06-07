using ComfortSpace.Models;
using Microsoft.AspNetCore.Identity;

namespace ComfortSpace.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(User user, string password)
            => _hasher.HashPassword(user, password);

        public bool Verify(User user, string hashedPassword, string password)
            => _hasher.VerifyHashedPassword(user, hashedPassword, password)
               == PasswordVerificationResult.Success;
    }
}
