using FTWManager.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using FTWManager.Type;

namespace FTWManager.Windows
{
    /// <summary>
    /// Interaktionslogik für FTWJobmarketManager.xaml
    /// </summary>
    public partial class FTWJobmarketManager : Window
    {
        FTWJobMarket ftwJobmarket;

        public FTWJobmarketManager()
        {
            InitializeComponent();
            ftwJobmarket = new FTWJobMarket();
        }

        private void CmdSearch_Click(object sender, RoutedEventArgs e)
        {

            List<DestinationFromDeparture> listDestinationFromDepartures  = ftwJobmarket.getBestDestinationFromDeparture(textboxDepartureIcao.Text);

            foreach(DestinationFromDeparture tempdestinationFromDeparture in listDestinationFromDepartures)
            {             

                foreach (SummaryAssignment tempSummaryAssignment in tempdestinationFromDeparture.Hops)
                {

                    lvJobMarket.Items.Add(new Items
                    {

                        Departure = tempdestinationFromDeparture.StartAssignment.DepartureICAO,
                        Arrival = tempdestinationFromDeparture.StartAssignment.ArrivalICAO,
                        Pax = Convert.ToString(tempdestinationFromDeparture.StartAssignment.getGesammtPax()),
                        PaxMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.getGesammtPaxMoney()),
                        Cargo = Convert.ToString(tempdestinationFromDeparture.StartAssignment.Cargo),
                        CargoMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.CargoMoney),

                        HopDeparture = tempSummaryAssignment.DepartureICAO,
                        HopArrival = tempSummaryAssignment.ArrivalICAO,
                        HopPax = Convert.ToString(tempSummaryAssignment.getGesammtPax()),
                        HopPaxMoney = Convert.ToString(tempSummaryAssignment.getGesammtPaxMoney()),
                        HopCargo = Convert.ToString(tempSummaryAssignment.Cargo),
                        HopCargoMoney = Convert.ToString(tempSummaryAssignment.CargoMoney),

                        GesammtPaxMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.getGesammtPaxMoney() + tempSummaryAssignment.getGesammtPaxMoney()),
                        GesammtCargoMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.CargoMoney + tempSummaryAssignment.CargoMoney)

                    }
                    );
                }
            }
        }
        public struct Items
        {
            public string Departure { get; set; }
            public string Arrival { get; set; }
            public string Pax { get; set; }
            public string PaxMoney { get; set; }
            public string Cargo { get; set; }
            public string CargoMoney { get; set; }

            public string HopDeparture { get; set; }
            public string HopArrival { get; set; }
            public string HopPax { get; set; }
            public string HopPaxMoney { get; set; }
            public string HopCargo { get; set; }
            public string HopCargoMoney { get; set; }

            public string GesammtPaxMoney { get; set; }
            public string GesammtCargoMoney { get; set; }
           
        }
    }
}
