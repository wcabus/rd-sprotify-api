using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprotify.Domain.Services
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAlbums(bool includeSongs);
        Task<Album> GetAlbumById(Guid id, bool includeSongs);
        Task<Album> CreateAlbum(string title, string imageUri, string remarks);
        Task<Album> CreateAlbum(string title, string imageUri, string remarks, Artist artist);
        Task UpdateAlbum(Album album);
        Task DeleteAlbum(Album album);
    }
}
