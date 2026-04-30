using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventra.DTOs;
using Inventra.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Inventra.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            var token = GenerateJwtToken(user);

            return Ok(new BaseResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Login Berhasil",
                Data = new AuthResponseDto
                {
                    Token = token,
                    Email = user.Email!,
                    Expiry = DateTime.Now.AddHours(3)
                }
            });
        }

        return Unauthorized(new BaseResponse<string> { Success = false, Message = "Email atau Password Salah" });
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

    private string GenerateJwtToken(IdentityUser user)
    {
        var claims = new[] {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
