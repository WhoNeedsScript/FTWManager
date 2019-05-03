using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FTWManager.Class
{
    class FTWCheckResources
    {
        public FTWCheckResources()
        {
            CheckFolder();
        }

        public void CheckFolder()
        {
            if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Downloud")))
            {
                DirectoryInfo di = Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Downloud"));
            }
        }
    }
}
