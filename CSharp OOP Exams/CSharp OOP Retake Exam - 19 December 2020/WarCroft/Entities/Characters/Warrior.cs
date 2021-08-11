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
        private const double basehealth = 100;
        private const double basearmor = 50;
        private const double abilitypoints = 40;
        public Warrior(string name)
            : base(name, basehealth, basearmor, abilitypoints, new Satchel())
        {

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
