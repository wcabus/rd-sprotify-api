using Sprotify.Domain;

namespace Sprotify.WebApi.Models
{
    public class ArtistToUpdate : ArtistToCreate
    {
        public ArtistToUpdate()
        {

        }

        public ArtistToUpdate(Artist artist)
        {
            Name = artist.Name;
            ImageUri = artist.ImageUri;
        }

        public void ApplyTo(Artist artist)
        {
            artist.Name = Name;
            artist.ImageUri = ImageUri;
        }
    }
}
