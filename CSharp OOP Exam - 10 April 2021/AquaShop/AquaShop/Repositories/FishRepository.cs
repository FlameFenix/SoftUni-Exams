using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Repositories
{
    public class FishRepository : IRepository<IFish>
    {
        private List<IFish> models;

        public FishRepository()
        {
            models = new List<IFish>();
        }

        public IReadOnlyCollection<IFish> Models => models.ToList().AsReadOnly();

        public void Add(IFish model)
        {
            models.Add(model);
        }

        public IFish FindByType(string type)
        {
            return models.FirstOrDefault(x => x.GetType().Name == type);
        }

        public bool Remove(IFish model)
        {
           return models.Remove(model);
        }
    }
}
