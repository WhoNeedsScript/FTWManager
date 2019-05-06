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

using FTWManager.Pages;
using FTWManager.Class;

namespace FTWManager.Windows
{
    /// <summary>
    /// Interaktionslogik für FTWManager.xaml
    /// </summary>
    public partial class FTWManagerWindow : Window
    {
        FTWJobMarketPage pageJobMarket;
        FTWPlanePage  pageFTWPlane;

        FTWCheckResources ftwCheckResources;

        public FTWManagerWindow()
        {
            InitializeComponent();
            ftwCheckResources = new FTWCheckResources();
        }

        private void ButtonJobMarket_Click(object sender, RoutedEventArgs e)
        {
            if(pageJobMarket == null)
            {
                pageJobMarket = new FTWJobMarketPage();
                frameFTWManager.Content = pageJobMarket;
            }
            else if(pageJobMarket != null)
            {
                frameFTWManager.Content = pageJobMarket;
            }
        }

        private void ButtonPlane_Click(object sender, RoutedEventArgs e)
        {
            if (pageJobMarket == null)
            {
                pageFTWPlane = new FTWPlanePage();
                frameFTWManager.Content = pageFTWPlane;
            }
            else if (pageFTWPlane != null)
            {
                frameFTWManager.Content = pageFTWPlane;
            }
        }
    }
}
