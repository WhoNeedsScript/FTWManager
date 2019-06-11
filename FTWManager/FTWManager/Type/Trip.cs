using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTWManager.Type;

namespace FTWManager.Type
{
    class Trip
    {
       

        //Ausgewählte Ziele vom Startflughafen
        public List<AssignmentsFromDeparture> Hop = new List<AssignmentsFromDeparture>();

        public double money;

        public double getMoney()
        {
            double money = 0;

            foreach(AssignmentsFromDeparture tempHop in Hop)
            {
                money += tempHop.getTotalMoney();
            }
            this.money = money;
            return money;
        }


        //Sortiert die Arrivals nach Gewinn
        public void OrderArrivalsByTotalMoney(int maxArrivals)
        {

            for (int i = 0; i < Hop.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < Hop.Count; j++)
                    if (Hop[j].getTotalMoney() > Hop[min].getTotalMoney())
                        min = j;

                AssignmentsFromDeparture tmp = Hop[min];
                Hop[min] = Hop[i];
                Hop[i] = tmp;
            }

            while (Hop.Count > maxArrivals)
            {
                Hop.RemoveAt(Hop.Count - 1);
            }


        }

    }
}
