using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprotify.Domain.Services
{
    public interface IArtistService
    {
        Task<List<Artist>> GetArtists();
        Task<Artist> GetArtistById(Guid id);
        Task<Artist> CreateArtist(string name, string imageUri);
        Task UpdateArtist(Artist artist);
        Task DeleteArtist(Artist artist);

        Task<bool> ArtistExists(Guid artistId);
        Task<bool> AlbumExists(Guid artistId, Guid albumId);

        Task<List<Album>> GetArtistAlbums(Guid artistId, bool includeSongs);
        Task<Album> GetArtistAlbumById(Guid artistId, Guid albumId, bool includeSongs);
    }
}
