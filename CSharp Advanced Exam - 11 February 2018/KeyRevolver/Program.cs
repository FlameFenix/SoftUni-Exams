using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyRevolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int priceOfBullet = int.Parse(Console.ReadLine());
            int sizeOfBarrel = int.Parse(Console.ReadLine());

            int[] inputBullets = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            Stack<int> bullets = new Stack<int>(inputBullets);

            int[] inputLocks = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            Queue<int> locks = new Queue<int>(inputLocks);

            int intelligence = int.Parse(Console.ReadLine());
            int countBullets = 0;
            while (bullets.Count != 0 && locks.Count != 0)
            {
                //If the bullet has a smaller or equal size to the current lock, print “Bang!”, then remove the lock. 
                //If not, print “Ping!” , leaving the lock intact. The bullet is removed in both cases.
                int currentBullet = bullets.Peek();
                int currentLock = locks.Peek();
                if(currentBullet <= currentLock)
                {
                    Console.WriteLine("Bang!");
                    locks.Dequeue();
                }
                else
                {
                    Console.WriteLine("Ping!");
                }
                bullets.Pop();
                countBullets++;
                if(countBullets > 0 && countBullets % sizeOfBarrel == 0 && bullets.Count > 0)
                {
                    Console.WriteLine("Reloading!");
                }
            }
            Console.WriteLine($"{(locks.Count <= 0 ? $"{bullets.Count} bullets left. Earned ${intelligence - (countBullets * priceOfBullet)}" : $"Couldn't get through. Locks left: {locks.Count}")}");
        }
    }
}
