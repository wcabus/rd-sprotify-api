using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprotify.Domain;

namespace Sprotify.Data.EF.Configurations
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsUnicode()
                .HasMaxLength(4096);

            builder.Property(x => x.ImageUri)
                .IsUnicode()
                .HasMaxLength(1024);

            builder.HasMany(x => x.Songs)
                .WithOne(x => x.Playlist)
                .HasForeignKey(x => x.PlaylistId);
        }
    }
}
