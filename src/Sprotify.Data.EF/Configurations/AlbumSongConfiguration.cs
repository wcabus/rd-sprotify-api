using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprotify.Domain;

namespace Sprotify.Data.EF.Configurations
{
    public class AlbumSongConfiguration : IEntityTypeConfiguration<AlbumSong>
    {
        public void Configure(EntityTypeBuilder<AlbumSong> builder)
        {
            builder.HasKey(x => new { x.AlbumId, x.SongId });

            builder.HasOne(x => x.Album)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.AlbumId);

            builder.HasOne(x => x.Song)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.SongId);
        }
    }
}
