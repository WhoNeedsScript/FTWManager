using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTWManager.Type;

namespace FTWManager.Type
{
    class DestinationFromDeparture
    {
        public SummaryAssignment StartAssignment;
        public List<SummaryAssignment> Hops;
    }
}
