using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWManager.Type
{
    class Plane
    {
        public string Name;
        public  int EconemySeats;
        public int BusinessSeats;
        public  int Cargo;
        public int Payloud;

        public  SummaryAssignment Load = new SummaryAssignment();
    }
}
