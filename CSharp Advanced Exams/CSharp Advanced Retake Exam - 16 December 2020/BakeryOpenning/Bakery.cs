using System;
using System.Collections.Generic;
using System.Text;

namespace BakeryOpenning
{
    public class Bakery
    {
        public Bakery(string name, int capacity)
        {
            Employees = new List<Employee>();
            Name = name;
            Capacity = capacity;
        }

        private List<Employee> Employees { get; set; }



        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Count
        {
            get
            {
                return Employees.Count;
            }

        }

        public string Report()
        {
            string example = $"Employees working at Bakery {Name}:" + Environment.NewLine;

            foreach (var item in Employees)
            {
                example += item.ToString() + Environment.NewLine;
            }

            return example;
        }

        public Employee GetEmployee(string name)
        {
            Employee isEmployeeExist = Employees.Find(x => x.Name == name);

            return isEmployeeExist;
        }

        public void GetOldestEmployee()
        {
            int maxAge = 0;
            string oldestName = string.Empty;

            foreach (var item in Employees)
            {
                if (item.Age > maxAge)
                {
                    maxAge = item.Age;
                    oldestName = item.Name;
                }
            }
            Console.WriteLine($"Employee with name {oldestName}");
        }

        public void Add(Employee employee)
        {
            Employees.Add(employee);
        }

        public bool Remove(string name)
        {
            Employee toRemove = Employees.Find(x => x.Name == name);

            if (Employees.Contains(toRemove))
            {
                Employees.Remove(toRemove);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
