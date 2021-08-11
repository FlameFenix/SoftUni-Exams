using System;
using System.Collections.Generic;
using System.Linq;

namespace Bee
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[][] matrix = new char[n][];

            FillMatrix(n, matrix);

            int[] beePosition = new int[2];

            GetBeePosition(matrix, beePosition);

            int polination = 0;

            string direction = Console.ReadLine();

            bool getLost = false;

            while (direction != "End")
            {
                

                bool isBonus = false;

                if(direction == "End")
                {
                    break;
                }

                int rowsPosition = beePosition[0];
                int colsPosition = beePosition[1];

                if (direction == "right")
                {
                    if(colsPosition + 1 >= 0 && colsPosition + 1 < matrix[rowsPosition].Length)
                    {
                        char newPosition = matrix[rowsPosition][colsPosition + 1];

                        if (newPosition == 'f')
                        {
                            polination++;
                        }
                        else if (newPosition == 'O')
                        {
                            isBonus = true;
                        }

                        matrix[rowsPosition][colsPosition + 1] = 'B';
                        matrix[rowsPosition][colsPosition] = '.';
                        beePosition[0] = rowsPosition;
                        beePosition[1] = colsPosition + 1;
                        rowsPosition = beePosition[0];
                        colsPosition = beePosition[1];

                        if (isBonus)
                        {
                            if (colsPosition + 1 >= 0 && colsPosition + 1 < matrix[rowsPosition].Length)
                            {
                                newPosition = matrix[rowsPosition][colsPosition + 1];

                                if (newPosition == 'f')
                                {
                                    polination++;
                                }

                                matrix[rowsPosition][colsPosition + 1] = 'B';
                                matrix[rowsPosition][colsPosition] = '.';
                                beePosition[0] = rowsPosition;
                                beePosition[1] = colsPosition + 1;

                            }
                        }

                    }
                    else
                    {
                        matrix[rowsPosition][colsPosition] = '.';
                        getLost = true;
                        break;
                    }
                }
                else if (direction == "left")
                {
                    if (colsPosition - 1 >= 0 && colsPosition - 1 < matrix[rowsPosition].Length)
                    {
                        char newPosition = matrix[rowsPosition][colsPosition - 1];

                        if (newPosition == 'f')
                        {
                            polination++;
                        }
                        else if (newPosition == 'O')
                        {
                            isBonus = true;
                        }

                        matrix[rowsPosition][colsPosition - 1] = 'B';
                        matrix[rowsPosition][colsPosition] = '.';
                        beePosition[0] = rowsPosition;
                        beePosition[1] = colsPosition - 1;
                        rowsPosition = beePosition[0];
                        colsPosition = beePosition[1];

                        if (isBonus)
                        {
                            if (colsPosition - 1 >= 0 && colsPosition - 1 < matrix[rowsPosition].Length)
                            {
                                newPosition = matrix[rowsPosition][colsPosition - 1];

                                if (newPosition == 'f')
                                {
                                    polination++;
                                }

                                matrix[rowsPosition][colsPosition - 1] = 'B';
                                matrix[rowsPosition][colsPosition] = '.';
                                beePosition[0] = rowsPosition;
                                beePosition[1] = colsPosition - 1;

                            }
                        }
                    }
                    else
                    {
                        matrix[rowsPosition][colsPosition] = '.';
                        getLost = true;
                        break;
                    }
                }
                else if (direction == "up")
                {
                    

                    if (rowsPosition - 1 >= 0 && rowsPosition - 1 < matrix.Length)
                    {
                        char newPosition = matrix[rowsPosition - 1][colsPosition];

                        if (newPosition == 'f')
                        {
                            polination++;
                        }
                        else if (newPosition == 'O')
                        {
                            isBonus = true;
                        }

                        matrix[rowsPosition - 1][colsPosition] = 'B';
                        matrix[rowsPosition][colsPosition] = '.';
                        beePosition[0] = rowsPosition - 1;
                        beePosition[1] = colsPosition;
                        rowsPosition = beePosition[0];
                        colsPosition = beePosition[1];

                        if (isBonus)
                        {
                            if (rowsPosition - 1 >= 0 && rowsPosition - 1 < matrix.Length)
                            {
                                newPosition = matrix[rowsPosition - 1][colsPosition];

                                if (newPosition == 'f')
                                {
                                    polination++;
                                }

                                matrix[rowsPosition - 1][colsPosition] = 'B';
                                matrix[rowsPosition][colsPosition] = '.';
                                beePosition[0] = rowsPosition - 1;
                                beePosition[1] = colsPosition;

                            }
                        }
                    }
                    else
                    {
                        matrix[rowsPosition][colsPosition] = '.';
                        getLost = true;
                        break;
                    }
                }
                else if (direction == "down")
                {
                    if (rowsPosition + 1 >= 0 && rowsPosition + 1 < matrix.Length)
                    {
                        char newPosition = matrix[rowsPosition + 1][colsPosition];

                        if (newPosition == 'f')
                        {
                            polination++;
                        }
                        else if (newPosition == 'O')
                        {
                            isBonus = true;
                        }

                        matrix[rowsPosition + 1][colsPosition] = 'B';
                        matrix[rowsPosition][colsPosition] = '.';
                        beePosition[0] = rowsPosition + 1;
                        beePosition[1] = colsPosition;
                        rowsPosition = beePosition[0];
                        colsPosition = beePosition[1];

                        if (isBonus)
                        {
                            if (rowsPosition + 1 >= 0 && rowsPosition + 1 < matrix.Length)
                            {
                                newPosition = matrix[rowsPosition + 1][colsPosition];

                                if (newPosition == 'f')
                                {
                                    polination++;
                                }

                                matrix[rowsPosition + 1][colsPosition] = 'B';
                                matrix[rowsPosition][colsPosition] = '.';
                                beePosition[0] = rowsPosition + 1;
                                beePosition[1] = colsPosition;

                            }
                        }
                    }
                    else
                    {
                        matrix[rowsPosition][colsPosition] = '.';
                        getLost = true;
                        break;
                    }
                }

                direction = Console.ReadLine();

            }

            if (getLost)
            {
                Console.WriteLine("The bee got lost!");
            }

            if (polination >= 5)
            {
                Console.WriteLine($"Great job, the bee managed to pollinate {polination} flowers!");
            }
            else
            {
                Console.WriteLine($"The bee couldn't pollinate the flowers, she needed {5 - polination} flowers more");
            }

            PrintMatrix(matrix);
        }

        private static void GetBeePosition(char[][] matrix, int[] beePosition)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if (matrix[rows][cols] == 'B')
                    {
                        beePosition[0] = rows;
                        beePosition[1] = cols;
                    }
                }
            }
        }

        private static void PrintMatrix(char[][] matrix)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    Console.Write(matrix[rows][cols]);
                }
                Console.WriteLine();
            }
        }

        private static void FillMatrix(int n, char[][] matrix)
        {
            for (int rows = 0; rows < n; rows++)
            {
                string currentLine = Console.ReadLine();

                char[] chars = currentLine.ToCharArray();

                for (int cols = 0; cols < 1; cols++)
                {
                    matrix[rows] = chars;
                }
            }
        }
    }
}
