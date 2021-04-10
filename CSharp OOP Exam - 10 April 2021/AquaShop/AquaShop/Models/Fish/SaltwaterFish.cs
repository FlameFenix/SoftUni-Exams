using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        private const int size = 5;
        private const int sizeincrease = 2;

        public SaltwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            this.Size = size;
            // Can only live in SaltwaterAquarium!
        }

        public override void Eat()
        {
            this.Size += sizeincrease;
        }
    }
}
