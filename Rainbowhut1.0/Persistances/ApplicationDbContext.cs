using Microsoft.EntityFrameworkCore;
using Rainbowhut1._0.Model;

using System;

namespace Rainbowhut1._0.Persistances
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Rainbowhut");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProfileModel>(entity =>
            {
                entity.HasIndex(p => p.ID).IsUnique();
            });

            modelBuilder.Entity<SlideShowModel>(entity =>
            {
                entity.HasIndex(p => p.ID).IsUnique();
            });

            modelBuilder.Entity<GalleryModel>(entity =>
            {
                entity.HasIndex(p => p.ID).IsUnique();
            });

            modelBuilder.Entity<QrCodeModel>(entity =>
            {
                entity.HasIndex(p => p.ID).IsUnique();
            });

        }
        public DbSet<ProfileModel> Profile { get; set; }
        public DbSet<SlideShowModel> Slideshow { get; set; }
        public DbSet<GalleryModel> Gallery { get; set; }
        public DbSet<QrCodeModel> QrCode { get; set; }
    }
}
