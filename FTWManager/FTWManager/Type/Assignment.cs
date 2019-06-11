using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWManager.Type
{
    class Assignment
    {
        public string Departure;
        public string Arrival;
        public int Type; // 1 =pax, 2 = bc pac,  3 = cargo
        public int Amount;
        public double Money;
        public int paxCargo;
        public int paxWeight;
    }
}
