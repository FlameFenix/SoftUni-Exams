using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character, IAttacker
    {
        private const double baseHealth = 100;
        private const double baseArmor = 50;
        private const double abilityPoints = 40;
        public Warrior(string name)
            : base(name, baseHealth, baseArmor, abilityPoints, new Satchel())
        {
            this.BaseHealth = baseHealth;
            this.BaseArmor = baseArmor;
            this.Health = BaseHealth;
            this.Armor = BaseArmor;

        }
        public void Attack(Character character)
        {
            this.EnsureAlive();

            if (!character.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }

            if (character.Name == this.Name)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterAttacksSelf);
            }

            character.TakeDamage(this.AbilityPoints);
        }
    }
}
