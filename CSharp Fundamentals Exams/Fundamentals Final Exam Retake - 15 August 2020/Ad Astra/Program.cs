using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ad_Astra
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();

            string pattern = @"(#|\|)([A-Za-z ]+)\1(([0-2][0-9])|(3[0-1]))\/((0[0-9])|(1[0-2]))\/([0-9]{2})\1[0-9]+\1";

            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(text);

            List<string> currentMatch = new List<string>();

            int caloriesSum = 0;
            List<Food> foods = new List<Food>();

            foreach (Match item in matches)
            {
                string currentItem = string.Empty;
                currentItem = item.Value;

                currentItem = currentItem.Replace(@"#", " ");
                currentItem = currentItem.Replace(@"|", " ");
                

                currentMatch = currentItem.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                if(currentMatch.Count == 4)
                {
                    string name = $"{currentMatch[0]} {currentMatch[1]}";
                    Food food = new Food(name, currentMatch[2], int.Parse(currentMatch[3]));
                    caloriesSum += int.Parse(currentMatch[3]);
                    foods.Add(food);
                }
                else
                {
                    Food food = new Food(currentMatch[0], currentMatch[1], int.Parse(currentMatch[2]));
                    caloriesSum += int.Parse(currentMatch[2]);
                    foods.Add(food);
                }
            }
            int days = caloriesSum / 2000;

            Console.WriteLine($"You have food to last you for: {days} days!");

            foreach (var food in foods)
            {
                Console.WriteLine(food.ToString());
            }

        }
    }

    class Food
    {
        public Food(string name, string date, int calories)
        {
            Name = name;
            Date = date;
            Calories = calories;
        }
        public string Name { get; set; }
        public string Date { get; set; }
        public int Calories { get; set; }
        public override string ToString()
        {
            return $"Item: {Name}, Best before: {Date}, Nutrition: {Calories}";
        }
    }
}
