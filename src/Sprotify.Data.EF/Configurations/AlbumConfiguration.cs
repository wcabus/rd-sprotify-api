using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprotify.Domain;

namespace Sprotify.Data.EF.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(x => x.ImageUri)
                .IsUnicode()
                .HasMaxLength(1024);

            builder.Property(x => x.Remarks)
                .IsUnicode()
                .HasMaxLength(4096);

            builder.HasOne(x => x.Artist)
                .WithMany()
                .HasForeignKey(x => x.ArtistId);

            builder.HasMany(x => x.Songs)
                .WithOne(x => x.Album)
                .HasForeignKey(x => x.AlbumId);            
        }
    }
}
