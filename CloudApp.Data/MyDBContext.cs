
using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data
{
    public class MyDBContext: DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
            :base(options)
        { 
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Concert> Concerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDBContext).Assembly);
        }
    }
}
