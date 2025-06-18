using GuitarCommerceAPI.Models;

namespace GuitarCommerceAPI.Services
{
    public interface IIdentityService
    {
        string GenerateToken(User user);
    }
}
