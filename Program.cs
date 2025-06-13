using System;
using System.Collections.Generic;
using VideoGame;
using VideoGame.Inventory;

class Program {
    static void Main(string[] args) {
        // Crear jugador y su inventario
        Player player = new Player();
        PlayerInventory playerInventory = player.Inventory;

        // Crear NPC y su inventario
        NPC npc = new NPC();
        NPCInventory npcInventory = new NPCInventory();

        // Crear inventario de tienda y cofre
        ShopInventory shopInventory = new ShopInventory();
        ChestInventory chestInventory = new ChestInventory(new List<IItem> {
            new Armor("Iron Armor", 50),
            new Sword("Steel Sword", 100),
            new Potion("Healing Potion", 10)
        });

        Console.WriteLine("=== Inventario del Cofre ===");
        foreach (var item in chestInventory.ListItems()) {
            Console.WriteLine($"- {item.Name} (Price: {(item is Item i ? i.Price : 0)})");
        }

        // Intentar almacenar item en cofre (debe fallar)
        var newPotion = new Potion("Mana Potion", 15);
        Console.WriteLine($"Intentando almacenar en Cofre: {chestInventory.Store(newPotion)} (esperado: False)");

        // Transferir item del cofre al jugador
        var chestItem = chestInventory.GetItemAt(0);
        if (chestItem != null) {
            bool transferred = chestInventory.Transfer(chestItem, playerInventory);
            Console.WriteLine($"Transferir '{chestItem.Name}' del Cofre al Jugador: {transferred}");
        }

        Console.WriteLine("\n=== Inventario del Jugador ===");
        foreach (var item in playerInventory.ListItems()) {
            Console.WriteLine($"- {item.Name} (Price: {(item is Item i ? i.Price : 0)})");
        }

        // Almacenar ítem sin precio en tienda (debe fallar)
        var freeSword = new Sword("Old Stick");
        Console.WriteLine($"Intentar almacenar item sin precio en Tienda: {shopInventory.Store(freeSword)} (esperado: False)");

        // Almacenar ítem con precio en tienda
        var masterSword = new Sword("Master Sword", 1000);
        Console.WriteLine($"Almacenar 'Master Sword' en Tienda: {shopInventory.Store(masterSword)} (esperado: True)");

        Console.WriteLine("\n=== Inventario de la Tienda ===");
        foreach (var item in shopInventory.ListItems()) {
            Console.WriteLine($"- {item.Name} (Price: {(item is Item i ? i.Price : 0)})");
        }

        // Transferir item de tienda a jugador
        bool transferShopToPlayer = shopInventory.Transfer(masterSword, playerInventory);
        Console.WriteLine($"Transferir 'Master Sword' de Tienda a Jugador: {transferShopToPlayer}");

        // Almacenar ítem en inventario NPC
        var npcPotion = new Potion("Small Potion", 5);
        Console.WriteLine($"Almacenar poción en Inventario NPC: {npcInventory.Store(npcPotion)}");

        // Intentar soltar ítem del NPC (debe fallar)
        Console.WriteLine($"NPC intenta soltar ítem: {npcInventory.Drop(npcPotion)} (esperado: False)");

        // Transferir ítem de NPC a jugador
        bool transferNpcToPlayer = npcInventory.Transfer(npcPotion, playerInventory);
        Console.WriteLine($"Transferir 'Small Potion' de NPC a Jugador: {transferNpcToPlayer}");

        Console.WriteLine("\n=== Inventario Final del Jugador ===");
        foreach (var item in playerInventory.ListItems()) {
            Console.WriteLine($"- {item.Name} (Price: {(item is Item i ? i.Price : 0)})");
        }

        // Limpiar inventario del jugador
        playerInventory.Clear();
        Console.WriteLine($"Inventario del jugador tras Clear(): {playerInventory.ListItems().Count} items (esperado: 0)");
    }
}
