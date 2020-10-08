using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dndAPI.Models
{
    public class UsersInRoomsModel
    {
        public int id { get; set; }
        public string userId { get; set; }
        public int roomId { get; set; }
        public RoomModel Room { get; set; }


    }
}