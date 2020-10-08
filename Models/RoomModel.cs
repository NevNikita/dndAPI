using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dndAPI.Models
{
    public class RoomModel
    {
        public int id { get; set; }
        public int worldId { get; set; }
        public string container { get; set; }
        public WorldModel world { get; set; }

    }
}