using Jwt_Token_Learn.Models;
using Jwt_Token_Learn.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_Token_Learn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginService loginService;
    public IJwtService JwtService;


    public AuthController(IJwtService jwtService, ILoginService loginService)
    {
        JwtService = jwtService;
        this.loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsnyc(LoginRequest request)
    {
        try
        {
            var token = await loginService.LoginAsync(request);

            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest("Username or Password is not valid");
        }
    }

}
