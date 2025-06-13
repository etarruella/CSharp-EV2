using System;
using System.Collections.Generic;

namespace VideoGame.Inventory {

    public abstract class BaseInventory {

        protected readonly List<IItem?> items = new();

        public virtual int Size => items.Count;

        public abstract string OwnerType { get; }

        public virtual bool Store(IItem item) {
            if (Contains(item)) return true;
            items.Add(item);
            if (item is Item concrete) concrete.MoveTo(this as PlayerInventory);
            return true;
        }

        public virtual IItem? GetItemAt(int index) {
            if (index < 0 || index >= items.Count) return null;
            return items[index];
        }

        public virtual bool Drop(IItem item) {
            int index = items.IndexOf(item);
            if (index == -1) return false;
            return Drop(index);
        }

        public virtual bool Drop(int index) {
            if (index < 0 || index >= items.Count || items[index] == null) return false;
            if (items[index] is Item concrete) concrete.MoveTo(null!);
            items[index] = null;
            return true;
        }

        public virtual bool Contains(IItem item) => items.Contains(item);

        public virtual ICollection<IItem> ListItems() => items.FindAll(i => i != null)!;

        public virtual void Clear() {
            for (int i = 0; i < items.Count; i++) {
                if (items[i] is Item concrete) concrete.MoveTo(null!);
                items[i] = null;
            }
        }

        public virtual bool Transfer(IItem item, BaseInventory target) {
            if (!Contains(item)) return false;
            if (!target.Store(item)) return false;
            return Drop(item);
        }

        public virtual IItem? Find(Func<IItem, bool> condition) =>
            items.Find(i => i != null && condition(i!));

        public virtual T? Find<T>(Func<T, bool> condition) where T : class, IItem =>
            items.Find(i => i is T t && condition(t)) as T;
    }
}
