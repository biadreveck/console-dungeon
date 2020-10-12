using ConsoleDungeon.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDungeon.Models
{
    public class Player
    {
        public int Health { get; private set; } = 3;

        public int Damage { get; private set; } = 0;

        public List<Item> Items { get; } = new List<Item>();

        public Room Room { get; private set; }

        public int CurrentHealth => !IsDead ? Health - Damage : 0;

        public bool IsDead => Damage >= Health;

        public bool HasSword => Items.Any(i => i.Type == ItemType.Sword);

        public bool HasKey => Items.Any(i => i.Type == ItemType.Key);

        public Player(Room initialRoom)
        {
            Room = initialRoom;
        }
        public Player(Room initialRoom, int damage, List<Item> items)
        {
            Room = initialRoom;
            Damage = damage;
            Items = items;
        }

        public void MoveTo(Room room)
        {
            Room = room;
        }

        public void Heal(int damageToHeal)
        {
            Damage = damageToHeal > Damage ? 0 : Damage - damageToHeal;
        }

        public void HealAll()
        {
            Damage = 0;
        }

        public void TakeDamage(int damage)
        {
            Damage += damage;
        }
    }
}
