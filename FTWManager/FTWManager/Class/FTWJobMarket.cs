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


            ftwSelenium.getAssignmentsByAirport(departure);
            ftwCSV.ReadAssignmentCSV(ref topSummaryWindowsAssignments);

            orderTopListSummaryWindowsAssignments(ref topSummaryWindowsAssignments,max);

            foreach(SummaryAssignment summaryAssignment in topSummaryWindowsAssignments)
            {
                DestinationFromDeparture tempDestinationFromDeparture = new DestinationFromDeparture();
                List<SummaryAssignment> tempSummaryAssignments = new List<SummaryAssignment>();

                tempDestinationFromDeparture.StartAssignment = summaryAssignment;

                ftwSelenium.getAssignmentsByAirport(summaryAssignment.ArrivalICAO);
                ftwCSV.ReadAssignmentCSV(ref tempSummaryAssignments);

                orderTopListSummaryWindowsAssignments(ref tempSummaryAssignments, maxHopsDestinations);

                tempDestinationFromDeparture.Hops = tempSummaryAssignments;
            }





            return destinationFromDepartures;
        }

        private void orderTopListSummaryWindowsAssignments(ref List<SummaryAssignment> refListSummaryWindowsAssignments, int top)
        {
            List<SummaryAssignment> temp = new List<SummaryAssignment>();

            temp.Add(refListSummaryWindowsAssignments[0]);
            foreach (SummaryAssignment summaryWindowsAssignment in refListSummaryWindowsAssignments)
            {
                bool foundmin = false;

                foreach (SummaryAssignment tempSummaryWindowsAssignment in temp)
                {
                    if(summaryWindowsAssignment.ArrivalICAO == tempSummaryWindowsAssignment.ArrivalICAO)
                    {
                        break;
                    }
                    if(summaryWindowsAssignment.getGesammtPaxMoney() < temp[temp.Count -1].getGesammtPaxMoney())
                    {
                        temp.Add(summaryWindowsAssignment);
                        break;
                    }
                    if (summaryWindowsAssignment.getGesammtPaxMoney() > tempSummaryWindowsAssignment.getGesammtPaxMoney() && foundmin == false)
                    {
                        temp.Insert(temp.IndexOf(tempSummaryWindowsAssignment), summaryWindowsAssignment);
                        break;
                    }
                    else if (summaryWindowsAssignment.getGesammtPaxMoney() > tempSummaryWindowsAssignment.getGesammtPaxMoney() && foundmin == true)
                    {
                        temp.Insert(temp.IndexOf(tempSummaryWindowsAssignment), summaryWindowsAssignment);
                        break;
                    }
                    else if (summaryWindowsAssignment.getGesammtPaxMoney() < tempSummaryWindowsAssignment.getGesammtPaxMoney())
                    {
                        foundmin = true;
                    }
                    
                }

                foundmin = false;
            }

            refListSummaryWindowsAssignments = temp;

            while(refListSummaryWindowsAssignments.Count > top)
            {
                refListSummaryWindowsAssignments.RemoveAt(refListSummaryWindowsAssignments.Count - 1);
            }
          

        }

    }
}
