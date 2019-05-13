using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shelter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterTests
{
    [TestClass()]
    public class AnimalTests
    {
        [TestMethod()]
        public void IsAdoptableTest()
        {
            bool stateExpected = true;
            
            Cat c1 = new Cat("fat", new DateTime(2002, 11, 17), "Tokio", new RFIDTag("12324"), "ugly too");
       
            Assert.AreEqual(stateExpected, c1.IsAdoptable());
        }
    }
}