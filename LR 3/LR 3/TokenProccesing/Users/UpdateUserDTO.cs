using System.ComponentModel.DataAnnotations;

namespace LR_3.TokenProccesing.Users
{
    public class UpdateUserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
