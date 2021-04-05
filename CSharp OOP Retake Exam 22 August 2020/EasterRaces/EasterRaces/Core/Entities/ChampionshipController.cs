using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private DriverRepository drivers;
        private RaceRepository races;
        private CarRepository cars;
        public ChampionshipController()
        {
            drivers = new DriverRepository();
            races = new RaceRepository();
            cars = new CarRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = drivers.GetByName(driverName);
            if(driver != null)
            {
                ICar car = cars.GetByName(carModel);
                if(car != null)
                {
                    driver.AddCar(car);
                    return $"Driver {driverName} received car {carModel}.";
                }
                else
                {
                    throw new InvalidOperationException(ExceptionMessages.CarNotFound);
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.DriverNotFound);
            }
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = races.GetByName(raceName);

            if(race != null)
            {
                IDriver driver = drivers.GetByName(driverName);
                if(driver != null)
                {
                    race.AddDriver(driver);
                    return $"Driver {driverName} added in {raceName} race.";
                }
                else
                {
                    throw new InvalidOperationException(ExceptionMessages.DriverNotFound);
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.RaceNotFound);
            }
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if ("Muscle" == type)
            {
                MuscleCar car = new MuscleCar(model, horsePower);
                if(cars.GetByName(model) != null)
                {
                    throw new ArgumentException(ExceptionMessages.CarExists);
                }
                else
                {
                    cars.Add(car);
                    return $"{car.GetType().Name} {model} is created.";
                }
                
            }
            else
            {
                SportsCar car = new SportsCar(model, horsePower);

                if (cars.GetByName(model) != null)
                {
                    throw new ArgumentException(ExceptionMessages.CarExists);
                }
                else
                {
                    cars.Add(car);
                    return $"{car.GetType().Name} {model} is created.";
                }
            }
        }

        public string CreateDriver(string driverName)
        {
            Driver driver = new Driver(driverName);

            if(drivers.GetByName(driverName) != null)
            {
                throw new ArgumentException(ExceptionMessages.DriversExists);
            }
            else
            {
                drivers.Add(driver);
                return $"Driver {driverName} is created.";
            }
            
        }

        public string CreateRace(string name, int laps)
        {
            IRace race = new Race(name, laps);
            if(races.GetByName(name) != null)
            {
                throw new InvalidOperationException(ExceptionMessages.RaceExists);
            }
            else
            {
                races.Add(race);
                return $"Race {name} is created.";
            }
        }

        public string StartRace(string raceName)
        {
            IRace race = races.GetByName(raceName);
            if (race != null)
            {      
                if(race.Drivers.Count < 3)
                {
                    throw new InvalidOperationException(ExceptionMessages.RaceInvalid);
                }
                else
                {

                    Dictionary<IDriver, double> driversInRace = new Dictionary<IDriver, double>();

                    string output = string.Empty;

                    foreach (var driver in race.Drivers)
                    {
                        driversInRace.Add(driver, driver.Car.CalculateRacePoints(race.Laps));
                    }

                    int count = 0;

                    foreach (var item in driversInRace.OrderByDescending(x => x.Value))
                    {
                        count++;

                        if(count == 1)
                        {
                            output += $"Driver {item.Key.Name} wins {race.Name} race." + Environment.NewLine;
                        }
                        else if(count == 2)
                        {
                            output += $"Driver {item.Key.Name} is second in {raceName} race." + Environment.NewLine;
                        }
                        else if(count == 3)
                        {
                            output += $"Driver {item.Key.Name} is third in {raceName} race.";
                        }
                    }

                    races.Remove(race);

                    return output;
                           
                          
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.RaceNotFound);
            }
        }
    }
}
