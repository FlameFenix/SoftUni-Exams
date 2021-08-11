using System;
using System.Linq;

namespace Garden
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizeOfMatrix = Console.ReadLine()
                                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                        .Select(int.Parse)
                                        .ToArray();

            int[,] matrix = new int[sizeOfMatrix[0], sizeOfMatrix[1]];

            FillMatrix(matrix);

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Bloom Bloom Plow")
            {
                int[] position = command.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                        .Select(int.Parse)
                                        .ToArray();

                int positionRows = position[0];
                int positionCols = position[1];

                if(positionRows >= 0 && positionRows < matrix.GetLength(0)
                    || positionCols >= 0 && positionCols < matrix.GetLength(1))
                {
                    for (int rows = 0; rows < matrix.GetLength(0); rows++)
                    {
                        for (int cols = 0; cols < matrix.GetLength(1); cols++)
                        {
                            if (positionCols == cols || positionRows == rows)
                            {
                                matrix[rows, cols] += 1;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid coordinates.");
                }
                

            }

            PrintMatrix(matrix);

        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int rows = 0; rows < matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                {
                    Console.Write($"{matrix[rows, cols]} ");
                }
                Console.WriteLine();
            }
        }

        private static void FillMatrix(int[,] matrix)
        {
            for (int rows = 0; rows < matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                {
                    matrix[rows, cols] = 0;
                }
            }
        }
    }
}
