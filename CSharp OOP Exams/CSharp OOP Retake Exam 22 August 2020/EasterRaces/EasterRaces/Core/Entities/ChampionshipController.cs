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

            ICar car = cars.GetByName(carModel);
            if (car == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound , carModel));
            }

            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            driver.AddCar(car);
            return $"{string.Format(OutputMessages.CarAdded, driverName, carModel)}";
            
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = races.GetByName(raceName);
            IDriver driver = drivers.GetByName(driverName);

            if(race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            if(driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            race.AddDriver(driver);
            return $"{string.Format(OutputMessages.DriverAdded, driverName, raceName)}";
            
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if ("Muscle" == type)
            {
                MuscleCar car = new MuscleCar(model, horsePower);
                if (cars.GetByName(model) != null)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
                }
                else
                {
                    cars.Add(car);
                    return $"{string.Format(OutputMessages.CarCreated, car.GetType().Name, model)}";


                }

            }
            else
            {
                SportsCar car = new SportsCar(model, horsePower);

                if (cars.GetByName(model) != null)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
                }
                else
                {
                    cars.Add(car);
                    return $"{string.Format(OutputMessages.CarCreated, car.GetType().Name, model)}";
                    
                }
            }
        }

        public string CreateDriver(string driverName)
        {
            Driver driver = new Driver(driverName);

            if (drivers.GetByName(driverName) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            else
            {
                drivers.Add(driver);
                return $"{string.Format(OutputMessages.DriverCreated, driverName)}";
            }

        }

        public string CreateRace(string name, int laps)
        {
            IRace race = new Race(name, laps);
            if (races.GetByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }
            else
            {
                races.Add(race);
                return $"{string.Format(OutputMessages.RaceCreated, name)}";
            }
        }

        public string StartRace(string raceName)
        {
            IRace race = races.GetByName(raceName);
            if (race != null)
            {
                if (race.Drivers.Count < 3)
                {
                    throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));
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

                        if (count == 1)
                        {
                            output += $"{string.Format(OutputMessages.DriverFirstPosition, item.Key.Name, race.Name)}" + Environment.NewLine;
                            
                        }
                        else if (count == 2)
                        {
                            output += $"{string.Format(OutputMessages.DriverSecondPosition, item.Key.Name, raceName)}" + Environment.NewLine;
                            
                        }
                        else if (count == 3)
                        {
                            output += $"{string.Format(OutputMessages.DriverThirdPosition, item.Key.Name, raceName)}";
                            
                        }
                    }

                    races.Remove(race);

                    return output;


                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
        }
    }
}
