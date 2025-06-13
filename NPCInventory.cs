namespace VideoGame.Inventory {

    public class NPCInventory : BaseInventory {

        public override string OwnerType => "NPC";

        public NPCInventory(int size = 10) {
            for (int i = 0; i < size; i++) items.Add(null);
        }

        public override bool Drop(IItem item) => false;

        public override bool Drop(int index) => false;
    }
}
