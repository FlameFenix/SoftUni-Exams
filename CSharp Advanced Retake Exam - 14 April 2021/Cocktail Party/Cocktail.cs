using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailParty
{
    public class Cocktail
    {
        private List<Ingredient> ingredients;

        public Cocktail(string name, int capacity, int maxAlcoholLevel)
        {
            ingredients = new List<Ingredient>();
            this.Name = name;
            this.Capacity = capacity;
            this.MaxAlcoholLevel = maxAlcoholLevel;
        }

        public string Name { get; private set; }

        public int Capacity { get; private set; }

        public int MaxAlcoholLevel { get; private set; }

        public int CurrentAlcoholLevel { get => ingredients.Sum(x => x.Alcohol); }

        public void Add(Ingredient ingredient)
        {
            if(!ingredients.Contains(ingredients.FirstOrDefault(x => x.Name == ingredient.Name)) && ingredients.Count < this.Capacity)
            {
                ingredients.Add(ingredient);
            }
        }

        public bool Remove(string name)
        {
            return ingredients.Remove(ingredients.FirstOrDefault(x => x.Name == name));
        }

        public Ingredient FindIngredient(string name)
        {
            return ingredients.FirstOrDefault(x => x.Name == name);
        }

        public Ingredient GetMostAlcoholicIngredient()
        {
            List<Ingredient> mostAlcoholic = ingredients.OrderByDescending(x => x.Alcohol).ToList();

            return mostAlcoholic.FirstOrDefault();
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Cocktail: {this.Name} - Current Alcohol Level: {this.CurrentAlcoholLevel}");
            foreach (var item in ingredients)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
