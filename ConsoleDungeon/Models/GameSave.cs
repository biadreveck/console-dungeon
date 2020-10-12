using ConsoleDungeon.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.Models
{
    public class GameSave
    {
        public Map Map { get; set; }

        public int PlayerDamage { get; set; }

        public string PlayerRoomCode { get; set; }

        public List<Item> PlayerItems { get; set; } = new List<Item>();

        public string EnemyRoomCode { get; set; }

        public static GameSave From(GameState state)
        {
            return new GameSave {
                Map = state.CurrentMap,
                EnemyRoomCode = state.Enemy.Room.Code,
                PlayerRoomCode = state.Player.Room.Code,
                PlayerDamage = state.Player.Damage,
                PlayerItems = state.Player.Items
            };
        }
    }
}
