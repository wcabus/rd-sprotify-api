using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprotify.Domain;

namespace Sprotify.Data.EF.Configurations
{
    public class SongArtistConfiguration : IEntityTypeConfiguration<SongArtist>
    {
        public void Configure(EntityTypeBuilder<SongArtist> builder)
        {
            builder.HasKey(x => new { x.SongId, x.ArtistId });

            builder.HasOne(x => x.Artist)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.ArtistId);

            builder.HasOne(x => x.Song)
                .WithMany(x => x.Artists)
                .HasForeignKey(x => x.SongId);
        }
    }
}
