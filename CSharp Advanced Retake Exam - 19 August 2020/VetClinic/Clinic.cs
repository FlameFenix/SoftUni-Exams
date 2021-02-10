using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VetClinic
{
    public class Clinic
    {
        public Clinic(int capacity)
        {
            pets = new List<Pet>();
            Capacity = capacity;
        }

        public int Count
        {
            get
            {
                return pets.Count;
            }
        }

        private List<Pet> pets;

        public int Capacity { get; set; }

        public void Add(Pet pet)
        {
            Pet currentPet = pets.Find(x => x.Name == pet.Name && x.Age == pet.Age && x.Owner == x.Owner);

            if(!pets.Contains(currentPet))
            {
                pets.Add(pet);
            }
        }

        public bool Remove(string name)
        {
            Pet currentPet = pets.Find(x => x.Name == name);

            if(pets.Contains(currentPet))
            {
                pets.Remove(currentPet);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Pet GetPet(string name, string owner)
        {
            Pet currentPet = pets.Find(x => x.Name == name && x.Owner == owner);

            if(pets.Contains(currentPet))
            {
                return currentPet;
            }
            else
            {
                return null;
            }
        }

        public Pet GetOldestPet()
        {
            int maxAge = 0;
            string name = string.Empty;

            foreach (var item in pets)
            {
                if(item.Age > maxAge)
                {
                    maxAge = item.Age;
                    name = item.Name;
                }
            }

            Pet currentPet = pets.Find(x => x.Name == name && x.Age == maxAge);

            return currentPet;
        }

        public string GetStatistics()
        {
            string output = string.Empty;

            output += "The clinic has the following patients:" + Environment.NewLine;

            foreach (var item in pets)
            {
                output += $"Pet {item.Name} with owner: {item.Owner}" + Environment.NewLine;
            }

            return output.Trim();
        }
    }
}
