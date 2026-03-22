using BC = BCrypt.Net.BCrypt;
using GameNLog.Data;
using GameNLog.DTOs;
using GameNLog.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace GameNLog.Services
{
    public class AuthService
    {
        private readonly GameNLogContext _context;
        private IConfiguration _config;

        public AuthService(GameNLogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResultDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDTO.Email))
            {
                return new AuthResultDTO 
                {
                    Success = false,
                    Error = "Email already in use"
                };
            }
            if (await _context.Users.AnyAsync(u => u.Username == registerDTO.Username))
            {
                return new AuthResultDTO
                {
                    Success = false,
                    Error = "Username already in use"
                };
            }

            var user = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = BC.HashPassword(registerDTO.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            string jwt = await GenerateJwt(user);
            return new AuthResultDTO
            {
                Success = true,
                Token = jwt
            };

        }

        private async Task<string> GenerateJwt(User user)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
