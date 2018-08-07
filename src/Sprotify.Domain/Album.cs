using System;
using System.Collections.Generic;

namespace Sprotify.Domain
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUri { get; set; }

        // Could contain copyright information
        public string Remarks { get; set; }

        // If this album is from a single band. 
        // If null, this could be a compilation album with various artists.
        public Guid? ArtistId { get; set; }
        public Artist Artist { get; set; }

        public ICollection<AlbumSong> Songs { get; set; } = new HashSet<AlbumSong>();
    }
}
