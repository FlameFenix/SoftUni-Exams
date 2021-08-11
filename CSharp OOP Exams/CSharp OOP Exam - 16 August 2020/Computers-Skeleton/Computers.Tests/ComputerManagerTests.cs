using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Computers.Tests
{
    public class Tests
    {
        private Computer computer;
        private ComputerManager manager;

        [SetUp]
        public void Setup()
        {
            computer = new Computer("hp", "pavilion", 500);
            manager = new ComputerManager();
        }

        [Test]
        public void CheckConstructorAndPropertiest()
        {
            Assert.That(computer.Manufacturer, Is.EqualTo("hp"));
            Assert.That(computer.Model, Is.EqualTo("pavilion"));
            Assert.That(computer.Price, Is.EqualTo(500));
            Assert.That(manager.Computers.Count, Is.EqualTo(0));
            Assert.That(manager.Count, Is.EqualTo(0));
            Assert.That(manager, Is.Not.Null);
            Assert.That(manager.Computers, Is.Not.Null);
        }

        [Test]
        public void AddingComputerShouldReturnArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => manager.AddComputer(null));
        }

        [Test]
        public void AddingExistingComputerShouldReturnArgumentExcp()
        {
            manager.AddComputer(computer);
            Assert.Throws<ArgumentException>(() => manager.AddComputer(computer));
        }

        [Test]
        public void AddingComputerShouldBeSuccsessfull()
        {
            manager.AddComputer(computer);
            Assert.That(manager.Computers.Count, Is.EqualTo(1));
            Assert.That(manager.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveComputerShouldBeSuccsessfull()
        {
            manager.AddComputer(computer);
            Computer dell = new Computer("dell", "latitude", 1500);
            manager.AddComputer(dell);

            Assert.That(manager.Computers.Count, Is.EqualTo(2));
            Assert.That(manager.RemoveComputer("dell", "latitude"), Is.EqualTo(dell));
            Assert.That(manager.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveNullComputerShouldThrowArgumentNull()
        {
            manager.AddComputer(computer);
            Computer dell = new Computer("dell", "latitude", 1500);
            manager.AddComputer(dell);

            Assert.Throws<ArgumentNullException>(() => manager.RemoveComputer(null, null));
        }

        [Test]
        public void GetComputerShouldBeSuccsessfull()
        {
            manager.AddComputer(computer);
            Computer dell = new Computer("dell", "latitude", 1500);
            manager.AddComputer(dell);

            Assert.That(() => manager.GetComputer("dell", "latitude"), Is.EqualTo(dell));
        }

        [Test]
        public void GetNullComputerShouldThrowArgumentNull()
        {
            manager.AddComputer(computer);
            Computer dell = new Computer("dell", "latitude", 1500);
            manager.AddComputer(dell);

            Assert.Throws<ArgumentNullException>(() => manager.GetComputer(null, "latitude"));
            Assert.Throws<ArgumentNullException>(() => manager.GetComputer("dell", null));
        }

        [Test]
        public void GetNullComputerShouldThrowArgumentNullSecondPhase()
        {
            manager.AddComputer(computer);
            Computer dell = new Computer("dell", "latitude", 1500);
            manager.AddComputer(dell);

            Assert.Throws<ArgumentException>(() => manager.GetComputer("sony", "latitude"));
        }

        [Test]
        public void GetComputersByManufacturerSuccsessfull()
        {
            manager.AddComputer(computer);
            Computer dell = new Computer("dell", "latitude", 1500);
            manager.AddComputer(dell);
            ICollection<Computer> computers = new List<Computer>() { dell };

            Assert.That(() => manager.GetComputersByManufacturer("dell"), Is.EqualTo(computers));
        }

        [Test]
        public void GetComputersByManufacturerNull()
        {
            manager.AddComputer(computer);
            Computer dell = new Computer("dell", "latitude", 1500);
            manager.AddComputer(dell);
            ICollection<Computer> computers = new List<Computer>() { dell };

            Assert.Throws<ArgumentNullException>(() => manager.GetComputersByManufacturer(null));
        }
    }
}