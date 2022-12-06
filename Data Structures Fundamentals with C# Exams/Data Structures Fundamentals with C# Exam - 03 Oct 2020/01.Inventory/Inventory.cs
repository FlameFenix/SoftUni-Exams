namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Inventory : IHolder
    {
        private Dictionary<int, IWeapon> weapons = new Dictionary<int, IWeapon>();
        public int Capacity => weapons.Count;

        public void Add(IWeapon weapon)
        {
            if (weapons.ContainsKey(weapon.Id))
            {
                return;
            }

            weapons.Add(weapon.Id, weapon);
        }

        public void Clear()
        {
            weapons.Clear();
        }

        public bool Contains(IWeapon weapon) => weapons.ContainsKey(weapon.Id);

        public void EmptyArsenal(Category category)
        {
            var empty = weapons.Values.Where(x => x.Category == category);

            foreach (var gun in weapons)
            {
                gun.Value.Ammunition = 0;
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (!weapons.ContainsKey(weapon.Id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if(weapon.Ammunition >= ammunition)
            {
                weapon.Ammunition -= ammunition;
                return true;
            }

            return false;
        }

        public IWeapon GetById(int id)
        {
            IWeapon weapon = weapons.Values.FirstOrDefault(x => x.Id == id);

            return weapon;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var weapon in weapons)
            {
                yield return weapon.Value;
            }
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            if (!weapons.ContainsKey(weapon.Id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if(weapon.Ammunition + ammunition > weapon.MaxCapacity)
            {
                weapon.Ammunition = weapon.MaxCapacity;
            }
            else
            {
                weapon.Ammunition += ammunition;
            }

            return weapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            var weapon = weapons[id];

            if (!weapons.ContainsKey(id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            weapons.Remove(id);

            return weapon;
        }

        public int RemoveHeavy()
        {
            var heavy = weapons.Values.Where(x => x.Category == Category.Heavy).ToList();
            var count = heavy.Count;

            foreach (var weapon in heavy)
            {
                weapons.Remove(weapon.Id);
            }

            return count;
        }

        public List<IWeapon> RetrieveAll()
            => weapons.Values.ToList();

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        => weapons.Values.Where(x => x.Category >= lower && x.Category <= upper).ToList();

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            if(!weapons.ContainsKey(firstWeapon.Id) || !weapons.ContainsKey(secondWeapon.Id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            var firstWep = weapons[firstWeapon.Id];
            var secondWep = weapons[secondWeapon.Id];
           

            if(firstWep.Category != secondWep.Category)
            {
                return;
            }

            weapons[firstWeapon.Id] = secondWep;
            weapons[secondWeapon.Id] = firstWep;
        }
    }
}
