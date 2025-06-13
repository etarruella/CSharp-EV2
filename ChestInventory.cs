using System.Collections.Generic;

namespace VideoGame.Inventory {

    public class ChestInventory : BaseInventory {

        public override string OwnerType => "Chest";

        public ChestInventory(int size = 10) : base(size) {
        }

        public ChestInventory(List<IItem> initialItems) : base(initialItems.Count) {
            for (int i = 0; i < initialItems.Count; i++) {
                items[i] = initialItems[i];
            }
        }

        // No se puede almacenar después de crear el cofre
        public override bool Store(IItem item) => false;

        public override bool StoreAt(IItem item, int index) => false;

        // Se puede sacar items (Drop) normalmente, lista y demás funciones funcionan como en base
    }
}
