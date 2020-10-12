using ConsoleDungeon.Core;
using ConsoleDungeon.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDungeon.Screens
{
    public class LockedRoomScreen : Screen
    {
        private readonly List<string> options = new List<string>
        {
            "(i)nventory (Abre o seu inventário)",
            "(b)ack (Voltar)"
        };

        private GameState State { get; }

        public LockedRoomScreen(GameState state)
        {
            State = state;
        }

        public override Screen Open()
        {
            if (IsDisposed) return null;

            Console.Clear();

            GuiHelper.LockedRoomHeader(State.NextRoom.Name);
            Console.WriteLine();

            if (!State.NextRoom.IsLocked)
            {
                Console.WriteLine($"A sala foi destrancada!");
                Console.WriteLine();
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();

                // Se o jogador estava com o inimigo, toma dano
                if (State.EnemyFound)
                {
                    State.Player.TakeDamage(State.Enemy.Power);

                    Console.WriteLine();
                    Console.WriteLine($"Você tomou {State.Enemy.Power} de dano do inimigo...");
                    Console.ReadKey();

                    if (State.Player.IsDead)
                    {
                        IsDisposed = true;
                        return new EndGameScreen(false);
                    }
                }

                // Move o jogador para a sala destrancada
                State.Player.MoveTo(State.NextRoom);
                State.NextRoom = null;

                // Move o inimigo em direção ao jogador, se puder
                if (State.CanMoveEnemy)
                {
                    var path = RouteHelper.FindShortestPath(State.CurrentMap, State.Enemy.Room, State.Player.Room);
                    if (path != null && path.Count > 0)
                    {
                        State.Enemy.MoveTo(path.First());
                    }
                    else
                    {
                        State.Enemy.MoveToRandom(State.CurrentMap);
                    }
                }

                State.CanMoveEnemy = !State.CanMoveEnemy;

                IsDisposed = true;
                return null;
            }

            GuiHelper.Question("O que deseja fazer", options);
            Console.WriteLine();

            Console.Write("Selecione uma opção: ");

            var option = Console.ReadLine()?.ToLower();

            switch (option)
            {
                case "inventory":
                case "i":
                    return new InventoryScreen(State);
                case "back":
                case "b":
                    State.NextRoom = null;
                    IsDisposed = true;
                    break;
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
