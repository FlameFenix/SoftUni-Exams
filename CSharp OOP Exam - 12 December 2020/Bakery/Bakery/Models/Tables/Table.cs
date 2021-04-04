using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private List<IBakedFood> foodOrders;
        private List<IDrink> drinkOrders;
        private int tableNumber;
        private int capacity;
        private int numberOfPeople;
        private decimal pricePerPerson;
        private bool isReserverd;
        private decimal price;


        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();

            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;
        }
        public int TableNumber
        {
            get
            {
                return this.tableNumber;
            }
            private set
            {
                this.tableNumber = value;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }
                this.capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get
            {
                return this.numberOfPeople;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }
                this.numberOfPeople = value;
            }
        }

        public decimal PricePerPerson
        {
            get
            {
                return this.pricePerPerson;
            }
            private set
            {
                this.pricePerPerson = value;
            }
        }

        public bool IsReserved
        {
            get
            {
                return isReserverd;
            }
            private set
            {
                this.isReserverd = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            private set
            {
                this.price = value;
            }
        }

        public void Clear()
        {
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
            IsReserved = false;
            numberOfPeople = 0;
        }

        public decimal GetBill()
        {
            decimal bill = 0m;

            foreach (var item in foodOrders)
            {
                bill += item.Price;
            }

            foreach (var item in drinkOrders)
            {
                bill += item.Price;
            }

            bill += numberOfPeople * pricePerPerson;

            return bill;
        }

        public string GetFreeTableInfo()
        {
               
         return $"Table: {TableNumber}" + Environment.NewLine +
                $"Type: {GetType().Name}" + Environment.NewLine +
                $"Capacity: {Capacity}" + Environment.NewLine +
                $"Price per Person: {PricePerPerson}";

        }

        public void OrderDrink(IDrink drink)
        {
            drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
            NumberOfPeople = numberOfPeople;
        }
    }
}
