using VideoGame;

namespace VideoGame.Inventory {

    public partial class PlayerInventory : BaseInventory {

        public Player parent { get; }
        public override string OwnerType => "Player";

        public PlayerInventory(Player player, int size = 10) : base(size) {
            parent = player;
        }

        public override bool Store(IItem item) {
            // Si ya está almacenado, devuelve true sin añadirlo
            if (Contains(item)) return true;

            for (int i = 0; i < items.Count; i++) {
                if (items[i] == null) {
                    items[i] = item;
                    if (item is Item concrete) concrete.MoveTo(this);
                    return true;
                }
            }
            return false; // no hay espacio
        }

        public override bool StoreAt(IItem item, int index) {
            if (index < 0 || index >= Size) return false;

            // Si el item ya está almacenado pero no en la posición indicada, no se puede añadir
            if (Contains(item) && items[index] != item) return false;

            if (items[index] != null && items[index] != item) return false;

            items[index] = item;
            if (item is Item concrete) concrete.MoveTo(this);
            return true;
        }
    }
}
