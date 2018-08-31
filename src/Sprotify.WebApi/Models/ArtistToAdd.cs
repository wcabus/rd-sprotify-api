using System;
using System.ComponentModel.DataAnnotations;

namespace Sprotify.WebApi.Models
{
    public class ArtistToAdd
    {
        public Guid ArtistId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(2048)]
        public string ImageUri { get; set; }
    }
}
