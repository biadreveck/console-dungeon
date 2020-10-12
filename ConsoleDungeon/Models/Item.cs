using ConsoleDungeon.Models.Enums;

namespace ConsoleDungeon.Models
{
    public abstract class Item
    {
        public ItemType Type { get; private set; }
        
        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool IsConsumable { get; private set; }

        public Item(ItemType type, string name)
        {
            Type = type;
            Name = name;
            IsConsumable = false;
        }
        public Item(ItemType type, string name, string description, bool isConsumable = false)
        {
            Type = type;
            Name = name;
            Description = description;
            IsConsumable = isConsumable;
        }
    }
}
