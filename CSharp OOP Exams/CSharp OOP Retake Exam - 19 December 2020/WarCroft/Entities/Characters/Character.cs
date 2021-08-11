using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;
        private double health;
        private double armor;

        // TODO: Implement the rest of the class.
        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            this.Name = name;
            this.BaseHealth = health;
            this.Health = health;
            this.BaseArmor = armor;
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

        public double BaseHealth { get; private set; }

        public double Health
        {
            get => this.health;
            set
            {
                if (value > 0 && value <= this.BaseHealth)
                {
                    this.health = value;
                }
            }
        }

        public double BaseArmor { get; private set; }

        public double Armor
        {
            get => this.armor;
            private set
            {
                if (value > 0 && value <= this.BaseArmor)
                {
                    this.armor = value;
                }

            }
        }

        public double AbilityPoints { get; private set; }
        public IBag Bag     { get; private set; }

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
                    IsAlive = false;
                    this.health = 0;
                }
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