using System;
using System.Collections.Generic;
using System.Text;

namespace Sprotify.Domain
{
    public class PlaylistSong
    {
        public Guid Id { get; set; }

        public Guid SongId { get; set; }
        public Song Song { get; set; }

        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        public DateTimeOffset AddedOn { get; set; }
        public int Index { get; set; }
    }
}
