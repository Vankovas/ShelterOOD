using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    public class Cat : Animal
    {
        //fields
        private string extraInfo;

        //properties
        public string ExtraInfo
        {
            get { return extraInfo; }
        }

        //constructor
        public Cat(string desc, DateTime dateBrought, string locationFound, RFIDTag rfidTag, string extraInfo)
            :base(desc, dateBrought, locationFound, rfidTag)
        {
            this.extraInfo = extraInfo;
        }

        //methods
        public override int CalculateAdoptionPay()
        {
            return 25;
        }

        public override int CalculateClaimPay()
        {
            return 15;
        }

        public void changeExtra(string desc)
        {
            extraInfo = desc;
        }

        public override string ToString()
        {
            return base.ToString() + $", Extra Info: {extraInfo}";
        }
    }
}
