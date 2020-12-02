using System;

namespace CookingMasterclass
{
    class Program
    {
        static void Main(string[] args)
        {
            double budget = double.Parse(Console.ReadLine());
            int students = int.Parse(Console.ReadLine());
            double flourPerPackage = double.Parse(Console.ReadLine());
            double eggPerOne = double.Parse(Console.ReadLine());
            double apronPerOne = double.Parse(Console.ReadLine());



            // Because the aprons get dirty often, George should buy 20% more, rounded up to the next integer. 
            double moreApron = Math.Ceiling((double)students * 0.2);
            // Also, every fifth package of flour is free. 
            double freePackages = students / 5;

            // Set per One Student =>
            // package flour
            // 10 eggs
            // apron

            double finalSum = apronPerOne * (students + moreApron) + (eggPerOne * 10 * (students)) + (flourPerPackage * (students - freePackages));

            if (budget >= finalSum)
            {

                Console.WriteLine($"Items purchased for {finalSum:f2}$.");
            }
            else
            {
                finalSum -= budget;
                Console.WriteLine($"{finalSum:f2}$ more needed.");
            }
        }
    }
}
