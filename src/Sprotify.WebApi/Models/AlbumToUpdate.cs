using Sprotify.Domain;

namespace Sprotify.WebApi.Models
{
    public class AlbumToUpdate : AlbumToCreate
    {
        public AlbumToUpdate()
        {

        }

        public AlbumToUpdate(Album album)
        {
            Title = album.Title;
            ImageUri = album.ImageUri;
            Remarks = album.Remarks;
        }

        public void ApplyTo(Album album)
        {
            album.Title = Title;
            album.ImageUri = ImageUri;
            album.Remarks = Remarks;
        }
    }
}
