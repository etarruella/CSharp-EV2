using System.Reflection.Metadata.Ecma335;

namespace VideoGame.Inventory{

    /// <summary>
    /// Clase base de Item
    /// </summary>
    public abstract class Item : IItem {

        /// <summary>
        /// Nombre del item
        /// </summary>
        public string? Name {
            get;
            init;
        }

        /// <summary>
        /// Precio de venta
        /// </summary>
        public virtual int? Price {
            get;
            init;
        }

        /// <summary>
        /// Especifica dónde está el Item
        /// </summary>
        public PlayerInventory? Location { get; private set; }


        public Item(string name, int? price = 0) {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Cambia la ubicación actual del item
        /// </summary>
        public void MoveTo(PlayerInventory newLocation) {
            Location = newLocation;
        }

    }


    /// <summary>
    /// Interfaz de item
    /// ! NO TOCAR
    /// </summary>
    public interface IItem {
        public string? Name {
            get;
            init;
        }

    }
}