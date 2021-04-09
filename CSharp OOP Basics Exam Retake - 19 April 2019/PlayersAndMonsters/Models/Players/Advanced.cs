using PlayersAndMonsters.Repositories;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Players
{
    public class Advanced : Player
    {
        public Advanced(ICardRepository cardRepository, string username) 
            : base(new CardRepository(), username, 250)
        {
        }
    }
}
