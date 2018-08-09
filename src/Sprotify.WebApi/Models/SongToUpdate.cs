using Sprotify.Domain;

namespace Sprotify.WebApi.Models
{
    public class SongToUpdate : SongToCreate
    {
        public void ApplyTo(Song song)
        {
            song.Title = Title;
            song.Duration = Duration;
            song.ReleaseDate = ReleaseDate;
            song.ContainsExplicitLyrics = ContainsExplicitLyrics;
        }
    }
}
