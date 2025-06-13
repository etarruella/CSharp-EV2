namespace VideoGame.Inventory {

    public class Armor : Item {

        public Armor(string name = "Starting Armor", int? price = 0) : base(name, price) { }
    }

    public class Sword : Item {

        public Sword(string name = "Starting Sword", int? price = 0) : base(name, price) { }
    }

    public class Potion : Item {
        public Potion(string name = "Potion", int? price = 0) : base(name, price) { }
    }
}
