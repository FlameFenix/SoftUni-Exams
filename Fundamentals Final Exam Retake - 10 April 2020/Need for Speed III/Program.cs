using System;
using System.Collections.Generic;
using System.Linq;

namespace Need_for_Speed_III
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfCars = int.Parse(Console.ReadLine());

            List<Cars> cars = new List<Cars>();

            for (int i = 0; i < numberOfCars; i++)
            {
                string[] cmdArgs = Console.ReadLine().Split("|").ToArray();
                string type = cmdArgs[0];
                int mileage = int.Parse(cmdArgs[1]);
                int fuel = int.Parse(cmdArgs[2]);

                Cars currentCar = new Cars(type, mileage, fuel);

                cars.Add(currentCar);
            }

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Stop")
            {
                string[] cmdArgs = command.Split(" : ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                string option = cmdArgs[0];

                string car = cmdArgs[1];

                Cars currentCar = cars.FirstOrDefault(x => x.BrandAndModel == car);

                if (option == "Drive")
                {
                    int miles = int.Parse(cmdArgs[2]);
                    int fuel = int.Parse(cmdArgs[3]);

                    if (cars.Contains(currentCar));
                    {
                        if (currentCar.Fuel >= fuel)
                        {
                            currentCar.Fuel -= fuel;
                            currentCar.Mileage += miles;

                            Console.WriteLine($"{car} driven for {miles} kilometers. {fuel} liters of fuel consumed.");

                            if (currentCar.Mileage >= 100000)
                            {
                                Console.WriteLine($"Time to sell the {car}!");
                                cars.Remove(currentCar);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not enough fuel to make that ride");
                        }
                    }
                }
                else if (option == "Refuel")
                {
                    int fuel = int.Parse(cmdArgs[2]);

                    if (cars.Contains(currentCar))
                        { 
                        if (currentCar.Fuel + fuel <= 75)
                        {
                            currentCar.Fuel += fuel;

                        }
                        else
                        {
                            fuel = 75 - currentCar.Fuel;
                            currentCar.Fuel = 75;
                        }
                        Console.WriteLine($"{car} refueled with {fuel} liters");
                    }
                }
                else if (option == "Revert")
                {
                    int miles = int.Parse(cmdArgs[2]);

                    if (cars.Contains(currentCar))
                        {
                        if (currentCar.Mileage - miles < 10000)
                        {
                            currentCar.Mileage = 10000;
                        }
                        else
                        {
                            currentCar.Mileage -= miles;
                        }
                        Console.WriteLine($"{car} mileage decreased by {miles} kilometers");
                    }
                }

            }

            foreach (var item in cars.OrderByDescending(x => x.Mileage).ThenBy(x => x.BrandAndModel))
            {
                    Console.WriteLine(item.ToString());
            }
        }
    }

    class Cars
    {
        public Cars(string type, int miles, int fuel)
        {
            BrandAndModel = type;
            Mileage = miles;
            Fuel = fuel;
        }
        public void Delete(string somestring)
        {
            this.BrandAndModel = null;
        }
        public string BrandAndModel { get; set; }
        public int Mileage { get; set; }

        public int Fuel { get; set; }

        public override string ToString()
        {
            return $"{BrandAndModel} -> Mileage: {Mileage} kms, Fuel in the tank: {Fuel} lt.";
        }
    }
}
