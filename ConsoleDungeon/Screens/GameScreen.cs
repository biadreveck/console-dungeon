using ConsoleDungeon.Core;
using ConsoleDungeon.Helpers;
using ConsoleDungeon.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDungeon.Screens
{
    public class GameScreen : Screen
    {
        private GameState State { get; set; }

        public GameScreen()
        {
            InitializeNewGame();
        }
        public GameScreen(GameSave savedGame)
        {
            if (savedGame == null)
            {
                InitializeNewGame();
            }
            else
            {
                InitializeSavedGame(savedGame);
            }
        }

        public void InitializeNewGame()
        {
            var currentMap = FileHelper.LoadMap("map1");

            var playerStartRoom = currentMap.Rooms.ContainsKey(currentMap.PlayerStart) ?
                currentMap.Rooms[currentMap.PlayerStart] : currentMap.Rooms.First().Value;

            var enemyStartRoom = currentMap.Rooms.ContainsKey(currentMap.EnemyStart) ?
                currentMap.Rooms[currentMap.EnemyStart] : currentMap.Rooms.Last().Value;

            State = new GameState
            {
                CurrentMap = currentMap,
                Player = new Player(playerStartRoom),
                Enemy = new Enemy(enemyStartRoom)
            };
        }

        public void InitializeSavedGame(GameSave savedGame)
        {
            var currentMap = savedGame.Map;

            var playerStart = savedGame.PlayerRoomCode ?? currentMap.PlayerStart;
            var enemyStart = savedGame.EnemyRoomCode ?? currentMap.EnemyStart;

            var playerStartRoom = currentMap.Rooms.ContainsKey(playerStart) ?
                currentMap.Rooms[playerStart] : currentMap.Rooms.First().Value;

            var enemyStartRoom = currentMap.Rooms.ContainsKey(enemyStart) ?
                currentMap.Rooms[enemyStart] : currentMap.Rooms.Last().Value;

            State = new GameState
            {
                CurrentMap = currentMap,
                Player = new Player(playerStartRoom, savedGame.PlayerDamage, savedGame.PlayerItems),
                Enemy = new Enemy(enemyStartRoom)
            };
        }

        public override Screen Open()
        {
            if (IsDisposed) return null;

            if (State.Enemy == null)
            {
                IsDisposed = true;
                return new EndGameScreen(true);
            }

            Console.Clear();

            GuiHelper.PlayerStatus(State.Player);
            Console.WriteLine();

            var player = State.Player;

            if (State.EnemyFound)
            {
                GuiHelper.Warning("Você encontrou o inimigo!");
                Console.WriteLine();
            }
            else if (player.Room.IsAdjacent(State.Enemy?.Room))
            {
                GuiHelper.Warning("Você sente a presença do inimigo, ele está por perto! Tome cuidado!");
                Console.WriteLine();
            }

            GetAvailableActions(out List<string> actions, out List<string> options);
            GuiHelper.Menu("Lista de ações possíveis", options);

            Console.WriteLine();

            Console.Write("Selecione uma ação: ");

            var action = Console.ReadLine()?.ToLower();

            if (!actions?.Contains(action) ?? true)
            {
                Console.WriteLine();
                Console.WriteLine("Essa ação não é válida, tente outra...");
                Console.ReadKey();

                return null;
            }

            switch (action)
            {
                case "inventory":
                case "i":
                    return new InventoryScreen(State);
                case "search":
                case "h":
                    return new SearchRoomScreen(player, player.Room);
                case "north":
                case "n":
                    State.NextRoom = State.CurrentMap.Rooms[player.Room.North];
                    break;
                case "south":
                case "s":
                    State.NextRoom = State.CurrentMap.Rooms[player.Room.South];
                    break;
                case "east":
                case "e":
                    State.NextRoom = State.CurrentMap.Rooms[player.Room.East];
                    break;
                case "west":
                case "w":
                    State.NextRoom = State.CurrentMap.Rooms[player.Room.West];
                    break;
                case "save":
                case "v":
                    Console.WriteLine();

                    if (FileHelper.SaveGame(GameSave.From(State)))
                    {                        
                        Console.WriteLine("Jogo salvo com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Não foi possível salvar o seu jogo, tente novamente.");
                    }

                    Console.WriteLine();
                    Console.Write("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "exit":
                case "x":
                    IsDisposed = LeaveGame();
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Essa ação não é válida, tente outra...");
                    Console.ReadKey();
                    break;
            }

            if (State.NextRoom != null)
            {
                if (State.NextRoom.IsLocked)
                {
                    return new LockedRoomScreen(State);
                }

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

                // Move o jogador para a nova posição
                player.MoveTo(State.NextRoom);
                State.NextRoom = null;

                // Move o inimigo em direção ao jogador, se puder
                if (State.CanMoveEnemy)
                {
                    var nextEnemyRoom = RouteHelper.FindNextRoom(State.CurrentMap, State.Enemy.Room, State.Player.Room);
                    if (nextEnemyRoom != null)
                    {
                        State.Enemy.MoveTo(nextEnemyRoom);
                    }
                    else
                    {
                        State.Enemy.MoveToRandom(State.CurrentMap);
                    }
                }

                State.CanMoveEnemy = !State.CanMoveEnemy;
            }

            return null;
        }

        private void GetAvailableActions(out List<string> actions, out List<string> options)
        {
            options = new List<string>();
            actions = new List<string>();

            var playerRoom = State.Player.Room;

            if (playerRoom != State.Enemy.Room)
            {
                options.Add("searc(h) (Busca um item na sala)");

                actions.Add("search");
                actions.Add("h");
            }

            options.Add("(i)nventory (Abre o seu inventário)");

            actions.Add("inventory");
            actions.Add("i");

            if (!string.IsNullOrEmpty(playerRoom.North) 
                && State.CurrentMap.Rooms.ContainsKey(playerRoom.North))
            {
                options.Add("(n)orth (Anda para sala ao norte)");

                actions.Add("north");
                actions.Add("n");
            }
            if (!string.IsNullOrEmpty(playerRoom.South) 
                && State.CurrentMap.Rooms.ContainsKey(playerRoom.South))
            {
                options.Add("(s)outh (Anda para sala ao sul)");

                actions.Add("south");
                actions.Add("s");
            }
            if (!string.IsNullOrEmpty(playerRoom.East) 
                && State.CurrentMap.Rooms.ContainsKey(playerRoom.East))
            {
                options.Add("(e)ast (Anda para sala ao leste)");

                actions.Add("east");
                actions.Add("e");
            }
            if (!string.IsNullOrEmpty(playerRoom.West) 
                && State.CurrentMap.Rooms.ContainsKey(playerRoom.West))
            {
                options.Add("(w)est (Anda para sala ao oeste)");

                actions.Add("west");
                actions.Add("w");
            }

            options.Add("sa(v)e (Salva o jogo atual)");
            options.Add("e(x)it (Sair do jogo atual)");

            actions.Add("save");
            actions.Add("v");
            actions.Add("exit");
            actions.Add("x");
        }

        private bool LeaveGame()
        {
            Console.Clear();

            List<string> options = new List<string>
            {
                "(y)es (Sim, sair do jogo)",
                "(n)o (Não, voltar para o jogo)"
            };

            Console.WriteLine();
            GuiHelper.Question("Deseja realmente abandonar o jogo atual", options);

            Console.WriteLine();

            Console.Write("Selecione uma opção: ");

            var option = Console.ReadLine()?.ToLower();

            switch (option)
            {
                case "yes":
                case "y":
                    return true;
                case "no":
                case "n":
                    return false;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Essa opção não é válida, tente outra...");
                    Console.ReadKey();
                    return LeaveGame();
            }
        }
    }
}
