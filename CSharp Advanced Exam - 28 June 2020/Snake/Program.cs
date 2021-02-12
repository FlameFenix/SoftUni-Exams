using System;
using System.Linq;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[][] matrix = new char[n][];

            int rowsPosition = 0;
            int colsPosition = 0;

            FillMatrix(matrix, ref rowsPosition, ref colsPosition);

            string direction = Console.ReadLine();

            int snakeFood = 0;

            bool isSnakeGone = false;

            while (true)
            {

                matrix[rowsPosition][colsPosition] = '.';

                if (direction == "left")
                {
                    colsPosition -= 1;
                }
                else if (direction == "right")
                {
                    colsPosition += 1;
                }
                else if (direction == "up")
                {
                    rowsPosition -= 1;
                }
                else if (direction == "down")
                {
                    rowsPosition += 1;
                }

                

                if (isIndexValid(rowsPosition, colsPosition, matrix))
                {
                    char currentSymbol = matrix[rowsPosition][colsPosition];

                    if (currentSymbol == '*')
                    {
                        snakeFood++;
                    }
                    else if(currentSymbol == '-')
                    {

                    }
                    else if(currentSymbol == 'B')
                    {
                        matrix[rowsPosition][colsPosition] = '.';
                        for (int rows = 0; rows < matrix.Length; rows++)
                        {
                            for (int cols = 0; cols < matrix[rows].Length; cols++)
                            {
                                if(matrix[rows][cols] == 'B')
                                {
                                    rowsPosition = rows;
                                    colsPosition = cols;
                                }
                            }
                        }
                    }

                    matrix[rowsPosition][colsPosition] = 'S';
                }
                else
                {
                    isSnakeGone = true;
                    break;
                }

                if (snakeFood == 10)
                {
                    break;
                }

                direction = Console.ReadLine();

                

            }
            if(isSnakeGone)
            {
                Console.WriteLine("Game over!");
            }
            else
            {
                Console.WriteLine("You won! You fed the snake.");
            }

            Console.WriteLine($"Food eaten: {snakeFood}");
            PrintMatrix(matrix);
        }

        public static bool isIndexValid(int rows, int cols, char[][] matrix)
        {
            if(rows >= 0 && rows < matrix.Length
                && cols >= 0 && cols < matrix[rows].Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void PrintMatrix(char[][] matrix)
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

        private static void FillMatrix(char[][] matrix, ref int rowsPosition, ref int colsPosition)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                string currentLine = Console.ReadLine();

                char[] charArray = currentLine.ToCharArray();
                matrix[rows] = charArray;

                bool isSnakeFound = false;

                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if (charArray[cols] == 'S')
                    {
                        rowsPosition = rows;
                        colsPosition = cols;
                        isSnakeFound = true;
                        break;
                    }

                    if (isSnakeFound)
                    {
                        break;
                    }
                }
            }
        }
    }
}
