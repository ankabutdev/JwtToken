using Jwt_Token_Learn.DataAccess;
using Jwt_Token_Learn.Entities;
using Jwt_Token_Learn.Models;
using Jwt_Token_Learn.Services;
using Jwt_Token_Learn.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Token_Learn.Controllers;

[Route("api/[controller]/action")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly ILogger<UsersController> _logger;

    public readonly AppDbContext _dbContext;

    public UsersController(ILoginService loginService,
        ILogger<UsersController> logger,
        AppDbContext dbContext)
    {
        this._loginService = loginService;
        _logger = logger;
        _dbContext = dbContext;
    }

    [Authorize]
    [HttpGet("users")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _dbContext.Users.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserDto dto)
    {
        var user = new User()
        {
            Name = dto.Name,
            Username = dto.Username,
            PasswrodHash = Hashlash.ComputeHash512(dto.Passwrod)
        };

        var result = await _dbContext.AddAsync(user);

        await _dbContext.SaveChangesAsync();

        return Ok("Created");
    }

}
