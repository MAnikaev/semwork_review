using Microsoft.EntityFrameworkCore;
using ZooHelp.Entities;

namespace ZooHelp;

public class AppDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration["Database:ConnectionString"]);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Email);
        modelBuilder.Entity<Chat>()
            .HasOne<User>(c => c.FirstUser)
            .WithMany()
            .HasForeignKey(c => c.FirstUserEmail);
        modelBuilder.Entity<Chat>()
            .HasOne<User>(c => c.SecondUser)
            .WithMany()
            .HasForeignKey(c => c.SecondUserEmail);
    }
}