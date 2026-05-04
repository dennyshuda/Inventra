using FluentValidation;
using FluentValidation.Results;
using Inventra.DTOs;
using Inventra.DTOs.Auth;
using Inventra.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<LoginDto> _loginValidator;

    public UserController(IAuthService authService, SignInManager<IdentityUser> signInManager, IValidator<RegisterDto> registerValidator, IValidator<LoginDto> loginValidator)
    {
        _authService = authService;
        _signInManager = signInManager;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        ValidationResult validationResult = await _registerValidator.ValidateAsync(registerDto);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            ApiResponseDto<AuthResponseDto> errorResponse = ApiResponseDto<AuthResponseDto>.ErrorResult("Validation failed", errors);
            return BadRequest(errorResponse);
        }

        ApiResponseDto<AuthResponseDto> result = await _authService.RegisterAsync(registerDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Berhasil Logout");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {

        ValidationResult validationResult = await _loginValidator.ValidateAsync(loginDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            ApiResponseDto<AuthResponseDto> errorResponse = ApiResponseDto<AuthResponseDto>.ErrorResult("Validation failed", errors);
            return BadRequest(errorResponse);
        }

        ApiResponseDto<AuthResponseDto> result = await _authService.LoginAsync(loginDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
