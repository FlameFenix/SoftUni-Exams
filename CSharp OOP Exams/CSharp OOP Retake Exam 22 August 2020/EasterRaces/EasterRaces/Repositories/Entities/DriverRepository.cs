using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        List<IDriver> drivers;

        public DriverRepository()
        {
            drivers = new List<IDriver>();
        }

        public void Add(IDriver model)
        {
            drivers.Add(model);
        }

        public IReadOnlyCollection<IDriver> GetAll()
        {
            return drivers;
        }

        public IDriver GetByName(string name)
        {
            IDriver driver = drivers.Find(x => x.Name == name);
            return driver;
        }

        public bool Remove(IDriver model)
        {
           return drivers.Remove(model);
        }
    }
}
