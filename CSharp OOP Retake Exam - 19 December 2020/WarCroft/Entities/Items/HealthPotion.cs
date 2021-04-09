using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;

namespace WarCroft.Entities.Items
{
    public class HealthPotion : Item
    {
        private const int weightConst = 5;
        public HealthPotion() 
            : base(weightConst)
        {

        }

        public override void AffectCharacter(Character character)
        {
            if(character.IsAlive)
            {
                character.Health += 20;
            }
        }
    }
}
