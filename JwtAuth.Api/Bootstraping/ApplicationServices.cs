using JwtAuth.Abtraction.IServices;
using JwtAuth.Infrastructure.Data;
using JwtAuth.Infrastructure.Services.Authentication;

namespace JwtAuth.Api.Bootstraping
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("JwtAuthDatabase")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),

                    ValidateLifetime = true
                };
            });

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
