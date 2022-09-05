using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NubSkull.DTOs;

namespace NubSkull.Authentication;

public class JWTTokenHandler : IJWTTokenHandler
{
    private readonly string Key;

    public JWTTokenHandler(string key)
    {
        Key = key;
    }

    public string GenerateToken(UserDto userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        List<Claim> Claims = new List<Claim>();
              Claims.Add(new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()));
              Claims.Add(new Claim(ClaimTypes.Name, userDto.UserName));
              Claims.Add(new Claim(ClaimTypes.Email, userDto.EmailAddress));
            foreach (var item in userDto.UserRoles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, item));
            }
           var key = Encoding.ASCII.GetBytes(Key);
           var tokenDescriptor = new SecurityTokenDescriptor
           {
                Subject = new ClaimsIdentity(Claims),
                IssuedAt = System.DateTime.UtcNow,
                Expires = System.DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
           };
           var token = tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        
    }
}
