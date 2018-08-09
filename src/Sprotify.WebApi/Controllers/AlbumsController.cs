using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Sprotify.Domain;
using Sprotify.Domain.Services;
using Sprotify.WebApi.Models;

namespace Sprotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _service;

        public AlbumsController(IAlbumService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Album>>> GetAlbums()
        {
            return await _service.GetAlbums(false);
        }

        [HttpGet("{id:guid}", Name = nameof(GetAlbumById))]
        public async Task<ActionResult<Album>> GetAlbumById(Guid id)
        {
            var album = await _service.GetAlbumById(id, false);
            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        [HttpPost]
        public async Task<ActionResult<Album>> CreateAlbum(AlbumToCreate model)
        {
            var album = await _service.CreateAlbum(model.Title, model.ImageUri, model.Remarks);
            return CreatedAtRoute(nameof(GetAlbumById), new { album.Id }, album);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAlbum(Guid id, AlbumToUpdate model)
        {
            var album = await _service.GetAlbumById(id, false);
            if (album == null)
            {
                return NotFound();
            }

            model.ApplyTo(album);

            await _service.UpdateAlbum(album);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var album = await _service.GetAlbumById(id, false);
            if (album == null)
            {
                return NotFound();
            }

            await _service.DeleteAlbum(album);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PatchAlbum(Guid id, JsonPatchDocument<AlbumToUpdate> patchDocument)
        {
            var album = await _service.GetAlbumById(id, false);
            if (album == null)
            {
                return NotFound();
            }

            var model = new AlbumToUpdate(album);
            patchDocument.ApplyTo(model);

            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.ApplyTo(album);
            await _service.UpdateAlbum(album);

            return NoContent();
        }
    }
}