using System;
using System.Collections.Generic;

namespace Sprotify.Domain
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }

        public ICollection<SongArtist> Songs { get; set; } = new HashSet<SongArtist>();
    }
}
