using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> cars;

        public CarRepository()
        {
            cars = new List<ICar>();
        }
        public void Add(ICar model)
        {
            cars.Add(model);
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
           return cars;
        }

        public ICar GetByName(string name)
        {
            ICar car = cars.Find(x => x.Model == name);
            return car;
        }

        public bool Remove(ICar model)
        {
            return cars.Remove(model);
        }
    }
}
