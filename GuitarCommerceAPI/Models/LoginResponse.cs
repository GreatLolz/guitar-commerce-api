namespace GuitarCommerceAPI.Models
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; }

        public LoginResponse(string userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
