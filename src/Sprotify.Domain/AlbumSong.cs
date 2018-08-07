using System;

namespace Sprotify.Domain
{
    public class AlbumSong
    {
        public Guid SongId { get; set; }
        public Song Song { get; set; }

        public Guid AlbumId { get; set; }
        public Album Album { get; set; }

        public int Index { get; set; }
    }
}
