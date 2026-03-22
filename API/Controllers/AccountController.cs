using System.Security.Cryptography;
using System.Text;
using dating_app.Data;
using dating_app.DTOs;
using dating_app.Entities;
using dating_app.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dating_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AppDbContext context,ITokenService tokenService) : BaseApiConroller
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var hmac=new HMACSHA512();

            var user =new AppUser
            {
                DisplayName =registerDto.DisplayName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt =hmac.Key

            };
            context.Users.Add(user);

            return new UserDto
            {
                Id = user.Id,
                DisplayName =user.DisplayName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            }
            ;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>>Login(Login logindto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x =>x.Email ==  logindto.Email);

            if(user == null) return Unauthorized("Invalid email address");

            using var hmac =new HMACSHA512(user.PasswordSalt);

            var computedHash =hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));

            for(var i=0; i< computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i] ) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Id = user.Id,
                DisplayName =user.DisplayName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            }
            ;
        }
    }
}
