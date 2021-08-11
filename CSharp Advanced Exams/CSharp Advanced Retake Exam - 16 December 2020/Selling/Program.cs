using System;
using System.Linq;

namespace Selling
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[][] matrix = new char[n][];

            FillMatrix(matrix);

            string command = Console.ReadLine();

            int sum = 0;

            int[] currentPosition = new int[2];

            CurrentPosition(matrix, currentPosition);

            while (true)
            {

                int rows = currentPosition[0];
                int cols = currentPosition[1];

                if (command == "right")
                {
                    bool isNewIndexValid = cols + 1 < matrix.Length;

                    if(!isNewIndexValid)
                    {
                        matrix[rows][cols] = '-';
                        break;
                    }

                    if (isNewIndexValid)
                    {
                        string nextElement = matrix[rows][cols + 1].ToString();

                        if (nextElement == "-")
                        {
                            matrix[rows][cols + 1] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows;
                            currentPosition[1] = cols + 1;
                        }

                        if (nextElement.All(char.IsDigit))
                        {
                            sum += int.Parse(nextElement);
                            matrix[rows][cols + 1] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows;
                            currentPosition[1] = cols + 1;
                        }
                        else if (nextElement.All(char.IsLetter))
                        {
                            if (nextElement == "O")
                            {
                                matrix[rows][cols + 1] = '-';
                                matrix[rows][cols] = '-';
                                CheckPillars(matrix, currentPosition);
                            }
                        }
                    }
                }
                else if (command == "left")
                {
                    bool isNewIndexValid = cols - 1 >= 0 && cols - 1 < matrix.Length;

                    if(!isNewIndexValid)
                    {
                        matrix[rows][cols] = '-';
                        break;
                    }

                    if (isNewIndexValid)
                    {
                        string nextElement = matrix[rows][cols - 1].ToString();
                        if (nextElement == "-")
                        {
                            matrix[rows][cols - 1] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows;
                            currentPosition[1] = cols - 1;
                        }

                        if (nextElement.All(char.IsDigit))
                        {
                            sum += int.Parse(nextElement);
                            matrix[rows][cols - 1] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows;
                            currentPosition[1] = cols - 1;
                            
                        }
                        else if (nextElement.All(char.IsLetter))
                        {
                            if (nextElement == "O")
                            {
                                matrix[rows][cols - 1] = '-';
                                matrix[rows][cols] = '-';
                                CheckPillars(matrix, currentPosition);
                            }
                        }
                    }
                }
                else if (command == "up")
                {
                    bool isNewIndexValid = rows - 1 >= 0 &&  rows - 1 < matrix[rows].Length;

                    if(!isNewIndexValid)
                    {
                        matrix[rows][cols] = '-';
                        break;
                    }

                    if (isNewIndexValid)
                    {
                        string nextElement = matrix[rows - 1][cols].ToString();

                        if (nextElement == "-")
                        {
                            matrix[rows - 1][cols] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows - 1;
                            currentPosition[1] = cols;
                        }

                        if (nextElement.All(char.IsDigit))
                        {
                            sum += int.Parse(nextElement);
                            matrix[rows - 1][cols] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows - 1;
                            currentPosition[1] = cols;
                        }
                        else if (nextElement.All(char.IsLetter))
                        {
                            if (nextElement == "O")
                            {
                                matrix[rows - 1][cols] = '-';
                                matrix[rows][cols] = '-';
                                CheckPillars(matrix, currentPosition);
                            }
                        }
                    }
                }
                else if (command == "down")
                {
                    bool isNewIndexValid = rows + 1 < matrix[rows].Length;
                    if(!isNewIndexValid)
                    {
                        matrix[rows][cols] = '-';
                        break;
                    }

                    if (isNewIndexValid)
                    {
                        string nextElement = matrix[rows + 1][cols].ToString();

                        if (nextElement == "-")
                        {
                            matrix[rows + 1][cols] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows + 1;
                            currentPosition[1] = cols;
                        }

                        if (nextElement.All(char.IsDigit))
                        {
                            sum += int.Parse(nextElement);
                            matrix[rows + 1][cols] = 'S';
                            matrix[rows][cols] = '-';
                            currentPosition[0] = rows + 1;
                            currentPosition[1] = cols;
                        }
                        else if (nextElement.All(char.IsLetter))
                        {
                            if(nextElement == "O")
                            {
                                matrix[rows + 1][cols] = '-';
                                matrix[rows][cols] = '-';
                                CheckPillars(matrix, currentPosition);
                            }
                        }
                    }
                }

                if (sum >= 50)
                {
                    break;
                }

                command = Console.ReadLine();
                
            }

            
            if(sum >= 50)
            {
                Console.WriteLine("Good news! You succeeded in collecting enough money!");
                Console.WriteLine($"Money: {sum}");
            }
            else
            {
                Console.WriteLine("Bad news, you are out of the bakery.");
                Console.WriteLine($"Money: {sum}");
            }

            PrintMatrix(matrix);

        }

        private static void CheckPillars(char[][] matrix, int[] currentPosition)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                bool isEnd = false;

                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 'O')
                    {
                        matrix[i][j] = 'S';
                        currentPosition[0] = i;
                        currentPosition[1] = j;
                        isEnd = true;
                        break;
                    }
                }
                if (isEnd)
                {
                    break;
                }
            }
        }

        private static void CurrentPosition(char[][] matrix, int[] currentPosition)
        {
            for (int rows = 0; rows < matrix.Length; rows++)
            {
                for (int cols = 0; cols < matrix[rows].Length; cols++)
                {
                    if (matrix[rows][cols] == 'S')
                    {
                        currentPosition[0] = rows;
                        currentPosition[1] = cols;
                        break;
                    }
                }
            }
        }

        private static void FillMatrix(char[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                char[] array = Console.ReadLine().ToCharArray();

                matrix[i] = array;
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
    }
}
