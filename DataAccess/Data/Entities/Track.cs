using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public Artist Artist { get; set; }
        public string Audio { get; set; }
        public string Img { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}
