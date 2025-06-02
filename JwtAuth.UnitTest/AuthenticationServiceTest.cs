using JwtAuth.Infrastructure.Data;
using JwtAuth.Infrastructure.Services.Authentication;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using JwtAuth.Infrastructure.Services.Authentication;
using JwtAuth.Abtraction.Entities;
using Microsoft.AspNetCore.Identity;
using JwtAuth.Abtraction.Models;
using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtAuth.UnitTest
{
    public class AuthenticationServiceTest
    {
        private SqliteConnection _connection = default!;
        private DbContextOptions<AppDbContext> _context = default!;

        private IConfiguration GetJwtTestConfig() => new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                {"Jwt:Key", "3OtmdcjooTLneJsdUbNRSFLw92UiMSRoFoKnedu8vRSlqHkLcJ7okRBTQkh3SBLF"},
                {"Jwt:Issuer", "https://localhost:7187"},
                {"Jwt:Audience", "https://localhost:7187"}
                }).Build();

        private AppDbContext GetDbContext()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _context = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;
            return new AppDbContext(_context);
        }

        [Fact]
        public void Test_RegisterUser_ReturnUser_WhenUserIsNotExist()
        {
            using var context = GetDbContext();
            context.Database.EnsureCreated();

            var authService = new AuthService(context, GetJwtTestConfig());
            var register = new UserDto
            {
                UserName = "test123",
                Password = "test123"
            };

            var user = authService.RegisterAsync(register).Result;
            Assert.NotNull(user);
            Assert.Equal(register.UserName, user.UserName);
            var verifyResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, register.Password);
            Assert.Equal(PasswordVerificationResult.Success, verifyResult);
        }

        [Fact]
        public void Test_RegisterUser_ReturnUser_WhenUserAlreadyExists()
        {
            using var context = GetDbContext();
            context.Database.EnsureCreated();

            var authService = new AuthService(context, GetJwtTestConfig());
            var register1 = new UserDto
            {
                UserName = "test123",
                Password = "test123"
            };

            var user = authService.RegisterAsync(register1).Result;

            var register2 = new UserDto
            {
                UserName = "test123",
                Password = "test123"
            };

            var user2 = authService.RegisterAsync(register2).Result;
            user2.Should().BeNull();
        }

        [Fact]
        public void Test_AuthenticateUser_ReturnsToken_WhenCredentialsAreValid()
        {
            using var context = GetDbContext();
            context.Database.EnsureCreated();

            User user = new()
            {
                UserName = "testuser",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "TestPassword123")
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            var authService = new AuthService(context, GetJwtTestConfig());

            UserDto userDto = new()
            {
                UserName = "testuser",
                Password = "TestPassword123"
            };

            var tokenResponse = authService.LoginAsync(userDto).Result;

            Assert.NotNull(tokenResponse);
            Assert.NotEmpty(tokenResponse.AccessToken);
            Assert.NotEmpty(tokenResponse.RefreshToken);
            Assert.NotNull(tokenResponse.AccessToken);
            Assert.NotNull(tokenResponse.RefreshToken);
            Assert.IsType<TokenResponseDto>(tokenResponse);
        }

        [Fact]
        public void Test_AuthenticateUser_ReturnsNull_WhenCredentialsAreInvalid()
        {
            using var context = GetDbContext();
            context.Database.EnsureCreated();

            User user = new()
            {
                UserName = "testuser",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "TestPassword123")
            };
            context.Add(user);
            context.SaveChanges();


            var authService = new AuthService(context, GetJwtTestConfig());
            var userDto = new UserDto
            {
                UserName = "testuser",
                Password = "WrongPassword"
            };
            var tokenResponse = authService.LoginAsync(userDto).Result;

            tokenResponse.Should().BeNull();
        }

        [Fact]
        public void Test_AuthenticateUser_ReturnsRightClaims_WhenCredentialsAreValid()
        {
            using var context = GetDbContext();
            context.Database.EnsureCreated();

            User user = new()
            {
                Id = Guid.NewGuid(),
                UserName = "testuser",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "TestPassword123"),
                Role = "Admin"
            };

            context.Users.Add(user);
            context.SaveChanges();

            // Act
            var authService = new AuthService(context, GetJwtTestConfig());
            var userDto = new UserDto
            {
                UserName = "testuser",
                Password = "TestPassword123"
            };

            var tokenResponse = authService.LoginAsync(userDto).Result;
            Assert.NotNull(tokenResponse);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(tokenResponse.AccessToken);

            jwtToken.Should().NotBeNull();
            jwtToken.Issuer.Should().Be("https://localhost:7187");
            jwtToken.Audiences.Should().Contain("https://localhost:7187");

            var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            nameClaim.Should().NotBeNull();
            nameClaim!.Value.Should().Be(user.UserName);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            roleClaim.Should().NotBeNull();
            roleClaim!.Value.Should().Be(user.Role);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            userIdClaim.Should().NotBeNull();
            userIdClaim!.Value.Should().Be(user.Id.ToString());
        }
    }
}