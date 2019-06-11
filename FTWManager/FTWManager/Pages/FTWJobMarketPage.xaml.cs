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
        Class.FTWXML ftwXML;

        List<Plane> planes = new List<Plane>();

        public FTWJobMarketPage()
        {
            InitializeComponent();

            ftwJobmarket = new Class.FTWJobMarket();
            ftwCheckResources = new Class.FTWCheckResources();
            ftwXML = new Class.FTWXML();

            loadGui();
        }

        private void loadGui()
        { 
            planes.AddRange(ftwXML.readFTWPlaneXML());
            foreach (Plane plane in planes)
            {
                comboBoxPlanes.Items.Add(plane.Name);
            }
        }

        private void CmdSearch_Click(object sender, RoutedEventArgs e)
        {
            lvJobMarket.Items.Clear();

            Trip bestTrip = ftwJobmarket.GetBestTrip(textboxDepartureIcao.Text, true, planes.Find(x => x.Name == comboBoxPlanes.SelectedItem.ToString()));

            foreach (AssignmentsFromDeparture tempdestinationFromDeparture in bestTrip.Hop)
            {

                lvJobMarket.Items.Add(new Items
                {
                    // Noch alles zu toString Ändern
                    Departure = tempdestinationFromDeparture.DepartureICAO,
                    Arrival = tempdestinationFromDeparture.ArrivalICAO,
                    Pax = Convert.ToString(tempdestinationFromDeparture.getTotalPax()),
                    PaxMoney = Convert.ToString(tempdestinationFromDeparture.getTotalPaxMoney()),
                    Cargo = Convert.ToString(tempdestinationFromDeparture.TotalCargo),
                    CargoMoney = tempdestinationFromDeparture.TotalCargoMoney.ToString(),
                    GesammtMoney = bestTrip.getMoney().ToString()



                }
                    );

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

            public string GesammtPaxMoney { get; set; }
            public string GesammtCargoMoney { get; set; }

            public string GesamtMoney { get; set; }

        }

      
    }
}
