using System;
using System.Collections.Generic;

namespace Exam.MoovIt
{
    class Program
    {
        static void Main(string[] args)
        {
            MoovIt moovIt = new MoovIt();

            Route route = new Route("Test1", 10D, 1, false, new List<string>(new string[] { "Sofia", "Plovdiv", "Stara Zagora", "Burgas" }));
            Route route2 = new Route("Test2", 10D, 1, false, new List<string>(new string[] { "Sofia", "Pleven", "Veliko Turnovo", "Varna", "Burgas" }));

            moovIt.AddRoute(route);

            Console.WriteLine(moovIt.Contains(route2));
        }
    }
}