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
            bool isAlive = true;
            bool isNikoladzeDead = false;
            int[] deadLocation = new int[2];
            for (int i = 0; i < directions.Length; i++)
            {
                for (int rows = 0; rows < matrix.Length; rows++)
                {
                    bool isSet = false;
                    for (int cols = 0; cols < matrix[rows].Length; cols++)
                    {
                        deadLocation = GetPosition(matrix, deadLocation);
                        
                        
                        if (matrix[rows][cols] == 'b' && isAlive && !isNikoladzeDead)
                        {
                            bool isValid = CheckIndex(matrix, rows, cols + 1);
                            if(isValid)
                            {
                                isAlive = CheckRowForSamOnRight(matrix, isAlive, rows, cols + 1);
                                if (!isAlive) { break; }
                                matrix[rows][cols] = '.';
                                matrix[rows][cols + 1] = 'b';
                                isSet = true;
                            }
                            else
                            {
                                matrix[rows][cols] = 'd';
                                isAlive = CheckRowForSamOnLeft(matrix, isAlive, rows, cols);
                                isSet = true;
                            }
                            break;
                        }
                        else if (matrix[rows][cols] == 'd' && isAlive && !isNikoladzeDead) 
                        {
                            bool isValid = CheckIndex(matrix, rows, cols -1);
                            if (isValid)
                            {
                                isAlive = CheckRowForSamOnLeft(matrix, isAlive, rows, cols - 1);
                                if (!isAlive) { break; }
                                matrix[rows][cols] = '.';
                                matrix[rows][cols -1] = 'd';

                                isSet = true;
                            }
                            else
                            {
                                matrix[rows][cols] = 'b';
                                isAlive = CheckRowForSamOnRight(matrix, isAlive, rows, cols);
                                if (!isAlive) { break; }
                                isSet = true;
                            }
                            break;
                        }
                    }
                    if (!isAlive && isNikoladzeDead && isSet) { break; };
                }

                char currentDirection = direction[i];

                if (isAlive)
                {
                    int[] positionOfSam = new int[2];
                    positionOfSam = GetPosition(matrix, positionOfSam);
                    int row = positionOfSam[0];
                    int col = positionOfSam[1];

                    matrix[row][col] = '.';

                    if (currentDirection == 'U')
                    {
                        matrix[row - 1][col] = 'S';
                        isNikoladzeDead = CheckForNikoladze(matrix, row - 1);
                    }
                    else if (currentDirection == 'D')
                    {
                        matrix[row + 1][col] = 'S';
                        isNikoladzeDead = CheckForNikoladze(matrix, row + 1);
                    }
                    else if (currentDirection == 'L')
                    {
                        matrix[row][col - 1] = 'S';
                    }
                    else if (currentDirection == 'R')
                    {
                        matrix[row][col + 1] = 'S';
                    }
                    else if (currentDirection == 'W')
                    {
                        matrix[row][col] = 'S';
                    }

                    
                }
                if (isNikoladzeDead || !isAlive)
                {
                    Console.WriteLine($"{(isNikoladzeDead ? "Nikoladze killed!" : $"Sam died at {deadLocation[0]}, {deadLocation[1]}")}");
                    PrintMatrix(matrix);
                    break;
                }
            }

            

            
        }

        private static bool CheckForNikoladze(char[][] matrix, int row)
        {
            bool isNikolidjaDead = false;
            for (int rows = row; rows < row + 1; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if (matrix[rows][cols] == 'N')
                    {
                        matrix[rows][cols] = 'X';
                        isNikolidjaDead = true;
                        break;
                    }
                }
                if (isNikolidjaDead)
                {
                    break;
                }
            }
            return isNikolidjaDead;
        }

        private static bool CheckRowForSamOnRight(char[][] matrix, bool isAlive, int rows, int cols)
        {
            for (int x = cols; x < matrix[rows].Length; x++)
            {
                if (matrix[rows][x] == 'S')
                {
                    matrix[rows][x] = 'X';
                    isAlive = false;
                    break;
                }
            }

            return isAlive;
        }

        private static bool CheckRowForSamOnLeft(char[][] matrix, bool isAlive, int rows, int cols)
        {
            for (int x = cols; x >= 0; x--)
            {
                if (matrix[rows][x] == 'S')
                {
                    matrix[rows][x] = 'X';
                    isAlive = false;
                    break;
                }
            }

            return isAlive;
        }

        public static int[] GetPosition(char[][] matrix, int[] position)
        {
            bool isFound = false;
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if (matrix[rows][cols] == 'S')
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
        public static void FillMatrix(char[][] matrix)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                string currentLine = Console.ReadLine();
                char[] characters = currentLine.ToCharArray();
                matrix[rows] = characters;
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
    }
}