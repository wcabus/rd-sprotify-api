using Microsoft.EntityFrameworkCore;
using Sprotify.Data.EF;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprotify.Application.Songs
{
    public class SongService : ISongService
    {
        private readonly SprotifyDbContext _context;

        public SongService(SprotifyDbContext context)
        {
            _context = context;
        }

        public Task<List<Song>> GetSongs()
        {
            return _context.Set<Song>().ToListAsync();
        }

        public Task<Song> GetSongById(Guid id)
        {
            return _context.Set<Song>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Song> CreateSong(string title, TimeSpan duration, DateTime? releaseDate, bool explicitLyrics)
        {
            var song = new Song
            {
                Title = title,
                Duration = duration,
                ReleaseDate = releaseDate,
                ContainsExplicitLyrics = explicitLyrics
            };

            _context.Add(song);
            await _context.SaveChangesAsync();

            return song;
        }

        public Task UpdateSong(Song song)
        {
            _context.Update(song);
            return _context.SaveChangesAsync();
        }

        public Task DeleteSong(Song song)
        {
            _context.Remove(song);
            return _context.SaveChangesAsync();
        }
    }
}
