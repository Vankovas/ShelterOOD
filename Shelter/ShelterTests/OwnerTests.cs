using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shelter;

namespace ShelterTests
{
    [TestClass]
    public class OwnerTests
    {
        [TestMethod]
        public void AdoptAdoptableTest()
        {
            Owner owner = new Owner("1234", "Ivan G.");
            Dog puppy = new Dog("Ramses", new DateTime(2014, 01, 05), "Eindhoven", new RFIDTag("rfid1234"));
            owner.Adopt(puppy);
            Assert.IsTrue(owner.Animals.Contains(puppy));
        }

        [TestMethod]
        public void AdoptUnadobtableTest()
        {
            Owner owner = new Owner("1234", "Ivan G.");
            Dog puppy = new Dog("Ramses", DateTime.Now, "Eindhoven", new RFIDTag("rfid1234"));
            owner.Adopt(puppy);
            Assert.IsFalse(owner.Animals.Contains(puppy));
        }

        [TestMethod]
        public void ClaimTest()
        {
            Owner owner = new Owner("1234", "Ivan G.");
            Dog puppy = new Dog("Ramses", DateTime.Now, "Eindhoven", new RFIDTag("rfid1234"));
            puppy.ChangeOwner(owner);
            owner.Claim(puppy);
            
            Assert.AreSame(owner, puppy.LastOwner);
        }
    }
}
