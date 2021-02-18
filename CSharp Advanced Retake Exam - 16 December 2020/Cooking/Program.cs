using System;
using System.Collections.Generic;
using System.Linq;

namespace Cooking
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] liquids = Console.ReadLine()
                                   .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                   .Select(int.Parse)
                                   .ToArray();

            Dictionary<string, int> food = new Dictionary<string, int>();

            food.Add("Bread", 0);
            food.Add("Cake", 0);
            food.Add("Pastry", 0);
            food.Add("Fruit Pie", 0);

            Queue<int> queueLiquids = new Queue<int>();

            for (int i = 0; i < liquids.Length; i++)
            {
                queueLiquids.Enqueue(liquids[i]);
            }

            int[] ingredients = Console.ReadLine()
                                       .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                       .Select(int.Parse)
                                       .ToArray();

            Stack<int> stackIngredients = new Stack<int>();

            for (int i = 0; i < ingredients.Length; i++)
            {
                stackIngredients.Push(ingredients[i]);
            }

            while (stackIngredients.Count != 0 && queueLiquids.Count != 0)
            {
                bool isEquals = true;

                int currentSum = queueLiquids.Peek() + stackIngredients.Peek();

                if (currentSum == 25)
                {
                    food["Bread"] += 1;
                }
                else if (currentSum == 50)
                {
                    food["Cake"] += 1;
                }
                else if (currentSum == 75)
                {
                    food["Pastry"] += 1;
                }
                else if (currentSum == 100)
                {
                    food["Fruit Pie"] += 1;
                }
                else
                {
                    int currentItem = stackIngredients.Pop();
                    currentItem += 3;
                    stackIngredients.Push(currentItem);
                    queueLiquids.Dequeue();
                    isEquals = false;
                }

                if(isEquals)
                {
                    stackIngredients.Pop();
                    queueLiquids.Dequeue();
                }
            }

            if(food["Bread"] >= 1 && food["Cake"] >= 1 
                && food["Pastry"] >= 1 && food["Fruit Pie"] >= 1)
            {
                Console.WriteLine("Wohoo! You succeeded in cooking all the food!");
                if (stackIngredients.Count > 0 && queueLiquids.Count == 0)
                {
                    Console.WriteLine("Liquids left: none");
                    Console.WriteLine($"Ingredients left: {string.Join(", ", stackIngredients)}");

                }
                else if (queueLiquids.Count > 0 && stackIngredients.Count == 0)
                {
                    Console.WriteLine($"Liquids left: {string.Join(", ", queueLiquids)}");
                    Console.WriteLine("Ingredients left: none");

                }
                else
                {
                    Console.WriteLine("Liquids left: none");
                    Console.WriteLine("Ingredients left: none");
                }
                PrintFood(food);
            }
            else
            {
                Console.WriteLine("Ugh, what a pity! You didn't have enough materials to cook everything.");

                if (stackIngredients.Count > 0 && queueLiquids.Count == 0)
                {
                    Console.WriteLine("Liquids left: none");
                    Console.WriteLine($"Ingredients left: {string.Join(", ", stackIngredients)}");

                }
                else if (queueLiquids.Count > 0 && stackIngredients.Count == 0)
                {
                    Console.WriteLine($"Liquids left: {string.Join(", ", queueLiquids)}");
                    Console.WriteLine("Ingredients left: none");
                    
                }
                else
                {
                    Console.WriteLine("Liquids left: none");
                    Console.WriteLine("Ingredients left: none");
                }

                PrintFood(food);
            }
        }

        private static void PrintFood(Dictionary<string, int> food)
        {
            foreach (var item in food.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
