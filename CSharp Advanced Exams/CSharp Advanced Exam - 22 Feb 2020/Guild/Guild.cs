using System;
using System.Collections.Generic;
using System.Text;

namespace Guild
{
    public class Guild
    {
        public Guild(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            roster = new List<Player>();
        }

        private List<Player> roster;

        public string Name { get; set; }

        public int Capacity { get; set; }

        public void AddPlayer(Player player)
        {
            if(roster.Count < Capacity)
            {
                roster.Add(player);
            }
        }

        public bool RemovePlayer(string name)
        {
            Player playerToRemove = roster.Find(x => x.Name == name);

            if(roster.Contains(playerToRemove))
            {
                roster.Remove(playerToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PromotePlayer(string name)
        {
            Player player = roster.Find(x => x.Name == name);
            player.Rank = "Member";
        }

        public void DemotePlayer(string name)
        {
            Player player = roster.Find(x => x.Name == name);

            player.Rank = "Trial";
        }

        public Player[] KickPlayersByClass(string clas)
        {
            Player player = roster.Find(x => x.Class == clas);

            Queue<Player> players = new Queue<Player>();

            while (roster.Contains(player))
            {
                players.Enqueue(player);
                roster.Remove(player);
                player = roster.Find(x => x.Class == clas);
            }

            Player[] array = players.ToArray();

            return array;
        }

        public int Count
        {
            get
            {
                return roster.Count;
            }
        }

        public string Report()
        {
            string output = string.Empty;
            output += $"Players in the guild: {Name}" + Environment.NewLine;

            foreach (var item in roster)
            {
                output += $"{item.ToString()}" + Environment.NewLine;
            }

            return output.Trim();
        }
    }
}
