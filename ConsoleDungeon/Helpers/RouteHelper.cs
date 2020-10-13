using ConsoleDungeon.Models;
using System.Collections.Generic;

namespace ConsoleDungeon.Helpers
{
    public class RouteHelper
    {
        public static Room FindNextRoom(Map map, Room source, Room destiny)
        {
            if (source.Code == destiny.Code
                || destiny.IsAdjacent(source))
            {
                return destiny;
            }

            /*** 
             * Procura o caminho mais curto para o destino usando o algoritmo 
             * BFS (Breadth First Search) adaptado para a situação.
             ***/
            return BreadthFirstSearch(map, source, destiny); ;
        }

        private static Room BreadthFirstSearch(Map map, Room source, Room destiny)
        {
            var queue = new Queue<string>();
            queue.Enqueue(source.Code);

            // Dicionário para armazenar a primeira sala do menor caminho até o destino
            Dictionary<string, string> firstRooms = new Dictionary<string, string>
            {
                { source.Code, null }
            };

            do
            {
                var roomCode = queue.Dequeue();
                if (!map.Rooms.ContainsKey(roomCode))
                {
                    continue;
                }

                Room room = map.Rooms[roomCode];
                string firstRoom = firstRooms[roomCode];

                // Quando a sala é adjacente ao destino, retorna a próxima sala inicial
                if (destiny.IsAdjacent(room))
                {
                    return map.Rooms[firstRoom];
                }

                // Verifica a sala ao Norte
                if (!string.IsNullOrEmpty(room.North)
                    && !firstRooms.ContainsKey(room.North))
                {
                    queue.Enqueue(room.North);
                    firstRooms.Add(room.North, firstRoom ?? room.North);
                }

                // Verifica a sala ao Sul
                if (!string.IsNullOrEmpty(room.South)
                    && !firstRooms.ContainsKey(room.South))
                {
                    queue.Enqueue(room.South);
                    firstRooms.Add(room.South, firstRoom ?? room.South);
                }

                // Verifica a sala ao Leste
                if (!string.IsNullOrEmpty(room.East)
                    && !firstRooms.ContainsKey(room.East))
                {
                    queue.Enqueue(room.East);
                    firstRooms.Add(room.East, firstRoom ?? room.East);
                }

                // Verifica a sala ao Oeste
                if (!string.IsNullOrEmpty(room.West)
                    && !firstRooms.ContainsKey(room.West))
                {
                    queue.Enqueue(room.West);
                    firstRooms.Add(room.West, firstRoom ?? room.West);
                }
            } while (queue.Count > 0);

            return null;
        }
    }
}
