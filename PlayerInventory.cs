namespace VideoGame.Inventory {

    public partial class PlayerInventory : BaseInventory {

        public Player parent { get; }
        public override string OwnerType => "Player";

        private readonly int maxSize;

        public override int Size => maxSize;

        public PlayerInventory(Player player, int size = 10) {
            parent = player;
            maxSize = size;
            for (int i = 0; i < size; i++) items.Add(null);
        }

        public override bool Store(IItem item) {
            if (Contains(item)) return true;

            for (int i = 0; i < items.Count; i++) {
                if (items[i] == null) {
                    items[i] = item;
                    if (item is Item concrete) concrete.MoveTo(this);
                    return true;
                }
            }
            return false;
        }

        public override bool StoreAt(IItem item, int index) {
            if (index < 0 || index >= Size) return false;
            if (items[index] == item) return true;
            if (Contains(item)) return false;
            if (items[index] != null) return false;

            items[index] = item;
            if (item is Item concrete) concrete.MoveTo(this);
            return true;
        }
    }
}
