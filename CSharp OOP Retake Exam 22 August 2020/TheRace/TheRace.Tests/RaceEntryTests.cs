using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestOne()
        {
            RaceEntry race = new RaceEntry();

            Assert.That(race.Counter, Is.EqualTo(0));
        }

        [Test]
        public void AddingRacer()
        {
            RaceEntry race = new RaceEntry();
            UnitDriver driver = new UnitDriver("pesho", new UnitCar("porsche", 200, 5000));

            Assert.That(race.AddDriver(driver), Is.EqualTo("Driver pesho added in race."));
        }

        [Test]
        public void AddingEmptyRacer()
        {
            RaceEntry race = new RaceEntry();
            UnitDriver driver = null;
            Assert.Throws<InvalidOperationException>(() => race.AddDriver(driver));
        }

        [Test]
        public void AddingExistingRacer()
        {
            RaceEntry race = new RaceEntry();
            UnitDriver driver = new UnitDriver("pesho", new UnitCar("porsche", 200, 5000));
            race.AddDriver(driver);
            Assert.Throws<InvalidOperationException>(() => race.AddDriver(driver));
        }

        [Test]
        public void CalculateAverageHorsePowersMinParticipants()
        {
            RaceEntry race = new RaceEntry();
            UnitDriver driver = new UnitDriver("pesho", new UnitCar("porsche", 200, 5000));
            race.AddDriver(driver);
            Assert.Throws<InvalidOperationException>(() => race.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsePowersSucsses()
        {
            RaceEntry race = new RaceEntry();
            UnitDriver driver = new UnitDriver("pesho", new UnitCar("porsche", 200, 5000));
            UnitDriver racer = new UnitDriver("stoqn", new UnitCar("porsche", 200, 5000));
            UnitDriver petko = new UnitDriver("petko", new UnitCar("porsche", 200, 5000));
            race.AddDriver(driver);
            race.AddDriver(racer);
            race.AddDriver(petko);
            Assert.That(() => race.CalculateAverageHorsePower(), Is.EqualTo(600 / 3));
        }
    }
}