using BookManagementAPI.API.DTOs;
using BookManagementAPI.Core.Interfaces;
using BookManagementAPI.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _jwtExpirationMinutes;

        public AuthService(
            IUserRepository userRepository,
            string jwtSecret,
            string jwtIssuer,
            string jwtAudience,
            int jwtExpirationMinutes = 60)
        {
            _userRepository = userRepository;
            _jwtSecret = jwtSecret;
            _jwtIssuer = jwtIssuer;
            _jwtAudience = jwtAudience;
            _jwtExpirationMinutes = jwtExpirationMinutes;
        }

        public async Task<(string? Token, string? Username, string? Email)> Login(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return (null, null, null);
            }

            // Verify password (using simple comparison for now - in production use BCrypt or similar)
            if (user.Password != password)
            {
                return (null, null, null);
            }

            var token = GenerateJwtToken(user);
            return (token, user.Username, user.Email);
        }

        public async Task<(string? Token, string? Username, string? Email)> Register(string username, string password, string firstname, string lastname, string email)
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetUserByUsername(username);
            if (existingUser != null)
            {
                return (null, null, null);
            }

            // Check if email already exists
            var existingEmail = await _userRepository.GetUserByEmail(email);
            if (existingEmail != null)
            {
                return (null, null, null);
            }

            // Create new user (in production, hash the password using BCrypt or similar)
            var user = new User
            {
                Username = username,
                Password = password,
                Firstname = firstname,
                Lastname = lastname,
                Email = email
            };

            await _userRepository.AddUser(user);

            var token = GenerateJwtToken(user);
            return (token, user.Username, user.Email);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes),
                Issuer = _jwtIssuer,
                Audience = _jwtAudience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
