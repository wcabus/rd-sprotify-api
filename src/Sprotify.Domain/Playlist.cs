using System;
using System.Collections.Generic;

namespace Sprotify.Domain
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; }

        public ICollection<PlaylistSong> Songs { get; set; } = new HashSet<PlaylistSong>();
    }
}
