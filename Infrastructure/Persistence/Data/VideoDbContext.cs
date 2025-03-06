

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data
{
    public class VideoDBContext : DbContext
    {


        public DbSet<Video> Videos { get; set; }


        public DbSet<User> Users { get; set; }


        public VideoDBContext(DbContextOptions<VideoDBContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}