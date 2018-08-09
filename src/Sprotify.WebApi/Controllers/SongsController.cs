using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using Sprotify.WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sprotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : Controller
    {
        private readonly ISongService _service;

        public SongsController(ISongService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Song>>> GetSongs()
        {
            return await _service.GetSongs();
        }

        [HttpGet("{id:guid}", Name = nameof(GetSongById))]
        public async Task<ActionResult<Song>> GetSongById(Guid id)
        {
            var song = await _service.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        [HttpPost]
        public async Task<ActionResult<Song>> CreateSong(SongToCreate model)
        {
            var song = await _service.CreateSong(model.Title, model.Duration, model.ReleaseDate, model.ContainsExplicitLyrics);
            return CreatedAtRoute(nameof(GetSongById), new { song.Id }, song);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSong(Guid id, SongToUpdate model)
        {
            var song = await _service.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            model.ApplyTo(song);

            await _service.UpdateSong(song);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            var song = await _service.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            await _service.DeleteSong(song);
            return NoContent();
        }
    }
}
