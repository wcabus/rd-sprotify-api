using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprotify.Domain.Services
{
    public interface ISongService
    {
        Task<List<Song>> GetSongs();
        Task<Song> GetSongById(Guid id);
        Task<Song> CreateSong(string title, TimeSpan duration, DateTime? releaseDate, bool explicitLyrics);
        Task UpdateSong(Song song);
        Task DeleteSong(Song song);
    }
}
