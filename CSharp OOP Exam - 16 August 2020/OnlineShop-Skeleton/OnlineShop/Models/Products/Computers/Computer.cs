using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products
{
    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance { get => base.OverallPerformance + components.Average(x => x.OverallPerformance); protected set => base.OverallPerformance = value; }
        public override decimal Price { get => base.Price + components.Sum(x => x.Price) + peripherals.Sum(x => x.Price); protected set => base.Price = value; }
        public IReadOnlyCollection<IComponent> Components
        {
            get => this.components;
        }

        public IReadOnlyCollection<IPeripheral> Peripherals
        {
            get => this.peripherals;
        }

        public void AddComponent(IComponent component)
        {
            if(components.Contains(component))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, Manufacturer, GetType().Name, Id));
            }
            components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Contains(peripheral))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, GetType().Name, Id));
            }
            peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            IComponent component = components.Find(x => x.GetType().Name == componentType);

            if(components.Count == 0 || component == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, GetType().Name, Id));
            }
            components.Remove(component);
            return component;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            IPeripheral peripheral = peripherals.Find(x => x.GetType().Name == peripheralType);

            if (peripherals.Count == 0 || peripheral == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, GetType().Name, Id));
            }
            peripherals.Remove(peripheral);
            return peripheral;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{string.Format(SuccessMessages.ProductToString,OverallPerformance, Price, GetType().Name, Manufacturer, Model, Id)}");
            sb.AppendLine($" {string.Format(SuccessMessages.ComputerComponentsToString, components.Count)}");
            

            foreach (var item in components)
            {
                sb.AppendLine($"  {item}");
            }

            sb.AppendLine($" {string.Format(SuccessMessages.ComputerPeripheralsToString, peripherals.Count, (peripherals.Count != 0 ? peripherals.Average(x => x.OverallPerformance) : 0))}");
            
            foreach (var item in peripherals)
            {
                sb.AppendLine($"  {item}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
