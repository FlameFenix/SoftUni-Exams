using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Repositories;
using Easter.Repositories.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        private IRepository<IBunny> bunnies;
        private IRepository<IEgg> eggs;
        private Workshop workshop;
        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
            workshop = new Workshop();
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = null;
            if(bunnyType == nameof(HappyBunny))
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if(bunnyType == nameof(SleepyBunny))
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }
            bunnies.Add(bunny);
            return $"Successfully added {bunnyType} named {bunnyName}.";
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = bunnies.FindByName(bunnyName);
            if(bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            IDye dye = new Dye(power);
            bunny.AddDye(dye);

            return $"Successfully added dye with power {power} to bunny {bunnyName}!";
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);

            eggs.Add(egg);

            return $"Successfully added egg: {eggName}!";
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            bool isEggDone = false;
            if (bunnies.Models.Any(x => x.Energy > 0))
            {
                List<IBunny> orderedBunnies = bunnies.Models.Where(x => x.Energy >= 50).ToList();
                orderedBunnies = orderedBunnies.OrderByDescending(x => x.Energy).ToList();
                
                foreach (var item in orderedBunnies)
                {
                    workshop.Color(egg, item);
                    if(item.Energy == 0)
                    {
                        bunnies.Remove(item);
                    }
                    if(egg.IsDone())
                    {
                        isEggDone = true;
                        break;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            return $"Egg {eggName} is {(isEggDone ? "done" : "not done")}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            List<IEgg> finishedEggs = eggs.Models.Where(x => x.IsDone()).ToList();
            sb.AppendLine($"{finishedEggs.Count} eggs are done!");
            sb.AppendLine("Bunnies info:");
            foreach (var item in bunnies.Models)
            {
                sb.AppendLine($"Name: {item.Name}");
                sb.AppendLine($"Energy: {item.Energy}");
                List<IDye> dyesAboveZero = item.Dyes.Where(x => x.Power > 0).ToList();
                sb.AppendLine($"Dyes: {dyesAboveZero.Count} not finished");
            }
            return sb.ToString().Trim();
        }
    }
}
