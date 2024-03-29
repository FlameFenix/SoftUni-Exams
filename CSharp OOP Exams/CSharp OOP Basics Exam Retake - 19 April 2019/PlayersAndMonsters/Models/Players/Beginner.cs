﻿using PlayersAndMonsters.Repositories;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Players
{
    public class Beginner : Player
    {
        public Beginner(ICardRepository cardRepository, string username) 
            : base(new CardRepository(), username, 50)
        {

        }
    }
}
