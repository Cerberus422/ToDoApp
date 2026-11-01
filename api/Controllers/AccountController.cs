using api.Dtos.Account;
using api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.api.Models;

namespace api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
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
            return StatusCode(201, 
                new NewUserDto
                {
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    Token = _tokenService.CreateToken(appUser)
                });
        }

        return BadRequest(createUser.Errors.Select(error => new
        {
            error.Code,
            error.Description
        }));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(s => s.Email == loginDto.Email);

        if (user == null)
            return Unauthorized("User not found !");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if(!result.Succeeded)
            return Unauthorized("Username or Password incorrect");

        return Ok(
            new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
    }   
}