using ConsoleDungeon.Helpers;
using System;

namespace ConsoleDungeon.Screens
{
    public class EndGameScreen : Screen
    {
        private bool IsVictory { get; }

        public EndGameScreen(bool isVictory)
        {
            IsVictory = isVictory;
        }

        public override Screen Open()
        {
            if (IsDisposed) return null;

            Console.Clear();

            if (IsVictory) GuiHelper.Victory();
            else GuiHelper.Defeat();

            Console.WriteLine();
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            IsDisposed = true;
            return null;
        }
    }
}
