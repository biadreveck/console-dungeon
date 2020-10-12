using ConsoleDungeon.Models;
using ConsoleDungeon.Models.Enums;

namespace ConsoleDungeon.Core
{
    public class GameState
    {
        public Map CurrentMap { get; set; }

        public Player Player { get; set; }

        public Enemy Enemy { get; set; }

        public Room NextRoom { get; set; }

        public bool CanMoveEnemy { get; set; } = false;

        public bool EnemyFound => Enemy?.Room == Player.Room;

        public bool CanUse(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Sword:
                    return Player.Room.Code == Enemy.Room.Code;
                case ItemType.Key:
                    return NextRoom?.IsLocked ?? false;
                case ItemType.Potion:
                    return Player.Damage > 0 && !EnemyFound;
                case ItemType.GreatPotion:
                    return Player.Damage > 0 && !EnemyFound;
                default:
                    return false;
            }
        }

        public void Use(Item item)
        {
            if (!CanUse(item)) return;

            switch (item.Type)
            {
                case ItemType.Sword:
                    Enemy = null;
                    break;
                case ItemType.Key:
                    NextRoom.Unlock();
                    break;
                case ItemType.Potion:
                    Player.Heal(1);
                    break;
                case ItemType.GreatPotion:
                    Player.HealAll();
                    break;
                default:
                    break;
            }

            if (item.IsConsumable)
            {
                Player.Items.Remove(item);
            }
        }
    }
}
