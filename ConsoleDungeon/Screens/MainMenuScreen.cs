using ConsoleDungeon.Helpers;
using System;
using System.Collections.Generic;

namespace ConsoleDungeon.Screens
{
    public class MainMenuScreen : Screen
    {
        private readonly List<string> options = new List<string>
        {
            "(s)tart (Inicia um novo jogo)",
            "(l)oad (Carrega um jogo salvo)",
            "e(x)it (Sair do jogo)"
        };

        public override Screen Open()
        {
            if (IsDisposed) return null;

            Console.Clear();

            GuiHelper.Header();

            Console.WriteLine();

            GuiHelper.Menu("Menu principal", options);

            Console.WriteLine();

            Console.Write("Selecione uma opção: ");

            var option = Console.ReadLine()?.ToLower();

            switch (option)
            {
                case "start":
                case "s":
                    return new TutorialScreen();
                case "load":
                case "l":
                    Console.WriteLine();

                    if (FileHelper.HasSavedGame())
                    {
                        Console.WriteLine("Essa opção não é válida, tente outra...");

                        var savedGame = FileHelper.LoadGame();

                        if (savedGame != null)
                        {
                            return new GameScreen(savedGame);
                        }
                        else
                        {
                            Console.WriteLine("Não foi possível carregar o jogo salvo, tente outra opção...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Você não possui um jogo salvo, tente outra opção...");
                        Console.ReadKey();
                    }
                    break;
                case "exit":
                case "x":
                    IsDisposed = true;
                    return new LeaveGameScreen();
                default:
                    Console.WriteLine();
                    Console.WriteLine("Essa opção não é válida, tente outra...");
                    Console.ReadKey();
                    break;
            }

            return null;
        }
    }
}
