using System;
using System.ComponentModel.DataAnnotations;

namespace Sprotify.WebApi.Models
{
    public class SongToCreate
    {
        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool ContainsExplicitLyrics { get; set; } = false;
    }
}
