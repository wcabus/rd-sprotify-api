using Microsoft.EntityFrameworkCore;
using Sprotify.Data.EF;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprotify.Application.Albums
{
    public class AlbumService : IAlbumService
    {
        private readonly SprotifyDbContext _context;

        public AlbumService(SprotifyDbContext context)
        {
            _context = context;
        }

        public Task<List<Album>> GetAlbums(bool includeSongs)
        {
            var query = _context.Set<Album>()
                .Include(x => x.Artist)
                .AsQueryable();

            if (includeSongs)
            {
                query = query.Include(x => x.Songs.Select(s => s.Song.Artists));
            }

            return query.ToListAsync();
        }

        public Task<Album> GetAlbumById(Guid id, bool includeSongs)
        {
            var query = _context.Set<Album>()
                .Where(x => x.Id == id)
                .Include(x => x.Artist)
                .AsQueryable();

            if (includeSongs)
            {
                query = query.Include(x => x.Songs.Select(s => s.Song.Artists));
            }

            return query.SingleOrDefaultAsync();
        }

        public Task<Album> CreateAlbum(string title, string imageUri, string remarks)
        {
            return CreateAlbum(title, imageUri, remarks, null);
        }

        public async Task<Album> CreateAlbum(string title, string imageUri, string remarks, Artist artist)
        {
            var album = new Album
            {
                Title = title,
                ImageUri = imageUri,
                Remarks = remarks,
                Artist = artist
            };

            _context.Add(album);
            await _context.SaveChangesAsync();

            return album;
        }

        public Task UpdateAlbum(Album album)
        {
            _context.Update(album);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAlbum(Album album)
        {
            _context.Remove(album);
            return _context.SaveChangesAsync();
        }
    }
}
