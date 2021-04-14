using System;
using System.Linq;

namespace Super_Mario
{
    class Program
    {
        static void Main(string[] args)
        {
            int life = int.Parse(Console.ReadLine());

            int matrixSize = int.Parse(Console.ReadLine());

            char[][] matrix = new char[matrixSize][];

            FillMatrix(matrix);

            bool isPrincessSaved = false;
            int[] marioPosition = GetPosition(matrix);
            int currentRow = marioPosition[0];
            int currentCol = marioPosition[1];

            while (!isPrincessSaved && life > 0)
            {
                string[] command = Console.ReadLine()
                                          .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                          .ToArray();

                string direction = command[0];
                int guardPositionRows = int.Parse(command[1]);
                int guardPositionCols = int.Parse(command[2]);

                if (CheckIndex(matrix, guardPositionRows, guardPositionCols))
                {
                    matrix[guardPositionRows][guardPositionCols] = 'B';
                }

                matrix[currentRow][currentCol] = '-';
                life -= 1;
                if (direction == "W")
                {
                    if (CheckIndex(matrix, currentRow - 1, currentCol))
                        currentRow -= 1;
                }
                else if (direction == "S")
                {
                    if (CheckIndex(matrix, currentRow + 1, currentCol))
                        currentRow += 1;
                }
                else if (direction == "A")
                {
                    if (CheckIndex(matrix, currentRow, currentCol - 1))
                        currentCol -= 1;
                }
                else if (direction == "D")
                {
                    if (CheckIndex(matrix, currentRow, currentCol + 1))
                        currentCol += 1;
                }

                if (matrix[currentRow][currentCol] == 'B')
                {
                    life -= 2;
                    if (life <= 0) { matrix[currentRow][currentCol] = 'X'; break; }
                }
                else if (matrix[currentRow][currentCol] == 'P' && life > 0)
                {
                    matrix[currentRow][currentCol] = '-';
                    isPrincessSaved = true;
                    break;
                }

                if(life > 0)
                {
                    matrix[currentRow][currentCol] = 'M';
                }
                else
                {
                    matrix[currentRow][currentCol] = 'X';
                    break;
                }
            }

            if (life <= 0)
            {
                Console.WriteLine($"Mario died at {currentRow};{currentCol}.");
            }
            else if (isPrincessSaved)
            {
                Console.WriteLine($"Mario has successfully saved the princess! Lives left: {life}");
            }

            PrintMatrix(matrix);
        }


        public static void FillMatrix(char[][] matrix)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                string curerntLine = Console.ReadLine();
                char[] characters = curerntLine.ToCharArray();
                matrix[rows] = characters;
            }
        }

        public static void PrintMatrix(char[][] matrix)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    Console.Write($"{matrix[rows][cols]}");
                }
                Console.WriteLine();
            }
        }

        public static bool CheckIndex(char[][] matrix, int row, int col)
        {
            if (row >= 0 && row < matrix.Length)
            {
                if (col >= 0 && col < matrix[row].Length)
                {
                    return true;
                }
            }

            return false;
        }

        public static int[] GetPosition(char[][] matrix)
        {
            int[] position = new int[2];
            bool isFound = false;
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if (matrix[rows][cols] == 'M')
                    {
                        position[0] = rows;
                        position[1] = cols;
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                {
                    break;
                }
            }

            return position;
        }
    }
}
