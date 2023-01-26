using System.Collections.Generic;

namespace _01.DogVet
{
    public class Owner
    {
        public Owner(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string, Dog> DogsByName { get; set; } = new Dictionary<string, Dog>();
    }
}