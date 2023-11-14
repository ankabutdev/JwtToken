using Jwt_Token_Learn.DataAccess;
using Jwt_Token_Learn.Models;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Token_Learn.Services.AuthService;

public class LoginService : ILoginService
{
    private readonly AppDbContext _dbContext;
    private readonly IJwtService _jwtService;

    public LoginService(AppDbContext dbContext,
        IJwtService jwtService)
    {
        this._dbContext = dbContext;
        this._jwtService = jwtService;
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var resultHash = Hashlash.ComputeHash512(request.Password);

        var user = await _dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Username == request.Username
            && x.PasswrodHash == resultHash);

        if (user == null)
        {
            throw new Exception("User Not Found");
        }

        return await _jwtService.GenereteTokenAsync(user.Username);
    }
}
