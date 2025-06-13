using System;
using System.Collections.Generic;

namespace VideoGame.Inventory
{

    public partial class PlayerInventory
    {

        /// <summary>
        /// Jugador que contiene al inventario
        /// </summary>
        public Player parent { get; }

        /// <summary>
        /// Capacidad máxima del inventario
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Array interno de ítems
        /// </summary>
        private readonly IItem?[] items;

        /// <summary>
        /// Crea el inventario asociado a un jugador
        /// </summary>
        /// <param name="player">Dueño del inventario</param>
        /// <param name="size">Capacidad máxima del inventario</param>
        public PlayerInventory(Player player, int size = 10)
        {
            parent = player;
            Size = size;
            items = new IItem?[size];
        }

        public bool Store(IItem item)
        {
            if (Contains(item))
            {
                return true; // Ya está en el inventario, no lo volvemos a insertar
            }

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    if (item is Item concreteItem)
                    {
                        concreteItem.MoveTo(this);
                    }
                    return true;
                }
            }

            return false; // Inventario lleno
        }

        public bool StoreAt(IItem item, int index)
        {
            if (index < 0 || index >= Size || items[index] != null) return false;

            if (Contains(item))
            {
                return true; // Ya está en el inventario, no lo insertamos otra vez
            }

            items[index] = item;
            if (item is Item concreteItem)
            {
                concreteItem.MoveTo(this);
            }
            return true;
        }


        /// <summary>
        /// Obtiene el ítem en una posición o null si no hay
        /// </summary>
        public IItem? GetItemAt(int index)
        {
            if (index < 0 || index >= Size) return null;
            return items[index];
        }

        /// <summary>
        /// Elimina un ítem del inventario si se encuentra
        /// </summary>
        public bool Drop(IItem item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == item)
                {
                    items[i] = null;
                    if (item is Item concreteItem)
                    {
                        concreteItem.MoveTo(null!);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Elimina un ítem de una posición específica
        /// </summary>
        public bool Drop(int index)
        {
            if (index < 0 || index >= Size || items[index] == null) return false;
            if (items[index] is Item concreteItem)
            {
                concreteItem.MoveTo(null!);
            }
            items[index] = null;
            return true;
        }

        /// <summary>
        /// Devuelve una colección con todos los ítems actuales
        /// </summary>
        public ICollection<IItem> ListItems()
        {
            List<IItem> currentItems = new List<IItem>();
            foreach (var item in items)
            {
                if (item != null) currentItems.Add(item);
            }
            return currentItems;
        }

        /// <summary>
        /// Indica si un ítem está en el inventario
        /// </summary>
        public bool Contains(IItem item)
        {
            foreach (var i in items)
            {
                if (i == item) return true;
            }
            return false;
        }

        /// <summary>
        /// Busca un ítem que cumpla una condición
        /// </summary>
        public IItem? Find(Func<IItem, bool> condition)
        {
            foreach (var item in items)
            {
                if (item != null && condition(item)) return item;
            }
            return null;
        }

        /// <summary>
        /// Busca un ítem de tipo T que cumpla una condición
        /// </summary>
        public T? Find<T>(Func<T, bool> condition) where T : class, IItem
        {
            foreach (var item in items)
            {
                if (item is T typedItem && condition(typedItem)) return typedItem;
            }
            return null;
        }

        /// <summary>
        /// Vacía completamente el inventario
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] is Item concreteItem)
                {
                    concreteItem.MoveTo(null!);
                }
                items[i] = null;
            }
        }

        /// <summary>
        /// Transfiere un ítem a otro inventario
        /// </summary>
        public bool Transfer(IItem item, PlayerInventory target)
        {
            if (!Contains(item)) return false;
            if (!target.Store(item)) return false;
            return Drop(item);
        }
    }
}
