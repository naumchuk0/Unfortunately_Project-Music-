using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Track Track { get; set; }
        public int TrackId { get; set; }
    }
}
