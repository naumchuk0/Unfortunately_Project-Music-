using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
