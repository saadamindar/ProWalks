using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProWalks.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Further customization of the Identity model can be done here if needed

            var userId = "fb66d906-5201-4f83-8688-8d69070ffa09";
            var adminId = "47de2545-c521-4c06-89ad-f5b9301eb657";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = userId,
                    ConcurrencyStamp = userId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
