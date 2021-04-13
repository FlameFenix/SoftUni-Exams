using System;
using System.Linq;

namespace Sneaking
{
    class Program
    {
        static void Main(string[] args)
        {
            int sizeOfMatrix = int.Parse(Console.ReadLine());

            char[][] matrix = new char[sizeOfMatrix][];

            FillMatrix(matrix);

            string directions = Console.ReadLine();
            char[] direction = directions.ToCharArray();

            for (int i = 0; i < directions.Length; i++)
            {
                char currentDirection = direction[i];

                if(currentDirection == 'U')
                {

                }
                else if(currentDirection == 'D')
                {

                }
                else if(currentDirection == 'L')
                {

                }
                else if(currentDirection == 'R')
                {

                }
                else if (currentDirection == 'W')
                {

                }
            }
        }

        public static void FillMatrix(char[][] matrix)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                string currentLine = Console.ReadLine();
                char[] characters = currentLine.ToCharArray();
                matrix[rows] = characters;
            }
        }

        public static void EnemiesMove(char[][] matrix)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if(matrix[rows][cols] == 'b')
                    {
                        matrix[rows][cols] = '.';
                        if(matrix[rows][cols + 1] == 'S' )
                        {
                            matrix[rows][cols + 1] = 'b';
                        }
                    }
                    if(matrix[rows][cols] == 'd')
                    {

                    }
                }
            }
        }

        public static bool CheckIndex(char[][] matrix, int row, int col)
        {
            if(row >= 0 && row < matrix.Length)
            {
                if(col >=0 && col < matrix[row].Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
