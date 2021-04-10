using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using AquaShop.Utilities.Messages;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Models.Fish;
using System.Linq;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private FishRepository fishes;
        private AquariumRepository aquarium;
        private DecorationRepository decorations;

        public Controller()
        {
            fishes = new FishRepository();
            aquarium = new AquariumRepository();
            decorations = new DecorationRepository();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aqua = null;

            if(aquariumType is nameof(FreshwaterAquarium))
            {
                aqua = new FreshwaterAquarium(aquariumName);
            }
            else if(aquariumType is nameof(SaltwaterAquarium))
            {
                aqua = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            aquarium.Add(aqua);

            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        private object FreshWater()
        {
            throw new NotImplementedException();
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = null;

            if (decorationType is nameof(Ornament))
            {
                decoration = new Ornament();
            }
            else if (decorationType is nameof(Plant))
            {
                decoration = new Plant();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            decorations.Add(decoration);

            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish = null;

            if (fishType is nameof(FreshwaterFish))
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType is nameof(SaltwaterFish))
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IAquarium aqua = aquarium.FindByType(aquariumName);

            if(aqua.GetType().Name == nameof(FreshwaterAquarium) && fishType == nameof(FreshwaterFish))
            {
                aqua.AddFish(fish);
            }
            else if(aqua.GetType().Name == nameof(SaltwaterAquarium) && fishType == nameof(SaltwaterFish))
            {
                aqua.AddFish(fish);
            }
            else
            {
                return string.Format(OutputMessages.UnsuitableWater);
            }

            return string.Format(OutputMessages.EntityAddedToAquarium, fish.GetType().Name, aqua.Name);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aqua = aquarium.FindByType(aquariumName);

            decimal value = 0;

            if (aqua != null)
            {
                value = aqua.Fish.Sum(x => x.Price) + aqua.Decorations.Sum(x => x.Price);
            }


            return $"The value of Aquarium {aquariumName} is {value}.";
        }

        public string FeedFish(string aquariumName)
        {
           IAquarium aqua =  aquarium.FindByType(aquariumName);

            if(aqua != null)
            {
                aqua.Feed();
            }

            return string.Format(OutputMessages.FishFed, aqua.Fish.Count > 0 ? aqua.Fish.Count : 0);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = decorations.FindByType(decorationType);

            if(decoration == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }

            IAquarium aqua = aquarium.FindByType(aquariumName);

            if(aqua == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            aqua.AddDecoration(decoration);

            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in aquarium.Models)
            {
                sb.AppendLine(item.GetInfo());
            }

            return sb.ToString().Trim();
        }
    }
}
