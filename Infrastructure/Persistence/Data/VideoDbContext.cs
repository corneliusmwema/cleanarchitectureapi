using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data;

public class VideoDBContext : DbContext
{
    public VideoDBContext(DbContextOptions<VideoDBContext> options) : base(options)
    {
    }


    public DbSet<Video> Videos { get; set; }


    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}