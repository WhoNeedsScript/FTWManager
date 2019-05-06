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
    /// Interaktionslogik für FTWPlanePage.xaml
    /// </summary>
    public partial class FTWPlanePage : Page
    {
        FTWXML ftwXML;
        List<Plane> planes;


        public FTWPlanePage()
        {
            ftwXML = new FTWXML();
            planes = new List<Plane>();

            InitializeComponent();
            fillComboBoxPlanes();
        }

        private void fillComboBoxPlanes()
        {
            planes.AddRange(ftwXML.readFTWPlaneXML());
            foreach (Plane plane in planes)
            {
                comboBoxPlanes.Items.Add(plane.Name);
            }

        }

        private void ButtonAddPlane_Click(object sender, RoutedEventArgs e)
        {
            foreach (Plane plane in planes)
            {
                if (plane.Name == textBoxPlaneName.Text)
                {

                    return;
                }
            }

            ftwXML.writeAddAirplane(textBoxPlaneName.Text, Convert.ToInt16(textBoxPlaneEconemySeats.Text), Convert.ToInt16(textBoxPlaneBusinessSeats.Text), Convert.ToInt16(textBoxPlaneCargo.Text), Convert.ToInt16(textBoxPlanePayloud.Text));
        }

        private void ComboBoxPlanes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Plane plane in planes)
            {
                if (plane.Name == comboBoxPlanes.SelectedValue.ToString())
                {
                    textBoxPlaneName.Text = plane.Name;
                    textBoxPlaneEconemySeats.Text = plane.EconemySeats.ToString();
                    textBoxPlaneBusinessSeats.Text = plane.BusinessSeats.ToString();
                    textBoxPlaneCargo.Text = plane.Cargo.ToString();
                    textBoxPlanePayloud.Text = plane.Payloud.ToString();

                    return;
                }
            }
        }
    }
}
