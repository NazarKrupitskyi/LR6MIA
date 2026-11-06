using static LR_3.TokenProccesing.Tokens_Auth.RoleFlags;

namespace LR_3.TokenProccesing.Users
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public Roles Role { get; set; } = Roles.User;
    }
}
