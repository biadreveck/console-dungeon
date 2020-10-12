using System;
using System.Collections.Generic;

namespace ConsoleDungeon.Models
{
    public class Enemy
    {
        public int Power { get; private set; } = 1;

        public Room Room { get; private set; }

        public Enemy(Room initialPosition)
        {
            Room = initialPosition;
        }

        public void MoveTo(Room room)
        {
            Room = room;
        }

        public void MoveToRandom(Map map)
        {
            var availableRooms = GetAvailableRooms(map);
            if (availableRooms.Count > 0)
            {
                int index = new Random().Next(0, availableRooms.Count);
                string roomCode = availableRooms[index];
                Room = map.Rooms[roomCode];
            }
        }

        private List<string> GetAvailableRooms(Map map)
        {
            List<string> availableRooms = new List<string>();

            if (!string.IsNullOrEmpty(Room.North)
                && map.Rooms.ContainsKey(Room.North))
            {
                availableRooms.Add(Room.North);
            }
            if (!string.IsNullOrEmpty(Room.South)
                && map.Rooms.ContainsKey(Room.South))
            {
                availableRooms.Add(Room.South);
            }
            if (!string.IsNullOrEmpty(Room.East)
                && map.Rooms.ContainsKey(Room.East))
            {
                availableRooms.Add(Room.East);
            }
            if (!string.IsNullOrEmpty(Room.West)
                && map.Rooms.ContainsKey(Room.West))
            {
                availableRooms.Add(Room.West);
            }

            return availableRooms;
        }

        public bool IsNear(Room room)
        {
            return Room.Code == room.North 
                || Room.Code == room.South 
                || Room.Code == room.East 
                || Room.Code == room.West;
        }
    }
}