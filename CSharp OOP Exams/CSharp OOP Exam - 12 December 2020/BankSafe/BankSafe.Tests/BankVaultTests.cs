using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private BankVault bankVault;
        private Dictionary<string, Item> cells;
        private Item item;

        [SetUp]
        public void Setup()
        {
            cells = new Dictionary<string, Item>()
            {
                {"A1", null},
                {"A2", null},
                {"A3", null},
                {"A4", null},
                {"B1", null},
                {"B2", null},
                {"B3", null},
                {"B4", null},
                {"C1", null},
                {"C2", null},
                {"C3", null},
                {"C4", null},
            };
        }

        [Test]
        public void InitializationConstructor()
        {
            bankVault = new BankVault();

            Assert.That(bankVault.VaultCells, Is.EqualTo(cells));
        }

        [Test]

        public void AddItemInMissingCell()
        {
            bankVault = new BankVault();

            Assert.Throws<ArgumentException>(() => bankVault.AddItem("a16", item));
        }

        [Test]
        public void AddItemToCellWhichIsNotEmpty()
        {
            bankVault = new BankVault();
            item = new Item("Pesho", "5555");

            bankVault.AddItem("A1", item);

            Assert.Throws<ArgumentException>(() => bankVault.AddItem("A1", item));
        }

        [Test]
        public void AddItemToCellWhichContainsItem()
        {
            bankVault = new BankVault();
            item = new Item("Pesho", "5555");

            bankVault.AddItem("A1", item);

            Assert.Throws<InvalidOperationException>(() => bankVault.AddItem("A3", item));
        }

        [Test]
        public void RemoveItemFromNotExistingCell()
        {
            bankVault = new BankVault();
            item = new Item("Pesho", "5555");

            Assert.Throws<ArgumentException>(() => bankVault.RemoveItem("A15", item));
        }

        [Test]
        public void RemoveItemWhichDoesNotExistInThatCell()
        {
            bankVault = new BankVault();
            item = new Item("Pesho", "5555");
            bankVault.AddItem("A2", item);

            Item bag = new Item("chanta", "23223");

            Assert.Throws<ArgumentException>(() => bankVault.RemoveItem("A2", bag));
        }

        [Test]
        public void RemoveExistingItemSuccessfully()
        {
            bankVault = new BankVault();
            item = new Item("Pesho", "5555");
            bankVault.AddItem("A2", item);
            Assert.That(bankVault.RemoveItem("A2", item), Is.EqualTo($"Remove item:{item.ItemId} successfully!"));
        }

        [Test]
        public void AddingItemSuccessfully()
        {
            bankVault = new BankVault();
            item = new Item("Pesho", "5555");
            Assert.That(bankVault.AddItem("A2", item), Is.EqualTo($"Item:{item.ItemId} saved successfully!"));
        }
    }
}