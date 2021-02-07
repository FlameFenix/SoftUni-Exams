namespace BakeryOpenning
{
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Bakery bakery = new Bakery("Pri Stancho", 20);

            Employee employee = new Employee("Tanq", 15, "Bulgaria");

            Employee second = new Employee("Mitko", 13, "Bulgaria");

            Employee third = new Employee("Pesho", 33, "France");

            bakery.Add(employee);

            bakery.Add(second);

            bakery.Add(third);

            bakery.Remove("Pesho");

            Console.WriteLine(employee);

            Console.WriteLine(bakery.Count);

            bakery.GetOldestEmployee();

            Console.WriteLine(bakery.Report());

        }
    }
}
