using ConsoleDungeon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.Models
{
    public class GreatPotion : Item
    {
        public GreatPotion() : base(ItemType.GreatPotion, "Poção Suprema", "Você pode usar esse item para curar toda a sua vida", true) { }
    }
}
