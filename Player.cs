namespace VideoGame {

    /// <summary>
    /// Representa a un jugador concreto
    /// </summary>
    public partial class Player {

        /// <summary>
        /// Inventario del jugador
        /// </summary>
        public Inventory.PlayerInventory Inventory { get; init; }

        public Player() {
            Inventory = new Inventory.PlayerInventory(this);
        }
    }
}
