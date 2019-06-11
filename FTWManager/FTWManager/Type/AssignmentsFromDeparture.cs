using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWManager.Type
{

    //Eine Zusammenfassung aller Aufträge der Verbindung X Zu Y
    class AssignmentsFromDeparture
    {
        public  string DepartureICAO;
        public string ArrivalICAO;

        public double totalMoney;

        public int TotalEconomyPax;
        public double TotalEconomPaxMoney;

        public int TotalBusinessPax;
        public double TotalBusinessPaxMoney;

        public int TotalCargo;
        public double TotalCargoMoney;

        public List<Assignment> ListAssignments = new List<Assignment>();


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
            totalMoney = TotalEconomPaxMoney + TotalBusinessPaxMoney + TotalCargoMoney;
            return totalMoney;
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
        
        public void OrderByTypeAndAmount()
        {
            List<Assignment> ListAssignmentsPax = new List<Assignment>();
            List<Assignment> ListAssignmentsCargo = new List<Assignment>();

            foreach(Assignment temp in ListAssignments)
            {
                if(temp.Type == 1)
                {
                    ListAssignmentsPax.Add(temp);
                }
                else if(temp.Type == 3)
                {
                    ListAssignmentsCargo.Add(temp);
                }
            }

            for (int i = 0; i < ListAssignmentsPax.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < ListAssignmentsPax.Count; j++)
                    if (ListAssignmentsPax[j].Amount < ListAssignmentsPax[min].Amount)
                        min = j;

                Assignment tmp = ListAssignmentsPax[min];
                ListAssignmentsPax[min] = ListAssignmentsPax[i];
                ListAssignmentsPax[i] = tmp;
            }

            for (int i = 0; i < ListAssignmentsCargo.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < ListAssignmentsCargo.Count; j++)
                    if (ListAssignmentsCargo[j].Amount < ListAssignmentsCargo[min].Amount)
                        min = j;

                Assignment tmp = ListAssignmentsCargo[min];
                ListAssignmentsCargo[min] = ListAssignmentsCargo[i];
                ListAssignmentsCargo[i] = tmp;
            }

            ListAssignments.Clear();

            ListAssignments.AddRange(ListAssignmentsPax);
            ListAssignments.AddRange(ListAssignmentsCargo);

        }
    }
}
