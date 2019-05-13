using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    public class Dog : Animal
    {
        //fields
        private DateTime? lastDateWalked;

        //properties
        public DateTime? LastDateWalked
        {
            get { return lastDateWalked; }
        }

        //constructor
        public Dog(string desc, DateTime dateBrought, string locationFound, RFIDTag rfidTag, DateTime? lastDateWalked = null)
            : base(desc, dateBrought, locationFound, rfidTag)
        {
            if (lastDateWalked != null)
            {
                this.lastDateWalked = lastDateWalked;
            }
        }

        //methods
        public override int CalculateAdoptionPay()
        {
            return 20;
        }

        public override int CalculateClaimPay()
        {
            if (lastDateWalked.HasValue)
            {
                return 10 + 2 * (base.DateBrought - lastDateWalked.Value).Days;
            }
            else
            {
                return 10;
            }
        }

        public override string ToString()
        {
            if(lastDateWalked.HasValue)
            {
                return base.ToString() + $", Last date walked: {((DateTime)lastDateWalked).ToShortDateString()}";
            }
            return base.ToString() + $", Last date walked: {lastDateWalked}";
        }

        public void Walk()
        {
            lastDateWalked = DateTime.Today;
        }
    }
}
