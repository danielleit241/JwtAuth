namespace JwtAuth.Api.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = string.Empty;
        public string? RefeshToken { get; set; }
        public DateTime? RefeshTokenExpiryTime { get; set; }
    }
}
