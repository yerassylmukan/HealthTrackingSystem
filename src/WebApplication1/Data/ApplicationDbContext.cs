using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<HealthEntry> HealthEntries { get; set; }
    public DbSet<DailyAdvice> DailyAdvices { get; set; }
    public DbSet<UserAdviceLog> UserAdviceLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAdviceLog>()
            .HasIndex(x => new { x.UserId, x.DateShown })
            .IsUnique();

        modelBuilder.Entity<HealthEntry>()
            .HasOne(h => h.User)
            .WithMany(u => u.HealthEntries)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAdviceLog>()
            .HasOne(ual => ual.User)
            .WithMany()
            .HasForeignKey(ual => ual.UserId);

        modelBuilder.Entity<UserAdviceLog>()
            .HasOne(ual => ual.Advice)
            .WithMany()
            .HasForeignKey(ual => ual.AdviceId);
    }
}