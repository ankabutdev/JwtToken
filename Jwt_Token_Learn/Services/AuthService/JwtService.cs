using Jwt_Token_Learn.DataAccess;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt_Token_Learn.Services.AuthService;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _dbContext;
    private readonly AppDbContext dbContext;

    public JwtService(IConfiguration configuration)
    {
        this._config = configuration;
    }

    public async Task<string> GenereteTokenAsync(string username)
    {
        // bu malumotlar
        var claims = new Claim[]
        {
                // name 
                new Claim(JwtRegisteredClaimNames.Name, username),
                // identificatori
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // vaqti
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),

        };

        // qandedur algoritm boyicha shifrlanadi
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])),
            SecurityAlgorithms.HmacSha256
            );

        var token = new JwtSecurityToken(
            _config["JWT:Issuer"],
            _config["JWT:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials
            );

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}
