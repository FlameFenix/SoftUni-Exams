using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;

namespace WarCroft.Entities.Items
{
    public class FirePotion : Item
    {
        private const int weightConst = 5;
        public FirePotion()
            : base(weightConst)
        {

        }

        public override void AffectCharacter(Character character)
        {
            if (character.IsAlive)
            {
                if (character.Health - 20 <= 0)
                {
                    character.IsAlive = false;
                    character.Health = 0;
                }
                else
                {
                    character.Health -= 20;
                }
            }
        }
    }
}
