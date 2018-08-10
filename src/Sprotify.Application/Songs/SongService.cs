using Microsoft.EntityFrameworkCore;
using Sprotify.Data.EF;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<bool> HasArtist(Guid songId, Guid artistId)
        {
            return _context.Set<SongArtist>().AnyAsync(x => x.SongId == songId && x.ArtistId == artistId);
        }

        public Task AddArtist(Song song, Artist artist)
        {
            // Check: do we need to load artists first?
            song.Artists.Add(new SongArtist { Artist = artist, Song = song });

            return _context.SaveChangesAsync();
        }

        public async Task RemoveArtist(Song song, Artist artist)
        {
            await LoadArtists(song);
            var songArtist = song.Artists.Where(x => x.ArtistId == artist.Id).SingleOrDefault();
            if (songArtist == null)
            {
                return;
            }

            song.Artists.Remove(songArtist);
            await UpdateSong(song);
        }

        private Task LoadArtists(Song song)
        {
            var songEntity = _context.Entry(song);
            if (!songEntity.Collection(x => x.Artists).IsLoaded)
            {
                return songEntity.Collection(x => x.Artists).LoadAsync();
            }

            return Task.CompletedTask;
        }
    }
}
