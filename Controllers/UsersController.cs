using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ventas.Models;
using BCrypt.Net;

namespace Ventas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IConfiguration _configuration;
        private string passwordHash;
        private readonly dotnetventasContext _context;

        public UsersController(IConfiguration config, dotnetventasContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(Usuario _userData)
        {
            
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                
                var user = await GetUser(_userData.Email);
                bool verified = BCrypt.Net.BCrypt.Verify(_userData.Password, user.Password);
                if (user != null && verified)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("IdUsers", user.IdUsers.ToString()),
                        new Claim("Name", user.Name),
                        //new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("sign")]
        public async Task<ActionResult<Usuario>> PostUser(Usuario usuario)
        {
            var user = await GetUser(usuario.Email);
            if(user != null)
            {
                return BadRequest("User already exists");
            }
            
            passwordHash = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            usuario.Password = passwordHash;
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUser", new { id = usuario.IdUsers }, usuario.Email);
        }
        private async Task<Usuario> GetUser(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}