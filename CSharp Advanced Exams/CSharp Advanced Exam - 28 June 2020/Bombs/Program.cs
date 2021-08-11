using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombs
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> bombEffects = new Queue<int>
                (Console.ReadLine().Split(", ").Select(int.Parse).ToArray());

            Stack<int> bombCasings = new Stack<int>
                (Console.ReadLine().Split(", ").Select(int.Parse).ToArray());

            Dictionary<string, int> bombList = new Dictionary<string, int>();

            bombList.Add("Datura Bombs", 0);
            bombList.Add("Cherry Bombs", 0);
            bombList.Add("Smoke Decoy Bombs", 0);

            while (bombEffects.Count > 0 && bombCasings.Count > 0)
            {
                int bombEff = bombEffects.Peek();
                int bombCas = bombCasings.Peek();

                if (bombEff + bombCas == 40)
                {
                    bombList["Datura Bombs"] += 1;
                }
                else if (bombEff + bombCas == 60)
                {
                    bombList["Cherry Bombs"] += 1;
                }
                else if (bombEff + bombCas == 120)
                {
                    bombList["Smoke Decoy Bombs"] += 1;
                }
                else
                {
                    bombCas = bombCasings.Pop();
                    bombCas -= 5;
                    bombCasings.Push(bombCas);
                    continue;
                }

                bombEff = bombEffects.Dequeue();
                bombCas = bombCasings.Pop();

                if (bombList["Datura Bombs"] >= 3 && bombList["Cherry Bombs"] >= 3 && bombList["Smoke Decoy Bombs"] >= 3)
                {
                    break;
                }
            }

            int countCheck = 0;

            foreach (var item in bombList)
            {
                if (item.Value >= 3)
                {
                    countCheck++;
                }
            }

            if (countCheck == 3)
            {
                Console.WriteLine("Bene! You have successfully filled the bomb pouch!");
            }
            else
            {
                Console.WriteLine("You don't have enough materials to fill the bomb pouch.");
            }

            if (bombEffects.Count != 0 && bombCasings.Count == 0)
            {
                Console.WriteLine($"Bomb Effects: {string.Join(", ", bombEffects)}");
                Console.WriteLine("Bomb Casings: empty");
            }
            else if (bombEffects.Count == 0 && bombCasings.Count != 0)
            {
                Console.WriteLine("Bomb Effects: empty");
                Console.WriteLine($"Bomb Casings: {string.Join(", ", bombCasings)}");
            }
            else if (bombEffects.Count != 0 && bombCasings.Count != 0)
            {
                Console.WriteLine($"Bomb Effects: {string.Join(", ", bombEffects)}");
                Console.WriteLine($"Bomb Casings: {string.Join(", ", bombCasings)}");
            }
            else
            {
                Console.WriteLine("Bomb Effects: empty");
                Console.WriteLine("Bomb Casings: empty");
            }

            foreach (var item in bombList.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
