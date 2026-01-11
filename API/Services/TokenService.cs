using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dating_app.Entities;
using dating_app.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace dating_app.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenkey = config["TokenKey"]?? throw new Exception("Cannot get token key");

        if(tokenkey.Length < 64)
        {
            throw new Exception("Your token key needs to be > 64 chars");
        }
        var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id),
        };

        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires =DateTime.UtcNow.AddDays(7),
            SigningCredentials =creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token =tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
        // throw new NotImplementedException();
    }

}
