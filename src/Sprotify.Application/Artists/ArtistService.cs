using Microsoft.EntityFrameworkCore;
using Sprotify.Data.EF;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprotify.Application.Artists
{
    public class ArtistService : IArtistService
    {
        private readonly SprotifyDbContext _context;

        public ArtistService(SprotifyDbContext context)
        {
            _context = context;
        }

        public Task<List<Artist>> GetArtists()
        {
            return _context.Set<Artist>().ToListAsync();
        }

        public Task<Artist> GetArtistById(Guid id)
        {
            return _context.Set<Artist>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Artist> CreateArtist(string name, string imageUri)
        {
            var artist = new Artist { Name = name, ImageUri = imageUri };
            _context.Set<Artist>().Add(artist);

            await _context.SaveChangesAsync();
            return artist;
        }

        public Task UpdateArtist(Artist artist)
        {
            _context.Entry(artist).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task DeleteArtist(Artist artist)
        {
            _context.Remove(artist);
            return _context.SaveChangesAsync();
        }

        public Task<bool> ArtistExists(Guid artistId)
        {
            return _context.Set<Artist>().AnyAsync(x => x.Id == artistId);
        }

        public Task<bool> AlbumExists(Guid artistId, Guid albumId)
        {
            return _context.Set<Album>().AnyAsync(x => x.ArtistId == artistId && x.Id == albumId);
        }

        public Task<List<Album>> GetArtistAlbums(Guid artistId, bool includeSongs)
        {
            var query = _context.Set<Album>()
                .Where(x => x.ArtistId == artistId)
                .AsQueryable();

            if (includeSongs)
            {
                query = query.Include(x => x.Songs.Select(s => s.Song.Artists));
            }

            return query.ToListAsync();
        }

        public Task<Album> GetArtistAlbumById(Guid artistId, Guid albumId, bool includeSongs)
        {
            var query = _context.Set<Album>()
                .Where(x => x.ArtistId == artistId && x.Id == albumId)
                .AsQueryable();

            if (includeSongs)
            {
                query = query.Include(x => x.Songs.Select(s => s.Song.Artists));
            }

            return query.SingleOrDefaultAsync();
        }
    }
}
