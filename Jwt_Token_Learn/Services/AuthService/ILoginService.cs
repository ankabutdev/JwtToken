using Jwt_Token_Learn.Models;

namespace Jwt_Token_Learn.Services.AuthService;

public interface ILoginService
{
    public Task<string> LoginAsync(LoginRequest request);
}
