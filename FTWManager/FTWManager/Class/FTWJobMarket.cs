using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTWManager.Type;

namespace FTWManager.Class
{
    class FTWJobMarket
    {
        FTWSelenium ftwSelenium;
        FTWCSV ftwCSV;

        public FTWJobMarket()
        {
            ftwSelenium = new FTWSelenium();
            ftwCSV = new FTWCSV(); 

        }

      

        public List<DestinationFromDeparture> getBestDestinationFromDeparture(string departure, int max = 3, int maxHops = 1, int maxHopsDestinations = 3)
        {
            List<DestinationFromDeparture> destinationFromDepartures = new List<DestinationFromDeparture>();
            List<SummaryAssignment> topSummaryWindowsAssignments = new List<SummaryAssignment>();


            ftwSelenium.GetAssignmentsByAirport(departure);
            ftwCSV.ReadAssignmentCSV(ref topSummaryWindowsAssignments);

            orderTopListSummaryWindowsAssignments(ref topSummaryWindowsAssignments,max);

            foreach(SummaryAssignment summaryAssignment in topSummaryWindowsAssignments)
            {
                DestinationFromDeparture tempDestinationFromDeparture = new DestinationFromDeparture();
                List<SummaryAssignment> tempSummaryAssignments = new List<SummaryAssignment>();

                tempDestinationFromDeparture.StartAssignment = summaryAssignment;

                ftwSelenium.GetAssignmentsByAirport(summaryAssignment.ArrivalICAO);
                ftwCSV.ReadAssignmentCSV(ref tempSummaryAssignments);

                orderTopListSummaryWindowsAssignments(ref tempSummaryAssignments, maxHopsDestinations);

                tempDestinationFromDeparture.Hops = tempSummaryAssignments;

                destinationFromDepartures.Add(tempDestinationFromDeparture);
            }





            return destinationFromDepartures;
        }

        private void orderTopListSummaryWindowsAssignments(ref List<SummaryAssignment> refListSummaryWindowsAssignments, int top)
        {

            for (int i = 0; i < refListSummaryWindowsAssignments.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < refListSummaryWindowsAssignments.Count; j++)
                    if (refListSummaryWindowsAssignments[j].getTotalPaxMoney() > refListSummaryWindowsAssignments[min].getTotalPaxMoney())
                        min = j;

                SummaryAssignment tmp = refListSummaryWindowsAssignments[min];
                refListSummaryWindowsAssignments[min] = refListSummaryWindowsAssignments[i];
                refListSummaryWindowsAssignments[i] = tmp;
            }

            while (refListSummaryWindowsAssignments.Count > top)
            {
                refListSummaryWindowsAssignments.RemoveAt(refListSummaryWindowsAssignments.Count - 1);
            }
          

        }

       
    }
}
