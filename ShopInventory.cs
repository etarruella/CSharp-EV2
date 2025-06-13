namespace VideoGame.Inventory {

    public class ShopInventory : BaseInventory {

        public override string OwnerType => "Shop";

        public ShopInventory(int initialSize = 10) : base(initialSize) {
            // Shop puede crecer ilimitadamente, lo gestionamos en Store
        }

        public override bool Store(IItem item) {
            // Solo acepta items con precio definido y no nulo
            if (item is Item concrete) {
                if (concrete.Price == null) return false;
            } else {
                return false;
            }

            // Intentamos guardar en el primer slot libre
            for (int i = 0; i < items.Count; i++) {
                if (items[i] == null) {
                    items[i] = item;
                    return true;
                }
            }

            // No hay espacio, ampliamos la capacidad
            items.Add(item);
            Size++;
            return true;
        }

        public override bool StoreAt(IItem item, int index) {
            if (index < 0) return false;

            if (item is Item concrete) {
                if (concrete.Price == null) return false;
            } else {
                return false;
            }

            // Si index está fuera del rango actual, ampliamos la lista con nulls
            while (index >= Size) {
                items.Add(null);
                Size++;
            }

            // Si ya está almacenado en otro slot distinto, no añadir
            if (Contains(item) && items[index] != item) return false;

            if (items[index] != null && items[index] != item) return false;

            items[index] = item;
            return true;
        }
    }
}
