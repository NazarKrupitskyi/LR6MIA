using AutoMapper;
using LR_3.DBCommunication.Documents;
using MongoDB.Driver;

namespace LR_3.TokenProccesing.Users
{
    public interface IUserReposetory
    {
        Task CreateAsync(User user);
        Task<List<User>> GetAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetAsync(string id);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }


    public class UserReposetory : IUserReposetory
    {
        private readonly IMongoCollection<UserDocument> _collection;
        private readonly IMapper _mapper;
        public UserReposetory(IMongoDatabase db, IMapper mapper)
        {
            _collection = db.GetCollection<UserDocument>("users");
            _mapper = mapper;
        }

        public async Task<User?> GetAsync(string id)
        {
            var document = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return document is null ? null : _mapper.Map<User>(document);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var document = await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
            return document is null ? null : _mapper.Map<User>(document);
        }

        public async Task<List<User>> GetAsync()
        {
            var documents = await _collection.Find(x => true).ToListAsync();
            return _mapper.Map<List<User>>(documents);
        }

        public async Task CreateAsync(User user)
        {
            var document = _mapper.Map<UserDocument>(user);
            await _collection.InsertOneAsync(document);
        }

        public async Task DeleteAsync(string id)
            => await _collection.DeleteOneAsync(x => x.Id == id);

        public async Task UpdateAsync(User user)
        {
            var document = _mapper.Map<UserDocument>(user);
            await _collection.ReplaceOneAsync(x => x.Id == user.Id, document);
        }

        //private static User ToModel(UserDocument document) => new()
        //{
        //    Id = document.Id,
        //    Username = document.Username,
        //    Email = document.Email,
        //    Password = document.Password,
        //    RefreshToken = document.RefreshToken,
        //    RefreshTokenExpiryTime = document.RefreshTokenExpiryTime
        //};

        //private static UserDocument ToDocument(User user) => new()
        //{
        //    Id = user.Id,
        //    Username = user.Username,
        //    Email = user.Email,
        //    Password = user.Password,
        //    RefreshToken = user.RefreshToken,
        //    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
        //};
    }
}