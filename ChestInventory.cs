namespace VideoGame.Inventory {

    public class ChestInventory : BaseInventory {

        public override string OwnerType => "Chest";

        public ChestInventory(IEnumerable<IItem> initialItems) {
            foreach (var item in initialItems) {
                items.Add(item);
                if (item is Item concrete) concrete.MoveTo(this as PlayerInventory);
            }
        }

        public override bool Store(IItem item) {
            // No se puede almacenar
            return false;
        }

        public override bool StoreAt(IItem item, int index) {
            return false;
        }
    }
}
