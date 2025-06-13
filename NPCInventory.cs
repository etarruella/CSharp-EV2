namespace VideoGame.Inventory {

    public class NPCInventory : BaseInventory {

        public override string OwnerType => "NPC";

        public NPCInventory(int size = 10) : base(size) {
        }

        // No se puede soltar items en NPCInventory
        public override bool Drop(IItem item) => false;

        public override bool Drop(int index) => false;

        public override bool Store(IItem item) {
            if (Contains(item)) return true;

            for (int i = 0; i < items.Count; i++) {
                if (items[i] == null) {
                    items[i] = item;
                    return true;
                }
            }
            return false;
        }

        public override bool StoreAt(IItem item, int index) {
            if (index < 0 || index >= Size) return false;

            if (Contains(item) && items[index] != item) return false;

            if (items[index] != null && items[index] != item) return false;

            items[index] = item;
            return true;
        }
    }
}
