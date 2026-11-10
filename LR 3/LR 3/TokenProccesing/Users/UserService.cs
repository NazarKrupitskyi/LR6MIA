using AutoMapper;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.DTO.Event;
using LR_3.Models;
using LR_3.TokenProccesing.Tokens_Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static LR_3.TokenProccesing.Tokens_Auth.RoleFlags;

namespace LR_3.TokenProccesing.Users
{
    public interface IUserService
    {
        Task<UserResponseDTO> CreateAsync(CreateUserDTO dto);
        Task<List<UserResponseDTO>> GetAsync();
        Task<UserResponseDTO?> GetByEmailAsync(string email);
        Task<UserResponseDTO?> GetAsync(string id);
        Task<UserResponseDTO?> UpdateAsync(string id, UpdateUserDTO dto);
        Task<LoginResponseDTO> LoginAsync(LoginUserDTO dto);
        Task<LoginResponseDTO> RefreshTokenAsync(string accessToken, string refreshToken);
        Task<bool> DeleteAsync(string id);
        Task<UserResponseDTO> SetRolesAsync(string id, Roles roles);
        Task<UserResponseDTO> AddRoleAsync(string id, Roles role);
        Task<UserResponseDTO> RemoveRoleAsync(string id, Roles role);
    }



    public class UserService : IUserService
    {
        private readonly IUserReposetory _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(IUserReposetory userRepository, IMapper mapper, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<List<UserResponseDTO>> GetAsync()
        {
            var users = await _userRepository.GetAsync();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO?> GetAsync(string id)
        {
            var user = await _userRepository.GetAsync(id);
            return user == null ? null : _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO?> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user == null ? null : _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> CreateAsync(CreateUserDTO dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Користувач з такою поштою вже існує.");

            var user = _mapper.Map<User>(dto);

            user.Password = _passwordHasher.HashPassword(dto.Password);

            await _userRepository.CreateAsync(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO?> UpdateAsync(string id, UpdateUserDTO dto)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null) return null;

            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }
        public async Task<LoginResponseDTO> LoginAsync(LoginUserDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Неправильна пошта або пароль.");

            if (!_passwordHasher.Verify(dto.Password, user.Password))
                throw new UnauthorizedAccessException("Неправильний пароль.");

            var token = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new LoginResponseDTO
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpiryTime = DateTime.UtcNow.AddMinutes(60),
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }
        public async Task<LoginResponseDTO> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                throw new UnauthorizedAccessException("Invalid token claims");

            var id = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id))
                throw new UnauthorizedAccessException("Invalid token claims");

            var user = await _userRepository.GetAsync(id);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            var newAccessToken = _jwtTokenGenerator.GenerateToken(user);
            var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new LoginResponseDTO
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                TokenExpiryTime = DateTime.UtcNow.AddMinutes(60),
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };


        }
        public async Task<UserResponseDTO> SetRolesAsync(string id, Roles roles)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null) throw new KeyNotFoundException("Користувача не знайдено");

            user.Role = roles;
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> AddRoleAsync(string id, Roles role)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null) throw new KeyNotFoundException("Користувача не знайдено");

            user.Role |= role; 
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> RemoveRoleAsync(string id, Roles role)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null) throw new KeyNotFoundException("Користувача не знайдено");

            user.Role &= ~role;
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
