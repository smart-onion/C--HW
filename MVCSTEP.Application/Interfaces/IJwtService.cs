using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Application.Interfaces;

public interface IJwtService
{

    public string CreateJwt(User user);

    public bool ValidateToken(string token);
}