using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shelter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Tests
{
    [TestClass()]
    public class ShelterTests
    {
        [TestMethod()]
        public void saveOneAnimalTest()
        {
            Shelter s = new Shelter("Shelder", "Neverland 3014LS", "0124531974", "shelder@gmail.com");
            s.saveOneAnimal(new Cat("1 leg missing", new DateTime(2005, 01, 17), "Paris", new RFIDTag("14157"), "2 more legs missing"));
            s.saveOneAnimal(new Dog("2 legs missing", new DateTime(2008, 04, 17), "New York", new RFIDTag("52324")));

            Assert.AreEqual(s.loadAnimals().Count, 2);
        }

        [TestMethod()]
        public void walkDogTest()
        {
            Shelter shelterino = new Shelter("TestShelter", "Aloha Street", "+35989889788", "doomShelter@aloha.com");
            Dog doggo = new Dog("Garry The Test Subject", new DateTime(2009, 04, 14), "Sofia De Janeiro", new RFIDTag("1337"));
            shelterino.saveOneAnimal(doggo);
            Assert.AreEqual(doggo.LastDateWalked, null);
            shelterino.WalkDog(doggo.RfidTag);
            bool existsDate = false;
            if (doggo.LastDateWalked.HasValue) existsDate = true;
            Assert.AreEqual(existsDate, true);
            Assert.AreEqual(doggo.LastDateWalked, DateTime.Today);
        }
        [TestMethod]
        public void rfidScanTest()
        {
            Shelter shelterino = new Shelter("TestShelter", "Aloha Street", "+35989889788", "doomShelter@aloha.com");
            String testString = "1234";
            RFIDTag tag = shelterino.ScanRFID(testString);
            RFIDTag secondTag = new RFIDTag(testString);
            Assert.AreEqual(tag.Number, secondTag.Number);
        }

        [TestMethod]
        public void generateOverviewTest()
        {
            Shelter shelterino = new Shelter("TestShelter", "Aloha Street", "+35989889788", "doomShelter@aloha.com");
            Dog doggo = new Dog("Garry The Test Subject", new DateTime(2009, 04, 14), "Sofia De Janeiro", new RFIDTag("1337"));
            shelterino.AddAnimal(doggo);
            shelterino.AddAnimal(new Cat("1 leg missing", new DateTime(2005, 01, 17), "Paris", new RFIDTag("14157"), "2 more legs missing"));
            shelterino.AddAnimal(new Dog("2 legs missing", new DateTime(2008, 04, 17), "New York", new RFIDTag("52324")));
            Owner own = new Owner("1", "Goshev");
            shelterino.AddOwner(own);
            doggo.ChangeOwner(own);
            shelterino.ClaimAnimal(own.CardId, doggo.RfidTag);
            Assert.AreEqual(shelterino.GenerateOverview()[0], "2");
            Assert.AreEqual(shelterino.GenerateOverview()[1], "3");
            Assert.AreEqual(shelterino.GenerateOverview()[2], "1");
        }

        [TestMethod]
        public void addOwnerTest()
        {
            Shelter shelterino = new Shelter("TestShelter", "Aloha Street", "+35989889788", "doomShelter@aloha.com");
            Owner own = new Owner("1", "Goshev");
            shelterino.AddOwner(own);
            Assert.AreEqual(shelterino.GetOwnersInfo().Length, 1);
        }

        [TestMethod]
        public void changeLocationTest()
        {
            Shelter shelterino = new Shelter("TestShelter", "Aloha Street", "+35989889788", "doomShelter@aloha.com");
            Dog doggo = new Dog("Garry The Test Subject", new DateTime(2009, 04, 14), "Sofia De Janeiro", new RFIDTag("1337"));
            shelterino.AddAnimal(doggo);
        }
    }
}