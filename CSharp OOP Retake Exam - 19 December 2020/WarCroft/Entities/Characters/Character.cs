using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;
        private double baseHealth;
        private double health;
        private double baseArmor;
        private double armor;
        private double abilityPoints;
        private IBag bag;

        // TODO: Implement the rest of the class.
        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            this.Name = name;
            this.Health = health;
            this.Armor = armor;
            this.AbilityPoints = abilityPoints;
            this.Bag = bag;
        }
        public bool IsAlive { get; set; } = true;

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }
                this.name = value;
            }
        }

        public double BaseHealth
        {
            get => this.baseHealth;
            protected set
            {
                this.baseHealth = value;
            }
        }

        public double Health
        {
            get => this.health;
            set
            {
                if (value >= 0 && value <= baseHealth)
                {
                    this.health = value;
                }
            }
        }

        public double BaseArmor
        {
            get => this.baseArmor;
            protected set
            {
                this.baseArmor = value;
            }
        }

        public double Armor
        {
            get => this.armor;
            protected set
            {
                if (value >= 0 && value <= this.baseArmor)
                {
                    this.armor = value;
                }

            }
        }

        public double AbilityPoints
        {
            get => this.abilityPoints;
            protected set
            {
                this.abilityPoints = value;
            }
        }
        public IBag Bag
        {
            get => this.bag;
            protected set
            {
                this.bag = value;
            }
        }

        public void TakeDamage(double hitPoints)
        {
            this.EnsureAlive();

            if (this.Armor - hitPoints >= 0)
            {
                this.Armor -= hitPoints;
            }
            else if (this.Armor - hitPoints < 0)
            {
                double diffrence = hitPoints - this.Armor;
                this.armor = 0;
                if (this.Health > diffrence)
                {
                    this.Health -= diffrence;
                }
                else
                {
                    this.health = 0;
                    IsAlive = false;

                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }

        public void UseItem(Item item)
        {
            this.EnsureAlive();
            item.AffectCharacter(this);

        }

        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
    }
}