using System;

namespace _02._New_SoftUni_Building
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[,] matrix = new char[n + n / 2, n];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int counter = 0;

                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if(row == 0 && col == 0)
                    {
                        matrix[row, col] = '#';
                        continue;
                    }

                    if (row != 0 && 
                        col != matrix.GetLength(1) - 1 &&
                        matrix[row - 1, col + 1] == '#')
                    {
                        matrix[row, col] = '#';
                        counter = 0;
                        continue;
                    }

                    if (counter == 3)
                    {
                        matrix[row, col] = '#';
                        counter = 0;
                        continue;
                    }

                    matrix[row, col] = '.';
                    counter++;
                }
            }

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row,col]);
                }
                Console.WriteLine();
            }
        }
    }
}
