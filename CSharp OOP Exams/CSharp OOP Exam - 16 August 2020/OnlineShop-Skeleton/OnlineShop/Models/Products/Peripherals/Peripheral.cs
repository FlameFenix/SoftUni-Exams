using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products
{
    public abstract class Peripheral : Product, IPeripheral
    {
        private string connectionType;
        public Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.ConnectionType = connectionType;
        }

        public string ConnectionType
        {
            get => this.connectionType;
            private set
            {
                this.connectionType = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(SuccessMessages.PeripheralToString, this.ConnectionType);
        }
    }
}
