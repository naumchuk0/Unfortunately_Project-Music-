using DataAccess.Data.Entities;

namespace Unfortunately_Project.Models
{
    public class CreateMusicModel
    {
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public IFormFile? Audio { get; set; }
        public IFormFile? Img { get; set; }
    }
}
