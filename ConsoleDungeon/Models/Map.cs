using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.Models
{
    public class Map
    {
        public string PlayerStart { get; set; }

        public string EnemyStart { get; set; }

        public Dictionary<string, Room> Rooms { get; set; }
    }
}
