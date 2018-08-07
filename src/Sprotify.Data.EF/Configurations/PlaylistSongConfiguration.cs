using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprotify.Domain;

namespace Sprotify.Data.EF.Configurations
{
    public class PlaylistSongConfiguration : IEntityTypeConfiguration<PlaylistSong>
    {
        public void Configure(EntityTypeBuilder<PlaylistSong> builder)
        {
            builder.HasOne(x => x.Playlist)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.PlaylistId);

            builder.HasOne(x => x.Song)
                .WithMany()
                .HasForeignKey(x => x.SongId);
        }
    }
}
