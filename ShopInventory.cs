namespace VideoGame.Inventory {

    public class ShopInventory : BaseInventory {

        public override string OwnerType => "Shop";

        public override bool Store(IItem item) {
            if (item is not Item concrete || concrete.Price == null)
                return false;

            if (Contains(item)) return true;

            items.Add(item);
            concrete.MoveTo(this as PlayerInventory);
            return true;
        }
    }
}
