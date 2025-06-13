using System;
using System.Collections.Generic;
using VideoGame.Inventory;

class Program {
    static void Main(string[] args) {
        // Crear jugadores
        Player player1 = new("Link");
        Player player2 = new("Zelda");

        // Crear inventarios
        var playerInventory = new PlayerInventory(player1);
        var npcInventory = new NPCInventory();
        var shopInventory = new ShopInventory();
        var chestInventory = new ChestInventory(new List<IItem> {
            new Potion("Health Potion", 5),
            new Sword("Rusty Sword", 15)
        });

        // Ver que hay en el cofre
        Console.WriteLine("Chest Contents:");
        foreach (var item in chestInventory.ListItems())
            Console.WriteLine($"- {item.Name}");

        // Intentar añadir al cofre (debe fallar)
        var failPotion = new Potion("Fail Potion", 10);
        Console.WriteLine("Trying to store in Chest (should fail): " + chestInventory.Store(failPotion));

        // Sacar ítem del cofre al jugador
        var potionFromChest = chestInventory.GetItemAt(0)!;
        Console.WriteLine("Transfer from Chest to Player: " + chestInventory.Transfer(potionFromChest, playerInventory));

        // Mostrar inventario del jugador
        Console.WriteLine("\nPlayer Inventory:");
        foreach (var item in playerInventory.ListItems())
            Console.WriteLine($"- {item.Name}");

        // Crear un ítem sin precio e intentar meterlo en tienda (debe fallar)
        var worthlessItem = new Sword("Broken Stick");
        Console.WriteLine("Store worthless item in Shop (should fail): " + shopInventory.Store(worthlessItem));

        // Añadir ítem válido a tienda
        var expensiveItem = new Sword("Master Sword", 1000);
        Console.WriteLine("Store valid item in Shop (should work): " + shopInventory.Store(expensiveItem));

        // Ver tienda
        Console.WriteLine("\nShop Inventory:");
        foreach (var item in shopInventory.ListItems())
            Console.WriteLine($"- {item.Name} (Price: {((Item)item).Price})");

        // Transferir ítem de Shop a Player
        Console.WriteLine("Transfer Master Sword to Player: " + shopInventory.Transfer(expensiveItem, playerInventory));

        // NPC recibe ítem
        var smallPotion = new Potion("Small Potion", 3);
        Console.WriteLine("Store to NPC Inventory: " + npcInventory.Store(smallPotion));

        // Intentar soltar desde NPC (debe fallar)
        Console.WriteLine("NPC drops item (should fail): " + npcInventory.Drop(smallPotion));

        // Transferir de NPC al Player
        Console.WriteLine("Transfer from NPC to Player: " + npcInventory.Transfer(smallPotion, playerInventory));

        // Listar final del inventario del jugador
        Console.WriteLine("\nFinal Player Inventory:");
        foreach (var item in playerInventory.ListItems())
            Console.WriteLine($"- {item.Name}");

        // Limpiar inventario
        playerInventory.Clear();
        Console.WriteLine("After Clear, Player Inventory count: " + playerInventory.ListItems().Count);
    }
}
