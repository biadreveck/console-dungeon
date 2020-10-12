using ConsoleDungeon.Models;
using System;
using System.Collections.Generic;

namespace ConsoleDungeon.Helpers
{
    public class GuiHelper
    {
        public static void Header()
        {
            Console.WriteLine("#########################################");
            Console.WriteLine("#            CONSOLE DUNGEON            #");
            Console.WriteLine("#########################################");
        }

        public static void Tutorial()
        {
            Console.WriteLine("########################################");
            Console.Write("Seja bem-vindo(a)! Console Dungeon é um jogo de aventura, ");
            Console.WriteLine("no qual você está preso em uma masmorra cheia de salas.");
            Console.WriteLine("Você inicia o jogo em uma das salas e com 3 pontos de vida.");
            Console.WriteLine("Objetivo:");
            Console.WriteLine(" * Encontrar o item 'Chave' para abrir a sala com o item 'Espada'");
            Console.WriteLine(" * Abrir a sala trancada com o item 'Chave'");
            Console.WriteLine(" * Encontrar o item 'Espada' na sala trancada");
            Console.WriteLine(" * Derrotar o inimigo com a 'Espada'");
            Console.WriteLine("Outras informações:");
            Console.WriteLine(" * O jogo irá informar quando o inimigo estiver em uma sala próxima a sua");
            Console.WriteLine(" * Ao encontrar com o inimigo sem o item 'Espada', você só poderá fugir e perderá 1 ponto de vida");
            Console.WriteLine(" * Você perde o jogo caso sua vida chegue à 0");
            Console.WriteLine("########################################");
        }

        public static void LeaveGame()
        {
            Header();
            Console.WriteLine();
            Console.WriteLine("Obrigado por jogar! Volte sempre!");
        }

        public static void Menu(string title, List<string> options, string optionBullet = ">")
        {
            if (!string.IsNullOrEmpty(title))
            {
                Console.WriteLine($"=== {title} ===");
                Console.WriteLine();
            }

            if (options == null || options.Count <= 0) return;

            foreach (string o in options)
            {
                Console.WriteLine($" {optionBullet} {o}");
            }
        }

        public static void NumberedMenu(string title, List<string> options)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Console.WriteLine($"=== {title} ===");
                Console.WriteLine();
            }

            if (options == null || options.Count <= 0) return;

            for (int i = 0; i < options.Count; i++)
            {
                var o = options[i];
                Console.WriteLine($" {i+1}) {o}");
            }
        }

        public static void Question(string title, List<string> options)
        {
            Console.WriteLine($"* {title}?");
            Console.WriteLine();

            if (options == null || options.Count <= 0) return;
            foreach (string o in options)
            {
                Console.WriteLine($" > {o}");
            }
        }

        public static void Warning(string message)
        {
            Console.WriteLine($">>> {message} <<<");
        }

        public static void PlayerStatus(Player player)
        {
            Console.WriteLine("#########################################");
            Console.WriteLine($"Sala atual: {player.Room.Name}");
            Console.WriteLine($"Vida: {player.CurrentHealth}/{player.Health}");
            Console.WriteLine($"Inventário: {player.Items.Count}");
            foreach (var item in player.Items)
            {
                Console.WriteLine($" * {item.Name} ({item.Description})");
            }
            Console.WriteLine("#########################################");
        }

        public static void PlayerInventoryHeader(Player player)
        {
            Console.WriteLine("#########################################");
            Console.WriteLine($"Sala atual: {player.Room.Name}");
            Console.WriteLine($"Vida: {player.CurrentHealth}/{player.Health}");
            Console.WriteLine("#########################################");
        }

        public static void SearchRoomHeader(string roomName)
        {
            Console.WriteLine("#########################################");
            Console.WriteLine($"Procurando na sala '{roomName}'");
            Console.WriteLine("#########################################");
        }

        public static void LockedRoomHeader(string roomName)
        {
            Console.WriteLine("#########################################");
            Console.WriteLine($"A sala '{roomName}' está trancada");
            Console.WriteLine("#########################################");
        }

        public static void Victory()
        {
            Console.WriteLine(" =======================================");
            Console.WriteLine("|                VITÓRIA                |");
            Console.WriteLine(" =======================================");
            Console.WriteLine();
            Console.WriteLine("Parabéns! Você ganhou!");
            Console.WriteLine("Jogue mais uma vez e derrote o inimigo ainda mais rápido!");
        }

        public static void Defeat()
        {
            Console.WriteLine(" =======================================");
            Console.WriteLine("|                DERROTA                |");
            Console.WriteLine(" =======================================");
            Console.WriteLine();
            Console.WriteLine("Que pena... Você perdeu...");
            Console.WriteLine("Tente mais uma vez, mas fique atento a movimentação do inimigo!");
        }
    }
}
