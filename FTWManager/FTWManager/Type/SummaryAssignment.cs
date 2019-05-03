using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWManager.Type
{
    class SummaryAssignment
    {
        public  string DepartureICAO;
        public string ArrivalICAO;

        public int TotalEconomyPax;
        public double TotalEconomPaxMoney;

        public int TotalBusinessPax;
        public double TotalBusinessPaxMoney;

        public int TotalCargo;
        public double TotalCargoMoney;

        List<Assignment> ListAssignments = new List<Assignment>();


        public int getTotalPax()
        {
            return TotalEconomyPax + TotalBusinessPax;
        }

        public double getTotalPaxMoney()
        {
            return TotalEconomPaxMoney + TotalBusinessPaxMoney;
        }

        public double getTotalMoney()
        {
            return TotalEconomyPax + TotalBusinessPax + TotalCargoMoney;
        }

        public double getTotalGewicht()
        {
            return TotalCargo;
        }

        public void addAssignment(Assignment assignment)
        {
            if(DepartureICAO == null)
            {
                DepartureICAO = assignment.Departure;
            }
            if(ArrivalICAO == null)
            {
                ArrivalICAO = assignment.Arrival;
            }

            switch (assignment.Type)
            {
                case 1:
                    TotalEconomyPax += assignment.Amount;
                    TotalEconomPaxMoney += assignment.Money;
                    break;

                case 2:
                    TotalBusinessPax += assignment.Amount;
                    TotalBusinessPaxMoney += assignment.Money;
                    break;

                case 3:
                    TotalCargo += assignment.Amount;
                    TotalCargoMoney += assignment.Money;
                    break;
            }

            ListAssignments.Add(assignment);
        }
        
    }
}
