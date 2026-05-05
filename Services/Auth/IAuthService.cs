using Inventra.DTOs;
using Inventra.DTOs.Auth;

namespace Inventra.Services.Auth;

public interface IAuthService
{
    Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);
    Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    Task<ApiResponseDto<UserDto>> GetCurrentUserAsync(string id);
}