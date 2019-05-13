using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    public abstract class Animal
    {
        //fields
        private string desc;
        private DateTime dateBrought;
        private string locationFound;
        private bool inShelter;
        private RFIDTag rfidTag;
        private Owner lastOwner;

        //properties
        public DateTime DateBrought
        {
            get { return dateBrought; }
        }

        public RFIDTag RfidTag
        {
            get { return rfidTag; }
        }

        public Owner LastOwner
        {
            get { return lastOwner; }
        }

        public bool InShelter
        {
            get { return inShelter; }
        }

        //constructor
        public Animal(string desc, DateTime dateBrought, string locationFound, RFIDTag rfidTag)
        {
            this.desc = desc;
            this.dateBrought = dateBrought;
            this.locationFound = locationFound;
            this.rfidTag = rfidTag;
            this.inShelter = true;
        }
        //methods 
        
        public bool IsAdoptable()
        {
            return (DateTime.Now - dateBrought).Days > 20 && inShelter ? true : false;
        }

        public void TakeFromShelter()
        {
            inShelter = false;
        }

        public bool ReturnToShelter() //returns true if animal is already in shelter and no changes should be made.
        {
            if(this.inShelter)
            {
                return false;
            }

            inShelter = true;
            dateBrought = DateTime.Today;

            return true;
        }

        public void ChangeLocation(string location)
        {
            this.locationFound = location;
        }

        public void ChangeOwner(Owner newOwner)
        {
            lastOwner = newOwner;
        }

        public abstract int CalculateAdoptionPay();

        public abstract int CalculateClaimPay();

        public override string ToString()
        {
            return $"RFID: {rfidTag}, Description: {desc}, Date brought: {dateBrought.ToShortDateString()}, Location found: {locationFound}, " +
                $"In shelter: {inShelter}, Type: {this.GetType()}, Adoptable: {this.IsAdoptable()}";
        }
    }
}
