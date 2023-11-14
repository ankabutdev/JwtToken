namespace Jwt_Token_Learn.Services.AuthService;

public interface IJwtService
{
    public Task<string> GenereteTokenAsync(string username);
}
