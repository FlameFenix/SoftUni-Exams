using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            if(bunny.Energy > 0)
            {
                bool isThereADyes = bunny.Dyes.Any(x => x.Power > 0);
                bool isEggFinished = false;

                if(isThereADyes)
                {
                    foreach (var item in bunny.Dyes.Where(x => x.Power > 0))
                    {
                        while (item.Power > 0 && !isEggFinished)
                        {
                            item.Use();
                            bunny.Work();
                            egg.GetColored();
                            isEggFinished = egg.IsDone();
                            if (isEggFinished)
                            {
                                break;
                            }
                        }
                        if(isEggFinished)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
