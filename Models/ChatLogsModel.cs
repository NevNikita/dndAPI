using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dndAPI.Models
{
    public class ChatLogsModel
    {
        public int id { get; set; }
        public int roomId { get; set; }
        public bool inGame { get; set; }
        public int userId { get; set; }
        public string message { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public RoomModel room { get; set; }

    }
}