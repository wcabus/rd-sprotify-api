using Microsoft.EntityFrameworkCore;
using Sprotify.Data.EF;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using System;
using System.Collections.Generic;
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

        public async Task<List<Artist>> GetArtists()
        {
            return await _context.Set<Artist>().ToListAsync(); //.ConfigureAwait(false);
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
    }
}
