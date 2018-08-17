using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PublishingSite.Model
{
     public class AppDbContext : IdentityDbContext<AppUser>
     {
          public AppDbContext()
          {

          }

          public AppDbContext(DbContextOptions options) : base(options)
          {

          }

          public static AppDbContext Create()
          {
               return new AppDbContext();
          }

          public virtual DbSet<AppUser> AppUsers { get; set; }
          public virtual DbSet<Paper> Papers { get; set; }


          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               // Single Property Configuration
               modelBuilder.Entity<AppUser>().HasKey(e => e.Id);
               modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(e => e.UserId);
               modelBuilder.Entity<IdentityUserRole<string>>().HasKey(e => e.RoleId);
               modelBuilder.Entity<IdentityRole<string>>().HasKey(e => e.Id);
               modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(e => e.Id);
               modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(e => e.Id);
               modelBuilder.Entity<IdentityUserToken<string>>().HasKey(e => e.UserId);

               // ToTable Property Configuration
               modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogin");
               modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRole");
               modelBuilder.Entity<IdentityRole<string>>().ToTable("AspNetRole");
               modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaim");
               modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaim");
               modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserToken");


               // One to Many Relationships Configurations
               modelBuilder.Entity<AppUser>()
                    .HasMany(a => a.Papers)
                    .WithOne(p => p.AppUser)
                    .HasForeignKey(p => p.AppUserId);
          }

          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
               optionsBuilder.UseSqlServer("Server=.;Database=PublishingSite;Trusted_Connection=True;MultipleActiveResultSets=true");
          }
     }
}
