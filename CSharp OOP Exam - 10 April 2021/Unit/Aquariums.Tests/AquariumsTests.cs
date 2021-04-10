namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class AquariumsTests
    {
        private Aquarium aquarium;

        [SetUp]
        public void SetUp()
        {
            aquarium = new Aquarium("Aqua", 50);
        }

        [Test]
        public void TestingConstructorAndProperties()
        {
            int expectedCapacity = 50;
            string expectedName = "Aqua";
            int expectedFishCount = 0;

            Fish fish = new Fish("Stoqn");

            Assert.That(aquarium.Capacity, Is.EqualTo(expectedCapacity));
            Assert.That(aquarium.Name, Is.EqualTo(expectedName));
            Assert.That(aquarium.Count, Is.EqualTo(expectedFishCount));
            Assert.That(fish.Name, Is.EqualTo("Stoqn"));
            Assert.That(fish.Available, Is.EqualTo(true));
        }

        [Test]
        public void TestingAquariumWhenNameIsNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => aquarium = new Aquarium(null, 30));
            Assert.Throws<ArgumentNullException>(() => aquarium = new Aquarium("", 30));
        }

        [Test]
        public void TestingAquariumWhenCapacityIsBelowZero()
        {
            Assert.Throws<ArgumentException>(() => aquarium = new Aquarium("Aqua", -2));
        }

        [Test]
        public void TestingAquariumWhenAddingFish()
        {
            Fish fish = new Fish("Misho");
            aquarium.Add(fish);
            int expectedCount = 1;
            Assert.That(aquarium.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void TestingAquariumWhenAddingFishAndAquaCapacityIsFull()
        {
            aquarium = new Aquarium("Aqua", 1);
            Fish fish = new Fish("Misho");
            Fish secondFish = new Fish("Stoqn");
            aquarium.Add(fish);
            Assert.Throws<InvalidOperationException>(() => aquarium.Add(secondFish));
        }

        [Test]
        public void TestingAquariumWhenRemoveFishNotExistingFish()
        {
            Fish fish = new Fish("Stoqm");
            aquarium.Add(fish);
            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish(null));
        }

        [Test]
        public void TestingAquariumWhenRemoveFishSuccsessfully()
        {
            Fish fish = new Fish("Pesho");
            aquarium.Add(fish);
            aquarium.RemoveFish("Pesho");
            int expectedCount = 0;
            Assert.That(aquarium.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void TestingAquariumWhenSellingSuccsessfully()
        {
            Fish secondFish = new Fish("Gosho");
            aquarium.Add(secondFish);
            Assert.That(() => aquarium.SellFish("Gosho"), Is.EqualTo(secondFish));
            aquarium.SellFish("Gosho");
            Assert.That(secondFish.Available, Is.EqualTo(false));
        }

        [Test]
        public void TestingAquariumWhenSellingFailed()
        {
            Fish fish = new Fish("Pesho");
            Fish secondFish = new Fish("Gosho");
            aquarium.Add(fish);
            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish("Gosho"));
        }

        [Test]
        public void TestingReportOutputFromAquarium()
        {
            Fish fish = new Fish("Misho");
            Fish secondFish = new Fish("Stoqn");
            aquarium.Add(fish);
            aquarium.Add(secondFish);
            string output = $"Fish available at {aquarium.Name}: {fish.Name}, {secondFish.Name}";

            Assert.That(() => aquarium.Report(), Is.EqualTo(output));
        }
    }
}
