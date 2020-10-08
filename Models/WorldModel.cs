using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dndAPI.Models
{
    public class WorldModel
    {
        public int id { get; set; }
        public string container { get; set; }
        public bool isPrivate { get; set; }
    }

    public class WorldDTO
    {
        public int id { get; set; }
        public string container { get; set; }
        public bool isPrivate { get; set; }
    }
}