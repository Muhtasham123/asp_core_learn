namespace learn_asp_clean_structure.Services;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class TokenProviderService : ITokenProviderService
{
    private readonly IConfiguration _configuration;

    public TokenProviderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(int Id, string Email)
    {
        var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Sub, Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]!)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["ISSUER"],
            audience: _configuration["AUDIENCE"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(
                    _configuration["JWT_EXPIRE_MINUTES"]!
                )
            ),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}