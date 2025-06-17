using GuitarCommerceAPI.Data;
using GuitarCommerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GuitarCommerceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly AppDbContext _db;

        public UserService(AppDbContext db) {
            _passwordHasher = new PasswordHasher<User>();
            _db = db;
        }

        public async Task<User?> Register(string username, string password)
        {
            if (await _db.Users.AnyAsync(u => u.Name == username)) {
                return null; // User already exists
            }
            User user = new User {
                Name = username,
                PasswordHash = _passwordHasher.HashPassword(null, password)
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Login(string username, string password)
        {
            User? user = await _db.Users.SingleOrDefaultAsync(u => u.Name == username);
            if (user == null) {
                return null;
            }

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
