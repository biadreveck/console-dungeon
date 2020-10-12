using ConsoleDungeon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.Models
{
    public class Key : Item
    {
        public Key() : base(ItemType.Key, "Chave", "Você pode usar esse item para abrir uma porta trancada") { }
    }
}
