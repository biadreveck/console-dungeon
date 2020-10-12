using ConsoleDungeon.Helpers;
using ConsoleDungeon.Models;
using System;
using System.Collections.Generic;

namespace ConsoleDungeon.Screens
{
    public class SearchRoomScreen : Screen
    {
        private readonly List<string> options = new List<string>
        {
            "(y)es (Sim, adionar o item ao meu inventário)",
            "(n)o (Não, deixar o item na sala)"
        };

        private Player Player { get; }

        private Room Room { get; }

        public SearchRoomScreen(Player player, Room room)
        {
            Player = player;
            Room = room;
        }

        public override Screen Open()
        {
            if (IsDisposed) return null;

            Console.Clear();

            GuiHelper.SearchRoomHeader(Room.Name);
            Console.WriteLine();

            if (Room.Item != null)
            {
                Console.WriteLine($"Você encontrou um(a) '{Room.Item.Name}'!");
                Console.WriteLine();

                GuiHelper.Question("Deseja adicioná-lo(a) ao seu inventário", options);

                Console.WriteLine();

                Console.Write("Selecione uma opção: ");

                var option = Console.ReadLine()?.ToLower();

                switch (option)
                {
                    case "yes":
                    case "y":
                        var item = Room.PickUpItem();

                        Player.Items.Add(item);

                        Console.WriteLine();
                        Console.WriteLine($"Você adicionou o item '{item.Name}' ao seu inventário!");
                        Console.WriteLine();
                        Console.Write("Pressione qualquer tecla para voltar...");
                        Console.ReadKey();

                        IsDisposed = true;
                        break;
                    case "no":
                    case "n":
                        IsDisposed = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Essa opção não é válida, tente outra...");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Que pena! Nenhum item foi encontrado...");
                Console.WriteLine();
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();

                IsDisposed = true;
            }
            
            return null;
        }
    }
}
