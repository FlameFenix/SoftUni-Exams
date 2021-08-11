using PlayersAndMonsters.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Repositories.Contracts
{
    public class PlayerRepository : IPlayerRepository
    {
        private List<IPlayer> players;

        public PlayerRepository()
        {
            players = new List<IPlayer>();
        }
        public int Count
        {
            get => this.players.Count;
        }

        public IReadOnlyCollection<IPlayer> Players
        {
            get => players.ToList().AsReadOnly();
        }

        public void Add(IPlayer player)
        {
            if(player == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            IPlayer playerExist = players.FirstOrDefault(x => x.Username == player.Username);

            if (players.Contains(playerExist))
            {
                throw new ArgumentException($"Player {player.Username} already exists!");
            }

            players.Add(player);
        }

        public IPlayer Find(string username)
        {
            IPlayer player = players.FirstOrDefault(x => x.Username == username);

            if(player == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            return player;
        }

        public bool Remove(IPlayer player)
        {
            IPlayer existingPlayer = players.FirstOrDefault(x => x == player);

            if(existingPlayer == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            return players.Remove(existingPlayer);
        }
    }
}
