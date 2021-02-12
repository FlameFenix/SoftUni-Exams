using System;
using System.Collections.Generic;
using System.Text;

namespace Parking
{
    public class Parking
    {
        public Parking(string type, int capacity)
        {
            Type = type;
            Capacity = capacity;
            data = new List<Car>();
        }

        private List<Car> data;

        public string Type { get; set; }

        public int Capacity { get; set; }

        public void Add(Car car)
        {
            if (data.Count < Capacity)
            {
                data.Add(car);
            }
        }

        public bool Remove(string manufacturer, string model)
        {
            Car carToRemove = data.Find(x => x.Manufacturer == manufacturer && x.Model == model);

            if (data.Contains(carToRemove))
            {
                data.Remove(carToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Car GetLatestCar()
        {
            int maxYear = 0;

            foreach (var item in data)
            {
                if (item.Year > maxYear)
                { 
                    maxYear = item.Year;
                }
            }

            if(maxYear > 0)
            {
                Car latestCar = data.Find(x => x.Year == maxYear);
                return latestCar;
            }
            else
            {
                return null;
            }
        }

        public Car GetCar(string manufacturer, string model)
        {
            Car currentCar = data.Find(x => x.Manufacturer == manufacturer && x.Model == model);

            if(data.Contains(currentCar))
            {
                return currentCar;
            }
            else
            {
                return null;
            }
        }

        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The cars are parked in {Type}:");

            foreach (var item in data)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
