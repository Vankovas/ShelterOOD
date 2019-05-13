using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    public class RFIDTag
    {
        //fields
        private string number;

        //properties
        public string Number
        {
            get { return number; }
        }

        //constructor
        public RFIDTag(string number)
        {
            this.number = number;
        }

        public override string ToString()
        {
            return number + "";
        }
    }
}
