using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Repositories.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Repositories
{
    public class AquariumRepository : IRepository<IAquarium>
    {
        private List<IAquarium> models;

        public AquariumRepository()
        {
            models = new List<IAquarium>();
        }

        public IReadOnlyCollection<IAquarium> Models => this.models.ToList().AsReadOnly();

        public void Add(IAquarium model)
        {
            if(model == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
            models.Add(model);
        }

        public IAquarium FindByType(string type)
        {
            IAquarium aqua = models.FirstOrDefault(x => x.Name == type);

            if(aqua == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            return aqua;
        }

        public bool Remove(IAquarium model)
        {
            IAquarium aqua = models.FirstOrDefault(x => x == model);

            if(aqua == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            return models.Remove(aqua);
        }
    }
}
