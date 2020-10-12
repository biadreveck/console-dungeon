using ConsoleDungeon.Helpers;
using ConsoleDungeon.Models;
using ConsoleDungeon.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDungeon.Core
{
    public class GameManager
    {
        private Stack<Screen> Screens { get; } = new Stack<Screen>();

        public GameManager()
        {
            Screens.Push(new MainMenuScreen());
        }

        public void StartGame()
        {
            while (Screens.Count > 0)
            {
                var currentScreen = Screens.Peek();
                var newScreen = currentScreen.Open();

                if (currentScreen.IsDisposed)
                    Screens.Pop();

                if (newScreen != null)
                    Screens.Push(newScreen);
            }
        }
    }
}
