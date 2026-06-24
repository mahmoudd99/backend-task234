using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task2.Data;
using Task2.Dtos;
using Task2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Task2.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {


        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        

        public AuthController(
     AppDbContext context,
     IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        /// Register
        /// 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
                return BadRequest("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password, 
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");



        }


        /// Login
 


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.Email &&
                    x.Password == dto.Password);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("UserId", user.Id.ToString())
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}
