using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.Models
{
    public class Room
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Item Item { get; set; }

        public bool IsLocked { get; set; }

        public string North { get; set; }

        public string South { get; set; }

        public string East { get; set; }

        public string West { get; set; }

        public Item PickUpItem()
        {
            var item = Item;
            Item = null;
            return item;
        }

        public void Unlock()
        {
            IsLocked = false;
        }

        public bool IsAdjacent(Room room)
        {
            return Code.Equals(room?.North)
                    || Code.Equals(room?.South)
                    || Code.Equals(room?.East)
                    || Code.Equals(room?.West);
        }
    }
}
