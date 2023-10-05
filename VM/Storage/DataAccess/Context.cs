using VM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VM.Storage.DataAccess;

public class Context : IdentityDbContext<IdentityUser>
{
	public Context(DbContextOptions<Context> options) : base(options)
	{
	}

	public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUser>(entity => {
            entity.ToTable(name: "Users");
        });
        builder.Entity<IdentityRole>(entity => {
            entity.ToTable(name: "Roles");
        });
        builder.Entity<IdentityUserRole<string>>(entity => {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<string>>(entity => {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(entity => {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityUserToken<string>>(entity => {
            entity.ToTable("UserTokens");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity => {
            entity.ToTable("RoleClaims");
        });
    }
}
