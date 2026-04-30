using Inventra.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: true, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Ok(new { message = "Login Berhasil!" });
        }

        if (result.IsLockedOut)
        {
            return BadRequest("Akun Anda terkunci sementara.");
        }

        return Unauthorized("Email atau Password salah.");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded) return Ok("User Berhasil Dibuat");
        return BadRequest(result.Errors);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Berhasil Logout");
    }

}
