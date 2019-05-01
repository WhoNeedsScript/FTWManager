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


namespace FTWManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FTWJobMarket ftwJobMarket;
        public MainWindow()
        {
            InitializeComponent();
            ftwJobMarket = new FTWJobMarket();
        }

        private void ButtonSuchen_Click(object sender, RoutedEventArgs e)
        {
            ftwJobMarket.getBestDestinationFromDeparture(TextBoxDeparture.Text);
        }
    }
}
