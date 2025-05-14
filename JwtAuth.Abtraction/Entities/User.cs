namespace JwtAuth.Abtraction.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
