using ConsoleDungeon.Helpers;
using System;

namespace ConsoleDungeon.Screens
{
    public class LeaveGameScreen : Screen
    {
        public override Screen Open()
        {
            if (IsDisposed) return null;

            Console.Clear();

            GuiHelper.LeaveGame();

            Console.WriteLine();
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            IsDisposed = true;
            return null;
        }
    }
}
