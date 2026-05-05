using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Inventra.DTOs;
using Inventra.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Inventra.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            IdentityUser? existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

            if (existingUser != null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResult("User with this email already exists");
            }

            IdentityUser user = _mapper.Map<IdentityUser>(registerDto);

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return ApiResponseDto<AuthResponseDto>.ErrorResult("Registration failed", errors);
            }

            AuthResponseDto authResponse = await GenerateJwtToken(user);
            return ApiResponseDto<AuthResponseDto>.SuccessResult(authResponse, "Registration successful");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<AuthResponseDto>.ErrorResult($"Registration error: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResult("Invalid email or password");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResult("Invalid email or password");
            }

            AuthResponseDto authResponse = await GenerateJwtToken(user);
            return ApiResponseDto<AuthResponseDto>.SuccessResult(authResponse, "Login successful");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<AuthResponseDto>.ErrorResult($"Login error: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<UserDto>> GetCurrentUserAsync(string id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return ApiResponseDto<UserDto>.ErrorResult("User not found");
            }

            var userDto = _mapper.Map<UserDto>(user);

            return ApiResponseDto<UserDto>.SuccessResult(userDto);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<UserDto>.ErrorResult($"Error retrieving user: {ex.Message}");
        }
    }

    private async Task<AuthResponseDto> GenerateJwtToken(IdentityUser user)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        UserDto userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            Expiry = tokenDescriptor.Expires.Value,
            Email = userDto.Email ?? ""
        };
    }
}
