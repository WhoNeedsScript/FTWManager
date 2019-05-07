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
            CheckXMLFiles();
        }

        public void CheckFolder()
        {
            if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Downloud")))
            {
                DirectoryInfo di = Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Download"));
            }
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Document")))
            {
                DirectoryInfo di = Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Document"));
            }
        }

        public void CheckXMLFiles()
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml")))
            {
                FTWXML ftwXML = new FTWXML();
                ftwXML.createFTWPlaneXML();
                ftwXML = null;
            }
        }
    }
}
