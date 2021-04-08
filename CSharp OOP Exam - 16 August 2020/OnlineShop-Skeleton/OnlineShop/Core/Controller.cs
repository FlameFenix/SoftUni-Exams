using OnlineShop.Common.Constants;
using OnlineShop.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using IComponent = OnlineShop.Models.Products.Components.IComponent;

namespace OnlineShop.Core
{
    public class Controller : IController
    {

        private List<IComponent> components;
        private List<Computer> computers;
        private List<IPeripheral> peripherals;

        public Controller()
        {
            components = new List<IComponent>();
            computers = new List<Computer>();
            peripherals = new List<IPeripheral>();
        }
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            IComponent component = components.Find(x => x.Id == id);

            Computer computer = computers.Find(x => x.Id == computerId);

            if(computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            if (components.Contains(component))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            if (nameof(CentralProcessingUnit) == componentType)
            {
                component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (nameof(Motherboard) == componentType)
            {
                component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);

            }
            else if (nameof(PowerSupply) == componentType)
            {
                component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);

            }
            else if (nameof(RandomAccessMemory) == componentType)
            {
                component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);

            }
            else if (nameof(SolidStateDrive) == componentType)
            {
                component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);

            }
            else if (nameof(VideoCard) == componentType)
            {
                component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);

            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            computer.AddComponent(component);
            components.Add(component);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {

            Computer computer = computers.Find(x => x.Id == id);

            if (computers.Contains(computer))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            if (computerType == nameof(Laptop))
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            else if (computerType == nameof(DesktopComputer))
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            computers.Add(computer);

            return string.Format(SuccessMessages.AddedComputer, computer.Id);

        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            IPeripheral peripheral = peripherals.Find(x => x.Id == id);
            Computer computer = computers.Find(x => x.Id == computerId);

            if(computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            if (peripherals.Contains(peripheral))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            if (nameof(Headset) == peripheralType)
            {
                peripheral = new Headset(id,manufacturer,model,price,overallPerformance,connectionType);

            }
            else if (nameof(Keyboard) == peripheralType)
            {
                peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (nameof(Monitor) == peripheralType)
            {
                peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (nameof(Mouse) == peripheralType)
            {
                peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            computer.AddPeripheral(peripheral);
            peripherals.Add(peripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheral.GetType().Name, id, computer.Id);
        }

        public string BuyBest(decimal budget)
        {
            Computer computer = null;

            if(computers.Count == 0 || !computers.Any(x => x.Price <= budget))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            foreach (var item in computers.OrderByDescending(x => x.OverallPerformance).ThenByDescending(x => x.Price))
            {
                if(item.Price <= budget)
                {
                    computer = item;
                    break;
                }
            }

            if(computer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            computers.Remove(computer);

            return computer.ToString();
        }

        public string BuyComputer(int id)
        {
            Computer computer = computers.Find(x => x.Id == id);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            computers.Remove(computer);

            return computer.ToString();
        }

        public string GetComputerData(int id)
        {
            Computer computer = computers.Find(x => x.Id == id);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return computer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            IComputer computer = computers.Find(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IComponent component = computer.RemoveComponent(componentType);

            if(component == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComponent);
            }

            components.Remove(component);

            return $"{string.Format(SuccessMessages.RemovedComponent, componentType, component.Id)}";
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IComputer computer = computers.Find(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IPeripheral peripheral = computer.RemovePeripheral(peripheralType);

            if (peripheral == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, computer.GetType().Name, computer.Id));
            }

            peripherals.Remove(peripheral);

            return $"{string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id)}";
        }
    }
}
