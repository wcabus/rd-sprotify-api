using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using Sprotify.WebApi.Models;

namespace Sprotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _service;

        public ArtistsController(IArtistService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Artist>>> GetArtists()
        {
            var artists = await _service.GetArtists();
            return artists;
        }

        [HttpGet("{id:guid}", Name = nameof(GetArtistById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Artist>> GetArtistById(Guid id)
        {
            var artist = await _service.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Artist>> CreateArtist(ArtistToCreate model)
        {
            var artist = await _service.CreateArtist(model.Name, model.ImageUri);
            return CreatedAtRoute(nameof(GetArtistById), new { artist.Id }, artist);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateArtist(Guid id, ArtistToUpdate model)
        {
            var artist = await _service.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            model.ApplyTo(artist);

            await _service.UpdateArtist(artist);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            var artist = await _service.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            await _service.DeleteArtist(artist);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchArtist(Guid id, JsonPatchDocument<ArtistToUpdate> patchDocument)
        {
            var artist = await _service.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            var model = new ArtistToUpdate(artist);
            patchDocument.ApplyTo(model);

            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.ApplyTo(artist);
            await _service.UpdateArtist(artist);

            return NoContent();
        }
    }
}