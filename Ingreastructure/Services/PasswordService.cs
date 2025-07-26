using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string plainTextPassword)
        {
            return _hasher.HashPassword(null, plainTextPassword);
        }

        public bool VerifyPassword(string hashedPassword, string plainTextPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, plainTextPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
