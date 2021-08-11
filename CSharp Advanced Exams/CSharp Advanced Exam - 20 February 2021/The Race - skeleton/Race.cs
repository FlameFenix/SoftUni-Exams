using System;
using System.Collections.Generic;
using System.Text;

namespace TheRace
{
    public class Race
    {
        public Race(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            racers = new List<Racer>();
        }

        private List<Racer> racers { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public void Add(Racer racer)
        {
            if (racers.Count < Capacity)
            {
                racers.Add(racer);
            }
        }

        public bool Remove(string name)
        {
            Racer racerToRemove = racers.Find(x => x.Name == name);

            if (racers.Contains(racerToRemove))
            {
                racers.Remove(racerToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Racer GetOldestRacer()
        {
            int maxAge = 0;

            foreach (var item in racers)
            {
                if (item.Age > maxAge)
                {
                    maxAge = item.Age;
                }
            }

            Racer oldestRacer = racers.Find(x => x.Age == maxAge);
            return oldestRacer;
        }

        public Racer GetRacer(string name)
        {
            Racer currentRacer = racers.Find(x => x.Name == name);
            return currentRacer;
        }

        public Racer GetFastestRacer()
        {
            int maxSpeed = 0;

            foreach (var item in racers)
            {
                if (item.Car.Speed > maxSpeed)
                {
                    maxSpeed = item.Car.Speed;
                }
            }

            Racer fastest = racers.Find(x => x.Car.Speed == maxSpeed);
            return fastest;
        }

        public int Count
        {
            get
            {
                return racers.Count;
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Racers participating at {Name}:");

            foreach (var item in racers)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
