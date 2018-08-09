using Sprotify.Domain;

namespace Sprotify.WebApi.Models
{
    public class SongToUpdate : SongToCreate
    {
        public SongToUpdate()
        {

        }

        public SongToUpdate(Song song)
        {
            Title = song.Title;
            Duration = song.Duration;
            ReleaseDate = song.ReleaseDate;
            ContainsExplicitLyrics = song.ContainsExplicitLyrics;
        }

        public void ApplyTo(Song song)
        {
            song.Title = Title;
            song.Duration = Duration;
            song.ReleaseDate = ReleaseDate;
            song.ContainsExplicitLyrics = ContainsExplicitLyrics;
        }
    }
}
