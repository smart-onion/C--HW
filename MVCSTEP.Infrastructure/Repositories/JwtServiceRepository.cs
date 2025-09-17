using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.WebAPI.Settings;

namespace MVCSTEP.Infrastructure.Repositories;

public class JwtServiceRepository: IJwtService
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    

    public string CreateJwt(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        };
        
        var jwt = new JwtSecurityToken(
            issuer: JwtSettings.ISSUER,
            audience: JwtSettings.AUDIENCE,
                
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
            signingCredentials: new SigningCredentials(JwtSettings.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    
  
    public bool ValidateToken(string token)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtSettings.ISSUER,
                ValidateAudience = true,
                ValidAudience = JwtSettings.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = JwtSettings.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };

            ClaimsPrincipal principal =
                _jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            
            if(principal.Identity != null) return principal.Identity.IsAuthenticated;
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}