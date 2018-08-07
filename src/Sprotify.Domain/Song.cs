using System;
using System.Collections.Generic;

namespace Sprotify.Domain
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public bool ContainsExplicitLyrics { get; set; }
        public long NumberOfPlays { get; set; }

        public ICollection<SongArtist> Artists { get; set; } = new HashSet<SongArtist>();
        public ICollection<AlbumSong> Albums { get; set; } = new HashSet<AlbumSong>();
    }
}
