namespace LR_3.TokenProccesing.Tokens_Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpiryTime { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
