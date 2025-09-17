using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MVCSTEP.WebAPI.Settings;

public class JwtSettings
{
    public static string ISSUER = Environment.GetEnvironmentVariable("AUTH_OPTIONS_ISSUER") ?? "Issuer";
    public static string AUDIENCE = Environment.GetEnvironmentVariable("AUTH_OPTIONS_AUDIENCE") ?? "Audience";

    private static string KEY = Environment.GetEnvironmentVariable("AUTH_OPTIONS_KEY") ??
                                "this_is_a_very_long_super_secret_key_1234567890";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}