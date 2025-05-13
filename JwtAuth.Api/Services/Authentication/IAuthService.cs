namespace JwtAuth.Api.Services.Authentication
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto> LoginAsync(UserDto request);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
