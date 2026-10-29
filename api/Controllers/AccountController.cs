using api.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.api.Models;

namespace api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var appUser = new AppUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };

        var createUser = await _userManager.CreateAsync(
            appUser, registerDto.Password);

        if (createUser.Succeeded)
        {
            return StatusCode(201, "User created successfully");
        }

        return BadRequest(createUser.Errors.Select(error => new
        {
            error.Code,
            error.Description
        }));
    }
}