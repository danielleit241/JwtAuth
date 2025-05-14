using JwtAuth.Abtraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
