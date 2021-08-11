using System;

namespace Bonus_Scoring_System
{
    class Program
    {
        static void Main(string[] args)
        {
            int countOfStudents = int.Parse(Console.ReadLine());
            int countOfLectures = int.Parse(Console.ReadLine());
            int additionalBonus = int.Parse(Console.ReadLine());

            double bestBonus = 0;
            double bestStudent = 0;
            for (int i = 0; i < countOfStudents; i++)
            {
                int studentAttendance = int.Parse(Console.ReadLine());
                double bonus = 0;

                bonus = (double)studentAttendance / countOfLectures * (5 + additionalBonus);

                if (bonus > bestBonus)
                {
                    bestBonus = bonus;
                    bestStudent = studentAttendance;
                }
            }

            Console.WriteLine($"Max Bonus: {Math.Round(bestBonus)}.");
            Console.WriteLine($"The student has attended {bestStudent} lectures.");
        }
    }
}
