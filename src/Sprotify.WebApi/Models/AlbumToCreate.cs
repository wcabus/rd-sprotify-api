using System.ComponentModel.DataAnnotations;

namespace Sprotify.WebApi.Models
{
    public class AlbumToCreate
    {
        [Required, StringLength(200)]
        public string Title { get; set; }

        [StringLength(2*1024)]
        public string ImageUri { get; set; }

        [StringLength(4*1024)]
        public string Remarks { get; set; }
    }
}
