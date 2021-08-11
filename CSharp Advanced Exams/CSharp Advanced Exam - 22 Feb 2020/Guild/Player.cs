using System;
using System.Collections.Generic;
using System.Text;

namespace Guild
{
    public class Player
    {
        public Player(string name, string clas)
        {
            Name = name;
            Class = clas;
            Rank = "Trial";
            Description = "n/a";
        }

        public string Name { get; set; }

        public string Class { get; set; }

        public string Rank { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            string output = string.Empty;

            output += $"Player {Name}: {Class}" + Environment.NewLine;
            output += $"Rank: {Rank}" + Environment.NewLine;
            output += $"Description: {Description}" + Environment.NewLine;

            return output.Trim();
        }
    }
}
