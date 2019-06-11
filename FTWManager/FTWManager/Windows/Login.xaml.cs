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

using FTWManager.Class;

namespace FTWManager.Windows
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {

            FTWCheckResources ftwCheckResources = new FTWCheckResources();

            if(ftwCheckResources.CheckUserdata())
            {
                //FTWManager fTWManager = new FTWManager();
            }

            InitializeComponent();
        }
    }
}
