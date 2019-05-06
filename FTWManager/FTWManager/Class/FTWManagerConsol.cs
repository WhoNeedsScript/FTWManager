using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using FTWManager.Pages;

namespace FTWManager.Class
{
    class FTWManagerConsol
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();


        bool run = true;
        int selection;

        public void OpenConsol(FTWJobMarketPage _ftwJobMarketPage)
        {
            AllocConsole();

            while(run == true)
            {
                



               // switch()
            }

            FreeConsole();
        }

        public void OpenConsol(FTWPlanePage _ftwPlanePage)
        {

        }

    }
}
