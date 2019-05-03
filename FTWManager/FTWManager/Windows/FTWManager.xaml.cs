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

using FTWManager.Page;

namespace FTWManager.Windows
{
    /// <summary>
    /// Interaktionslogik für FTWManager.xaml
    /// </summary>
    public partial class FTWManager : Window
    {
        FTWJobMarket pageJobMarket;

        public FTWManager()
        {
            InitializeComponent();
        }

        private void ButtonJobMarket_Click(object sender, RoutedEventArgs e)
        {
            if(pageJobMarket == null)
            {
                pageJobMarket = new FTWJobMarket();
                frameFTWManager.Content = pageJobMarket;
            }
            else if(pageJobMarket != null)
            {
                frameFTWManager.Content = pageJobMarket;
            }
        }
    }
}
