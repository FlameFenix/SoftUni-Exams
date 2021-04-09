using PlayersAndMonsters.Common;
using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Players
{
    public abstract class Player : IPlayer
    {
        private string username;
        private int health;
        private ICardRepository cardRepository;
        private bool isDead;

        public Player(ICardRepository cardRepository, string username, int health)
        {
            this.CardRepository = cardRepository;
            this.Username = username;
            this.Health = health;
            this.cardRepository = cardRepository;
        }
        public ICardRepository CardRepository
        {
            get => this.cardRepository;
            private set
            {
                this.cardRepository = value;
            }
        }

        public string Username
        {
            get => this.username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Player's username cannot be null or an empty string.");
                }

                this.username = value;
            }
        }

        public int Health
        {
            get => this.health;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Player's health bonus cannot be less than zero.");
                }
                this.health = value;
            }
        }

        public bool IsDead
        {
            get => this.isDead; 
            private set
            {
                if(this.Health <= 0)
                {
                    isDead = true;
                }
                isDead = false;
            }
        }
        public void TakeDamage(int damagePoints)
        {
            if(damagePoints < 0)
            {
                throw new ArgumentException("Damage points cannot be less than zero.");
            }

            if(this.Health - damagePoints <= 0)
            {
                this.Health = 0;
                this.isDead = true;
                return;
            }

            this.Health -= damagePoints;
        }
    }
}
