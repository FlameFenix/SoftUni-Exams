using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        private int size = 3;

        public FreshwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            this.Size = size;
            // Can only live in FreshwaterAquarium!
        }

        public override void Eat()
        {
            this.Size += size;
        }
    }
}
