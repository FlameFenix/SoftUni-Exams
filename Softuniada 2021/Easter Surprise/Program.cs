using System;
using System.Linq;

namespace Easter_Surprise
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizeOfField = Console.ReadLine().Split().Select(int.Parse).ToArray();
            char[,] matrix = new char[sizeOfField[0], sizeOfField[1]];

            FillMatrix(matrix);

            char mark = char.Parse(Console.ReadLine());
            int[] coordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();
            char firstSymbol = matrix[coordinates[0], coordinates[1]];

            EasterSurprise(matrix, mark, firstSymbol, coordinates[0], coordinates[1]);

            PrintMatrix(matrix);
        }

        private static void EasterSurprise(char[,] matrix, char mark,char firstSymbol, int row, int col)
        {
            if(!CheckIndex(matrix, row,col))
            {
                return;
            }

            if (matrix[row, col] != firstSymbol)
            {
                return;
            }

            matrix[row, col] = mark;

            EasterSurprise(matrix, mark, firstSymbol, row + 1, col);
            EasterSurprise(matrix, mark, firstSymbol, row - 1, col);
            EasterSurprise(matrix, mark, firstSymbol, row, col + 1);
            EasterSurprise(matrix, mark, firstSymbol, row, col - 1);
        }

        private static bool CheckIndex(char[,] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0)
                && col >= 0 && col < matrix.GetLength(1);
        }

        private static void PrintMatrix(char[,] matrix)
        {
            for (int rows = 0; rows < matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                {
                    Console.Write($"{matrix[rows,cols]}");
                }
                Console.WriteLine();
            }
        }

        private static void FillMatrix(char[,] matrix)
        {
            for (int rows = 0; rows < matrix.GetLength(0); rows++)
            {
                string currentRow = Console.ReadLine().Replace(" ", "");
                char[] characters = currentRow.ToCharArray();

                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                {
                    matrix[rows, cols] = characters[cols];
                }
            }
        }
    }
}
