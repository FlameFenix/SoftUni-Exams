using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private int capacity = 100;
        private int load;
        private List<Item> items;

        public Bag(int capacity)
        {
            items = new List<Item>();
            this.Capacity = capacity;
        }
        public int Capacity
        {
            get => this.capacity;
            set
            {
                this.capacity = value;
            }
        }

        public int Load
        {
            get => this.load;
            private set
            {
                this.load = value;
            }
        }

        public IReadOnlyCollection<Item> Items
        {
            get => this.items.ToList().AsReadOnly();
        }

        public void AddItem(Item item)
        {
            if(this.Load + item.Weight > this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }

            items.Add(item);
            this.Load += item.Weight;
        }

        public Item GetItem(string name)
        {
            Item item = items.FirstOrDefault(x => x.GetType().Name == name);

            if(items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            if(item == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            this.Load -= item.Weight;
            items.Remove(item);
            
            return item;
        }
    }
}
