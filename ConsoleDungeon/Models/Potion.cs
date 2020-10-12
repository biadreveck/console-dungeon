using ConsoleDungeon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.Models
{
    public class Potion : Item
    {
        public Potion() : base(ItemType.Potion, "Poção", "Você pode usar esse item para curar 1 ponto de vida", true) { }
    }
}
