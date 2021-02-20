using System;
using System.Linq;

namespace Warships
{
    class Program
    {
        static void Main(string[] args)
        {
            int sizeOfField = int.Parse(Console.ReadLine());

            string[] coordinates = Console.ReadLine()
                                       .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                       .ToArray();

            char[][] matrix = new char[sizeOfField][];

            int firstPlayerShipsCount = 0;
            int secondPlayerShipsCount = 0;
            int countDestroyedShips = 0;
            FillMatrix(matrix, ref firstPlayerShipsCount, ref secondPlayerShipsCount);

            for (int i = 0; i < coordinates.Length; i++)
            {
                int[] currentCordinates = coordinates[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                int rows = currentCordinates[0];
                int cols = currentCordinates[1];

                if(IsIndexValid(matrix, rows, cols))
                {
                    char currentChar = matrix[rows][cols];
                    if(i % 2 == 0)
                    {
                        if (currentChar == '>')
                        {
                            secondPlayerShipsCount--;
                            countDestroyedShips++;
                            matrix[rows][cols] = 'X';
                        }
                        else if (currentChar == '#')
                        {
                            int[] ships = new int[] { countDestroyedShips, firstPlayerShipsCount, secondPlayerShipsCount };

                            ships = DestroyedShipsSum(matrix, rows, cols, ships);
                            ships = DestroyedShipsSum(matrix, rows, cols + 1, ships);
                            ships = DestroyedShipsSum(matrix, rows, cols - 1, ships);
                            ships = DestroyedShipsSum(matrix, rows + 1, cols, ships);
                            ships = DestroyedShipsSum(matrix, rows + 1, cols + 1, ships);
                            ships = DestroyedShipsSum(matrix, rows + 1, cols - 1, ships);
                            ships = DestroyedShipsSum(matrix, rows - 1, cols, ships);
                            ships = DestroyedShipsSum(matrix, rows - 1, cols + 1, ships);
                            ships = DestroyedShipsSum(matrix, rows - 1, cols - 1, ships);

                            countDestroyedShips = ships[0];
                            firstPlayerShipsCount = ships[1];
                            secondPlayerShipsCount = ships[2];
                        }
                    }
                    else if(i % 2 != 0)
                    {
                        if (currentChar == '<')
                        {
                            firstPlayerShipsCount--;
                            countDestroyedShips++;
                            matrix[rows][cols] = 'X';
                        }
                        else if (currentChar == '#')
                        {
                            int[] ships = new int[] { countDestroyedShips, firstPlayerShipsCount, secondPlayerShipsCount };

                            ships = DestroyedShipsSum(matrix, rows, cols, ships);
                            ships = DestroyedShipsSum(matrix, rows, cols + 1, ships);
                            ships = DestroyedShipsSum(matrix, rows, cols - 1, ships);
                            ships = DestroyedShipsSum(matrix, rows + 1, cols, ships);
                            ships = DestroyedShipsSum(matrix, rows + 1, cols + 1, ships);
                            ships = DestroyedShipsSum(matrix, rows + 1, cols - 1, ships);
                            ships = DestroyedShipsSum(matrix, rows - 1, cols, ships);
                            ships = DestroyedShipsSum(matrix, rows - 1, cols + 1, ships);
                            ships = DestroyedShipsSum(matrix, rows - 1, cols - 1, ships);

                            countDestroyedShips = ships[0];
                            firstPlayerShipsCount = ships[1];
                            secondPlayerShipsCount = ships[2];


                        }
                    }
                    
                }
                

            }

            if (firstPlayerShipsCount > 0 && secondPlayerShipsCount == 0)
            {
                Console.WriteLine($"Player One has won the game! {countDestroyedShips} ships have been sunk in the battle.");
            }
            else if (firstPlayerShipsCount == 0 && secondPlayerShipsCount > 0)
            {
                Console.WriteLine($"Player Two has won the game! {countDestroyedShips} ships have been sunk in the battle.");
            }
            else
            {
                Console.WriteLine($"It's a draw! Player One has {firstPlayerShipsCount} ships left. Player Two has {secondPlayerShipsCount} ships left.");
            }
        }

        private static bool IsIndexValid(char[][] matrix, int rows, int cols)
        {
            if (rows >= 0 && rows < matrix.Length
                                && cols >= 0 && cols < matrix[rows].Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int[] DestroyedShipsSum(char[][] matrix, int rows, int cols, int[] destroyedShips)
        {
            if(IsIndexValid(matrix, rows, cols))
            {
                char currentChar = matrix[rows][cols];

                int totalDestroyedShips = destroyedShips[0];
                int firstPlayerShips = destroyedShips[1];
                int secondPlayerShips = destroyedShips[2];

                if(currentChar == '<')
                {
                    totalDestroyedShips++;
                    firstPlayerShips--;
                }
                else if(currentChar == '>')
                {
                    totalDestroyedShips++;
                    secondPlayerShips--;
                }

                destroyedShips[0] = totalDestroyedShips;
                destroyedShips[1] = firstPlayerShips;
                destroyedShips[2] = secondPlayerShips;

                matrix[rows][cols] = 'X';
            }

            return destroyedShips;
        }

        private static void FillMatrix(char[][] matrix, ref int firstPlayerShipsCount, ref int secondPlayerShipsCount)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                string input = Console.ReadLine().Replace(" ", "");

                char[] currentLine = input.ToCharArray();
                matrix[rows] = currentLine;

                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    char currentSymbol = matrix[rows][cols];
                    if (currentSymbol == '>')
                    {
                        secondPlayerShipsCount++;
                    }
                    else if (currentSymbol == '<')
                    {
                        firstPlayerShipsCount++;
                    }
                }
            }
        }
    }
}
