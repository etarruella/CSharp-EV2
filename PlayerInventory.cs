
namespace VideoGame.Inventory {

    public partial class PlayerInventory {

        /// <summary>
        /// Jugador que contiene al inventario
        /// </summary>
        public Player parent { get; }

        /// <summary>
        /// Capacidad máxima del inventario
        /// </summary>
        public int Size { get; }

        // TODO: Implementar cómo se almacenan los items 


        /// <summary>
        /// Crea el inventario asociado a un jugador
        /// </summary>
        /// <param name="player">Dueño del inventario</param>
        /// <param name="size">Capacidad máxima del inventario</param>
        public PlayerInventory(Player player, int size = 10) {
            parent = player;
            Size = size;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Store(IItem item) {

            return false;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool StoreAt(IItem item, int index) {

            return false;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public IItem? GetItemAt(int index) {
            return null;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Drop(IItem item) {

            return false;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Drop(int index) {

            return false;
        }
        

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public ICollection<IItem> ListItems() {
            return new List<IItem>();
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Contains(IItem item) {
            return false;
        }


        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public IItem? Find(Func<IItem, bool> condition) {
            return null;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public T? Find<T>(Func<T, bool> condition) where T:class,IItem {
            return null;
        }

        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public void Clear() {

        }
        
        /// <summary>
        /// TODO: Implementar
        /// </summary>
        public bool Transfer(IItem item, PlayerInventory target){
            return false;
        }
    }
}