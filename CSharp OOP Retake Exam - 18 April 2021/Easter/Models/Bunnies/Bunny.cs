using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private ICollection<IDye> dyes;
        public Bunny(string name, int energy)
        {
            dyes = new List<IDye>();
            this.Name = name;
            this.Energy = energy;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }
                this.name = value;
            }
        }
        public int Energy 
        {
            get => this.energy;
            protected set
            {
                if(value < 0)
                {
                    this.energy = 0;
                    return;
                }
                this.energy = value;
            }
        }

        public ICollection<IDye> Dyes => dyes.ToList().AsReadOnly();

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }

        public abstract void Work();
    }
}
