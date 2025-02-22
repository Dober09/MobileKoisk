using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MobileKoisk.Api.Data;
using MobileKoisk.Api.DTOs;
using MobileKoisk.Api.Models;
using MobileKoisk.Api.Services;

namespace MobileKoisk.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDtos>>  Resgister(RegisterDto registerDto)
        {
            if ( await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email already exists");
            }

            var user = new UserData
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                DateOfBirth = registerDto.DateOfBirth,
                PhoneNumber = registerDto.PhoneNumber,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDtos
            {
                UserId = user.UserId,
                Token = token,
                Email = user.Email,
                Name = user.Name,
                UserType = user.UserType,
            }; 
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDtos>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid Credentials");
            }

            if (!user.IsActive)
            {

                return Unauthorized("Account is deactivated");
            }

            user.UpdateLastLogin();
            await _context.SaveChangesAsync();

            var tokem = _jwtService.GenerateToken(user);
            return new AuthResponseDtos
            {
                UserId = user.UserId,
                Token = tokem,
                Email = user.Email,
                Name = user.Name,
                UserType = user.UserType,
            };
        }
    }
}
