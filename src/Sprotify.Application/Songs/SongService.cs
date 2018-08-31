using Microsoft.EntityFrameworkCore;
using Sprotify.Data.EF;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<Song> GetSongById(Guid id, bool includeArtists)
        {
            var query = _context.Set<Song>().AsQueryable();

            if (includeArtists)
            {
                query = query.Include(x => x.Artists);
            }
                
            return query.SingleOrDefaultAsync(x => x.Id == id);
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

        public Task<bool> SongExists(Guid songId)
        {
            return _context.Set<Song>().AnyAsync(x => x.Id == songId);
        }

        public Task<bool> HasArtist(Guid songId, Guid artistId)
        {
            return _context.Set<SongArtist>().AnyAsync(x => x.SongId == songId && x.ArtistId == artistId);
        }

        public Task<List<Artist>> GetSongArtists(Guid songId)
        {
            return _context.Set<SongArtist>()
                .Where(x => x.SongId == songId)
                .Include(x => x.Artist)
                .Select(x => x.Artist)
                .ToListAsync();
        }

        public Task<Artist> GetSongArtistById(Guid songId, Guid artistId)
        {
            return _context.Set<SongArtist>()
                .Where(x => x.SongId == songId && x.ArtistId == artistId)
                .Include(x => x.Artist)
                .Select(x => x.Artist)
                .SingleOrDefaultAsync();
        }

        public Task AddArtist(Song song, Artist artist)
        {
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
