using Bakery.Core.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Models.Tables;
using System.Linq;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;
        private decimal totalIncome;
        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();
        }
        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink;

            if (nameof(Tea) == type)
            {
                drink = new Tea(name, portion, brand);
                drinks.Add(drink);
            }
            else if (nameof(Water) == type)
            {
                drink = new Water(name, portion, brand);
                drinks.Add(drink);
            }

            return $"Added {name} ({brand}) to the drink menu";
        }

        public string AddFood(string type, string name, decimal price)
        {
            if (nameof(Cake) == type)
            {
                IBakedFood cake = new Cake(name, price);
                bakedFoods.Add(cake);
            }
            else if (nameof(Bread) == type)
            {
                IBakedFood bread = new Bread(name, price);
                bakedFoods.Add(bread);
            }

            return $"Added {name} ({type}) to the menu";
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table;

            if (nameof(OutsideTable) == type)
            {
                table = new OutsideTable(tableNumber, capacity);
                tables.Add(table);
            }
            else if (nameof(InsideTable) == type)
            {
                table = new InsideTable(tableNumber, capacity);
                tables.Add(table);
            }

            return $"Added table number {tableNumber} in the bakery";
        }

        public string GetFreeTablesInfo()
        {
            string tableInfo = string.Empty;

            foreach (var table in tables.Where(x => x.IsReserved == false))
            {
                tableInfo += table.GetFreeTableInfo() + Environment.NewLine;
            }

            return tableInfo.Trim();
        }

        public string GetTotalIncome()
        {
            return $"Total income: {totalIncome:f2}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            ITable table = tables.Find(x => x.TableNumber == tableNumber);
            decimal tableBill = table.GetBill();
            totalIncome += tableBill;
            table.Clear();

            return $"Table: {tableNumber}" + Environment.NewLine +
                   $"Bill: {tableBill:f2}";
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable table = tables.Find(x => x.TableNumber == tableNumber);

            if (table != null)
            {
                IDrink drink = drinks.Find(x => x.Name == drinkName && x.Brand == drinkBrand);
                if (drink != null)
                {
                    table.OrderDrink(drink);
                    return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
                }
                else
                {
                    return $"There is no {drinkName} {drinkBrand} available";
                }
            }
            else
            {
                return $"Could not find table {tableNumber}";
            }
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable table = tables.Find(x => x.TableNumber == tableNumber);

            if (table != null)
            {
                IBakedFood food = bakedFoods.Find(x => x.Name == foodName);
                if(food != null)
                {
                    table.OrderFood(food);
                    return $"Table {tableNumber} ordered {foodName}";
                }
                else
                {
                    return $"No {foodName} in the menu";
                }
                
            }
            else
            {
                return $"Could not find table {tableNumber}";
            }

        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable reserved = tables.Find(x => x.IsReserved == false && x.Capacity >= numberOfPeople);

            if (reserved == null)
            {
                return $"No available table for {numberOfPeople} people";
            }
            else
            {
                reserved.Reserve(numberOfPeople);
                return $"Table {reserved.TableNumber} has been reserved for {numberOfPeople} people";
            }
        }
    }
}
