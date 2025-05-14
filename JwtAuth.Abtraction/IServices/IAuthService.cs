using JwtAuth.Abtraction.Entities;
using JwtAuth.Abtraction.Models;

namespace JwtAuth.Abtraction.IServices
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto> LoginAsync(UserDto request);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}