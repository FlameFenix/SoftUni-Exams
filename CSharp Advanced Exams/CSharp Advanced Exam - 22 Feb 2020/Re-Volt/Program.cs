using System;

namespace Re_Volt
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int countCommands = int.Parse(Console.ReadLine());

            char[][] matrix = new char[n][];

            int[] position = new int[2];

            FillMatrix(matrix, position);

            bool isWin = false;

            for (int i = 0; i < countCommands; i++)
            {
                int rows = position[0];
                int cols = position[1];

                int oldRows = position[0];
                int oldCols = position[1];

                string direction = Console.ReadLine();

                Move(position, direction);

                IsIndexValid(matrix, position);

                rows = position[0];
                cols = position[1];

                if (!isWin)
                {
                    matrix[oldRows][oldCols] = '-';

                    char currentChar = matrix[rows][cols];

                    if (currentChar == '-')
                    {
                        matrix[rows][cols] = 'f';
                    }
                    else if (currentChar == 'B')
                    {
                        Move(position, direction);
                        IsIndexValid(matrix, position);
                        rows = position[0];
                        cols = position[1];

                        if(matrix[rows][cols] == '-')
                        {
                            matrix[rows][cols] = 'f';
                        }
                        else if(matrix[rows][cols] == 'F')
                        {
                            isWin = true;
                            matrix[rows][cols] = 'f';
                            break;
                        }
                    }
                    else if(currentChar == 'T')
                    {
                        if(direction == "right")
                        {
                            position[1] -= 1;
                        }
                        else if(direction == "left")
                        {
                            position[1] += 1;
                        }
                        else if(direction == "up")
                        {
                            position[0] += 1;
                        }
                        else if(direction == "down")
                        {
                            position[0] -= 1;
                        }

                        rows = position[0];
                        cols = position[1];

                        if (matrix[rows][cols] == '-')
                        {
                            matrix[rows][cols] = 'f';
                        }
                        else if (matrix[rows][cols] == 'F')
                        {
                            isWin = true;
                            matrix[rows][cols] = 'f';
                            break;
                        }
                    }
                    else if(currentChar == 'F')
                    {
                        isWin = true;
                        matrix[rows][cols] = 'f';
                        break;
                    }
                }
            }

            if (isWin)
            { 
                Console.WriteLine("Player won!");
            }
            else
            {
                Console.WriteLine("Player lost!");
            }

            PrintMatrix(matrix);
        }

        private static void Move(int[] position, string direction)
        {
            if (direction == "left")
            {
                position[1] -= 1;
            }
            else if (direction == "right")
            {
                position[1] += 1;
            }
            else if (direction == "up")
            {
                position[0] -= 1;
            }
            else if (direction == "down")
            {
                position[0] += 1;
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

        private static void FillMatrix(char[][] matrix, int[] position)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                string currentLine = Console.ReadLine();
                char[] charArray = currentLine.ToCharArray();
                matrix[rows] = charArray;

                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if (matrix[rows][cols] == 'f')
                    {
                        position[0] = rows;
                        position[1] = cols;
                    }
                }
            }
        }

        public static int[] IsIndexValid(char[][] matrix, int[] currentPosition)
        {
            int rows = currentPosition[0];
            int cols = currentPosition[1];

            if(rows >= 0 && rows < matrix.Length
                && cols >= 0 && cols < matrix[rows].Length)
            {

            }
            else if(rows < 0 || cols < 0)
            {
                if(rows < 0)
                {
                    rows = matrix.Length - 1;
                }
                if(cols < 0)
                {
                    cols = matrix[rows].Length - 1;
                }
            }
            else if(rows >= matrix.Length || cols >= matrix[rows].Length)
            {
                if (rows >= matrix.Length)
                {
                    rows = 0;
                }
                if(cols >= matrix[rows].Length)
                {
                    cols = 0;
                }
            }

            currentPosition[0] = rows;
            currentPosition[1] = cols;

            return currentPosition;
        }
    }
}
