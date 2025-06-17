namespace GuitarCommerceAPI.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public LoginResponse(int userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
