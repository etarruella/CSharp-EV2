using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoGame.Inventory {

    public abstract class BaseInventory {

        protected List<IItem?> items;
        public int Size { get; protected set; }
        public abstract string OwnerType { get; }

        protected BaseInventory(int size = 10) {
            Size = size;
            items = new List<IItem?>(size);
            // Inicializar con nulls para reservar espacio fijo
            for (int i = 0; i < size; i++) items.Add(null);
        }

        /// <summary>
        /// Devuelve el item en la posición indicada o null si está vacío o fuera de rango
        /// </summary>
        public virtual IItem? GetItemAt(int index) {
            if (index < 0 || index >= Size) return null;
            return items[index];
        }

        /// <summary>
        /// Lista los items no nulos en orden (incluye nulls en la lista, pero solo devuelve no nulos)
        /// </summary>
        public virtual List<IItem> ListItems() {
            // Devuelve todos los items no null manteniendo el orden, excluye espacios vacíos
            return items.Where(i => i != null).Cast<IItem>().ToList();
        }

        /// <summary>
        /// Intenta almacenar un item en el inventario
        /// </summary>
        public abstract bool Store(IItem item);

        /// <summary>
        /// Intenta almacenar un item en posición concreta
        /// </summary>
        public abstract bool StoreAt(IItem item, int index);

        /// <summary>
        /// Intenta eliminar un item del inventario, por objeto
        /// </summary>
        public virtual bool Drop(IItem item) {
            int index = items.IndexOf(item);
            if (index == -1) return false;

            items[index] = null;
            return true;
        }

        /// <summary>
        /// Intenta eliminar un item del inventario, por índice
        /// </summary>
        public virtual bool Drop(int index) {
            if (index < 0 || index >= Size) return false;
            if (items[index] == null) return false;

            items[index] = null;
            return true;
        }

        /// <summary>
        /// Limpia el inventario dejando todos los slots vacíos
        /// </summary>
        public virtual void Clear() {
            for (int i = 0; i < Size; i++) items[i] = null;
        }

        /// <summary>
        /// Comprueba si el inventario contiene el item dado (por referencia)
        /// </summary>
        public virtual bool Contains(IItem item) {
            return items.Contains(item);
        }

        /// <summary>
        /// Busca el primer item que cumpla la condición dada
        /// </summary>
        public virtual IItem? Find(Func<IItem, bool> predicate) {
            return items.Where(i => i != null).Cast<IItem>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Busca el primer item que cumpla la condición y sea del tipo T
        /// </summary>
        public virtual T? Find<T>(Func<T, bool> predicate) where T : class, IItem {
            return items.OfType<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Transfiere un item de este inventario a otro, devolviendo false si no es posible
        /// </summary>
        public virtual bool Transfer(IItem item, BaseInventory target) {
            if (!Contains(item)) return false;
            if (!target.Store(item)) return false;

            if (!Drop(item)) {
                // si no se pudo eliminar del actual inventario, revertir la inserción
                target.Drop(item);
                return false;
            }
            return true;
        }
    }
}
