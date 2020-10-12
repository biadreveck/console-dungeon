using ConsoleDungeon.Core;
using ConsoleDungeon.Models.Enums;
using System;

namespace ConsoleDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();
            manager.StartGame();
        }
    }
}
