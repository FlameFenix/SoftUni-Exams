using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] tasks = Console.ReadLine()
                                 .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                                 .Select(int.Parse)
                                 .ToArray();

            int[] threads = Console.ReadLine()
                                 .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                 .Select(int.Parse)
                                 .ToArray();

            int taskToBeKilled = int.Parse(Console.ReadLine());

            Stack<int> stack = new Stack<int>(tasks);

            Queue<int> queue = new Queue<int>(threads);

            while (stack.Count != 0 && queue.Count != 0)
            {
                if (stack.Peek() == taskToBeKilled)
                {
                    Console.WriteLine($"Thread with value {queue.Peek()} killed task {taskToBeKilled}");
                    break;
                }
                else if (queue.Peek() >= stack.Peek())
                {
                    queue.Dequeue();
                    stack.Pop();
                }
                else if(queue.Peek() < stack.Peek())
                {
                    queue.Dequeue();
                }
            }

            Console.WriteLine(string.Join(" ", queue));
        }
    }
}
