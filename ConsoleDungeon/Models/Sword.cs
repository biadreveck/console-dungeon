using ConsoleDungeon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.Models
{
    public class Sword : Item
    {
        public Sword() : base(ItemType.Sword, "Espada", "Você pode usar esse item para enfrentar o inimigo") { }
    }
}
