using GuitarCommerceAPI.Models;

namespace GuitarCommerceAPI.Services
{
    public interface IUserService
    {
        Task<bool> Register(string username, string password);
        Task<User?> Login(string username, string password);
        Task<User?> GetUserData(string userId);
    }
}
