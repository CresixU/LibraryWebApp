using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Models.Account;
using LibraryAPI.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public interface IAccountService
    {
        Task<int> RegisterUser(RegisterUserDTO dto);
        Task<string> GenerateJwt(LoginUserDTO dto);
    }

    public class AccountService : IAccountService
    {
        private readonly LibraryContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        
        public AccountService(LibraryContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<int> RegisterUser(RegisterUserDTO dto)
        {
            var user = new User()
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Email = dto.Email,
                Address = new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    Number = dto.Number,
                    PostalCode = dto.PostalCode,

                },
            };
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            user.Role = role;

            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.Password = hashedPassword;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<string> GenerateJwt(LoginUserDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null) 
                throw new BadRequestException("Invalid Username or Password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid Username or Password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Firstname} {user.Lastname}"),
                new Claim(ClaimTypes.Email,$"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("City", user.Address.City)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
