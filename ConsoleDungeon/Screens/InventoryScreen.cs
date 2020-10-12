using ConsoleDungeon.Core;
using ConsoleDungeon.Helpers;
using ConsoleDungeon.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDungeon.Screens
{
    public class InventoryScreen : Screen
    {
        private GameState State { get; set; }

        public InventoryScreen(GameState state)
        {
            State = state;
        }

        public override Screen Open()
        {
            if (IsDisposed) return null;

            Console.Clear();

            GuiHelper.PlayerInventoryHeader(State.Player);
            Console.WriteLine();

            var availableItems = State.Player.Items
                .Where(i => State.CanUse(i))
                .ToList();
            var unavailableItems = State.Player.Items
                .Where(i => !State.CanUse(i))
                .ToList();

            var availableOptions = availableItems
                .Select(i => $"{i.Name} ({i.Description})")
                .ToList();
            var unavailableOptions = unavailableItems
                .Select(i => $"{i.Name} ({i.Description})")
                .ToList();

            GuiHelper.NumberedMenu("Inventário", availableOptions);
            GuiHelper.Menu(null, unavailableOptions, "--");

            if (State.Player.Items.Count <= 0)
            {
                GuiHelper.Warning("Você não possui nenhum item no inventário");
            }

            Console.WriteLine();
            Console.Write("Selecione um item para usar ou (b)ack para voltar: ");

            var option = Console.ReadLine()?.ToLower();

            if ("back".Equals(option) || "b".Equals(option))
            {
                IsDisposed = true;
                return null;
            }

            int itemNumber = -1;
            try
            {
                itemNumber = Convert.ToInt32(option);
            }
            catch {}

            if (itemNumber <= 0 || availableItems.Count < itemNumber)
            {
                Console.WriteLine();
                Console.WriteLine("Essa opção não é válida, tente outra...");
                Console.ReadKey();
                return null;
            }

            var selectedItem = availableItems[itemNumber - 1];
            if (!State.CanUse(selectedItem))
            {
                Console.WriteLine();
                Console.WriteLine("Não é possível usar esse item no momento, tente outro item...");
                Console.ReadKey();
                return null;
            }

            State.Use(selectedItem);

            IsDisposed = true;
            return null;
        }
    }
}
