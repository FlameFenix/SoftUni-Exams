using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;
        private ICar car;
        private int numberOfWins;
        private bool canParticipate = false;
        private List<ICar> cars;

        public Driver(string name)
        {
            this.Name = name;
            cars = new List<ICar>();
        }
        public string Name 
        { 
            get => this.name;
            private set
            {
                if(string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));
                }
                this.name = value;
            }
        }
            

        public ICar Car
        {
            get => this.car;
            private set
            {
                this.car = value;
            }
        }

        public int NumberOfWins
        {
            get => this.numberOfWins;
            private set
            {
                this.numberOfWins = value;
            }
        }

        public bool CanParticipate
        {
            get => this.canParticipate;
            private set
            {
                this.canParticipate = value;
            }
        }

        public void AddCar(ICar car)
        {
            if(car == null)
            {
                throw new ArgumentNullException(ExceptionMessages.CarInvalid);
            }

            cars.Add(car);
            this.Car = car;
            canParticipate = true;
        }

        public void WinRace()
        {
            NumberOfWins++;
        }
    }
}
