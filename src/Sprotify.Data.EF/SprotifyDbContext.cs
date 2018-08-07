using Microsoft.EntityFrameworkCore;
using Sprotify.Data.EF.Configurations;
using System;
using System.Linq;
using System.Reflection;

namespace Sprotify.Data.EF
{
    public class SprotifyDbContext : DbContext
    {
        public SprotifyDbContext(DbContextOptions<SprotifyDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumSongConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistSongConfiguration());
            modelBuilder.ApplyConfiguration(new SongArtistConfiguration());
            modelBuilder.ApplyConfiguration(new SongConfiguration());
        }
    }
}
