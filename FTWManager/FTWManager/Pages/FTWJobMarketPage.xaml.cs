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
using System.Windows.Navigation;
using System.Windows.Shapes;

using FTWManager.Class;
using FTWManager.Type;

namespace FTWManager.Pages
{
    /// <summary>
    /// Interaktionslogik für FTWJobMarket.xaml
    /// </summary>
    public partial class FTWJobMarketPage : Page
    {
        Class.FTWJobMarket ftwJobmarket;
        Class.FTWCheckResources ftwCheckResources;

        public FTWJobMarketPage()
        {
            InitializeComponent();

            ftwJobmarket = new Class.FTWJobMarket();
            ftwCheckResources = new Class.FTWCheckResources();
        }

        private void CmdSearch_Click(object sender, RoutedEventArgs e)
        {
            List<DestinationFromDeparture> listDestinationFromDepartures = ftwJobmarket.getBestDestinationFromDeparture(textboxDepartureIcao.Text);

            foreach (DestinationFromDeparture tempdestinationFromDeparture in listDestinationFromDepartures)
            {

                foreach (SummaryAssignment tempSummaryAssignment in tempdestinationFromDeparture.Hops)
                {

                    lvJobMarket.Items.Add(new Items
                    {

                        Departure = tempdestinationFromDeparture.StartAssignment.DepartureICAO,
                        Arrival = tempdestinationFromDeparture.StartAssignment.ArrivalICAO,
                        Pax = Convert.ToString(tempdestinationFromDeparture.StartAssignment.getTotalPax()),
                        PaxMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.getTotalPaxMoney()),
                        Cargo = Convert.ToString(tempdestinationFromDeparture.StartAssignment.TotalCargo),
                        CargoMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.TotalCargoMoney),

                        HopDeparture = tempSummaryAssignment.DepartureICAO,
                        HopArrival = tempSummaryAssignment.ArrivalICAO,
                        HopPax = Convert.ToString(tempSummaryAssignment.getTotalPax()),
                        HopPaxMoney = Convert.ToString(tempSummaryAssignment.getTotalPaxMoney()),
                        HopCargo = Convert.ToString(tempSummaryAssignment.TotalCargo),
                        HopCargoMoney = Convert.ToString(tempSummaryAssignment.TotalCargoMoney),

                        GesammtPaxMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.getTotalPaxMoney() + tempSummaryAssignment.getTotalPaxMoney()),
                        GesammtCargoMoney = Convert.ToString(tempdestinationFromDeparture.StartAssignment.TotalCargoMoney + tempSummaryAssignment.TotalCargoMoney)

                    }
                    );
                }
            }
        }










        public void OpenConsol()
        {
            FTWManagerConsol ftwManagerConsol = new FTWManagerConsol();
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
