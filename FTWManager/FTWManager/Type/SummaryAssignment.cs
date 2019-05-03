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

        public int EconomyPax;
        public double EconomPaxMoney;

        public int BusinessPax;
        public double BusinessPaxMoney;

        public int Cargo;
        public double CargoMoney;

        List<Assignment> ListAssignments = new List<Assignment>();


        public int getGesammtPax()
        {
            return EconomyPax + BusinessPax;
        }

        public double getGesammtPaxMoney()
        {
            return EconomPaxMoney + BusinessPaxMoney;
        }

        public double getGesamtwert()
        {
            return EconomyPax + BusinessPax + CargoMoney;
        }

        public double getGesamtwertGewicht()
        {
            return Cargo;
        }

        
    }
}
