using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = string.Empty;

            List<guestAndMeal> guestsList = new List<guestAndMeal>();
            List<string> unlikedMeals = new List<string>();

            while ((command = Console.ReadLine()) != "Stop")
            {
                
                string[] cmdArgs = command.Split("-", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string option = cmdArgs[0];
                string guestName = cmdArgs[1];
                string mealName = cmdArgs[2];

                guestAndMeal currentGuest = new guestAndMeal();
                

                guestAndMeal isGuestExist = guestsList.FirstOrDefault(x => x.GuestName == guestName);

                if(option == "Like")
                {
                    if(guestsList.Contains(isGuestExist))
                    {
                        bool isMealExist = false;
                        foreach (var item in guestsList)
                        {
                            if(item.GuestName == guestName)
                            {
                                if (item.LikedMeal.Contains(mealName))
                                {
                                    isMealExist = true;
                                }
                            }   
                        }
                        if(!isMealExist)
                        {
                            isGuestExist.LikedMeal.Add(mealName);
                        }
                        
                    }
                    else
                    {
                        currentGuest.GuestName = guestName;
                        currentGuest.LikedMeal = new List<string> { mealName };

                        guestsList.Add(currentGuest);
                    }
                    
                }
                else if(option == "Unlike")
                {
                    if(guestsList.Contains(isGuestExist))
                    {
                        guestAndMeal isMealExist = guestsList.FirstOrDefault(x => x.LikedMeal.Contains(mealName));

                        bool isMealWasRemoved = false;

                        foreach (var item in guestsList)
                        {
                            if(item.GuestName == guestName)
                            {
                                if(item.LikedMeal.Contains(mealName))
                                {
                                    Console.WriteLine($"{guestName} doesn't like the {mealName}.");
                                    item.LikedMeal.Remove(mealName);
                                    unlikedMeals.Add(mealName);
                                    isMealWasRemoved = true;
                                    break;
                                }
                            }
                        }
                        if(!isMealWasRemoved)
                        {
                            Console.WriteLine($"{guestName} doesn't have the {mealName} in his/her collection.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{guestName} is not at the party.");
                    }
                }
            }

            foreach (var item in guestsList.OrderByDescending(x => x.LikedMeal.Count).ThenBy(x => x.GuestName))
            {
                Console.WriteLine($"{item.GuestName}: {string.Join(", ", item.LikedMeal)}");
            }
            Console.WriteLine($"Unliked meals: {unlikedMeals.Count}");
        }
    }
    class guestAndMeal
    {
        public string GuestName { get; set; }
        public List<string> LikedMeal { get; set; }
    }
}
