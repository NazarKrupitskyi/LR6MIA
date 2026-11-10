using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static LR_3.TokenProccesing.Tokens_Auth.RoleFlags;

namespace LR_3.TokenProccesing.Users
{
    public class UserDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("refreshToken")]
        public string? RefreshToken { get; set; }

        [BsonElement("refreshTokenExpiryTime")]
        public DateTime RefreshTokenExpiryTime { get; set; }
        [BsonElement("role")]
        public Roles Role { get; set; } = Roles.User;

    }
}
