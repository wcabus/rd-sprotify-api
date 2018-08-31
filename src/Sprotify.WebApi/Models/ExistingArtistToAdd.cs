using System;
using System.ComponentModel.DataAnnotations;

namespace Sprotify.WebApi.Models
{
    public class ExistingArtistToAdd
    {
        [Required]
        public Guid ArtistId { get; set; }
    }
}
