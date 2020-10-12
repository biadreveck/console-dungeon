using ConsoleDungeon.Helpers;
using ConsoleDungeon.Models;
using System;

namespace ConsoleDungeon.Screens
{
    public class TutorialScreen : Screen
    {
        public override Screen Open()
        {
            if (IsDisposed) return null;

            Console.Clear();

            GuiHelper.Tutorial();

            Console.WriteLine();
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            IsDisposed = true;
            return new GameScreen();
        }
    }
}
