using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sprotify.Domain;

namespace Sprotify.Data.EF.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(x => x.ImageUri)
                .IsUnicode()
                .HasMaxLength(1024);
        }
    }
}
