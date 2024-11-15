using CognifyzDatabaseIntegrationAndUserAuthentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            var adminRoleId = Guid.NewGuid().ToString();
            var adminUserId = Guid.NewGuid().ToString();
            var studentRoleId = Guid.NewGuid().ToString();
            var studentUserId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = studentRoleId,
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            );

            var hasher = new PasswordHasher<User>();

            builder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    Address = "No 4 Unity Str Aboru",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    UserRole = UserRole.Admin,
                    FullName = "Admin Mahmood",
                }
            );

            builder.Entity<User>().HasData(
                new User
                {
                    Id = studentUserId,
                    UserName = "Student",
                    NormalizedUserName = "STUDENT",
                    Email = "student@gmail.com",
                    NormalizedEmail = "STUDENT@GMAIL.COM",
                    Address = "No 5 Harmony Ave Aboru",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Student@123"),
                    SecurityStamp = string.Empty,
                    UserRole = UserRole.Student, 
                    FullName = "Student John Doe",
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = studentRoleId,
                    UserId = studentUserId
                }
            );
        }


    }
}
