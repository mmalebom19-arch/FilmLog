using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using FilmLogAPI.Data;
using FilmLogAPI.DTOs;
using FilmLogAPI.Models;

namespace FilmLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(AuthDto dto)
        {
            var email = dto.Email.Trim().ToLower();
            var password = dto.Password.Trim();

            if (!IsValidGmail(email))
                return BadRequest("Please enter a valid Gmail address.");

            if (_context.Users.Any(u => u.Email.ToLower() == email))
                return BadRequest("Email already registered.");

            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            var token = GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public IActionResult Login(AuthDto dto)
        {
            var email = dto.Email.Trim().ToLower();
            var password = dto.Password.Trim();

            if (!IsValidGmail(email))
                return BadRequest("Please enter a valid Gmail address.");

            var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Unauthorized("Invalid email or password.");

            var token = GenerateToken(user);
            return Ok(new { token });
        }

        private bool IsValidGmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@gmail\.com$");
        }


        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}