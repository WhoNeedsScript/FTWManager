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
        FTWSelenium fTWSelenium;
        public MainWindow()
        {
            InitializeComponent();
            fTWSelenium = new FTWSelenium();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<SummaryWindowsAssignment> summaryWindowsAssignments = new List<SummaryWindowsAssignment>();
            summaryWindowsAssignments.AddRange(fTWSelenium.getSummaryWindowsAssignment("EDDF"));
          
        }
    }
}
