using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using FTWManager.Type;

namespace FTWManager.Class
{
    class FTWXML
    {

        public void createFTWPlaneXML()
        {

            XDocument xDocument = new XDocument(
               new XDeclaration("1.0", "UTF-16", "yes"),
               new XElement("FTWPlane", null)
               );

            xDocument.Save(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml"));
        }

        public List<Plane> readFTWPlaneXML()
        {
            List<Plane> planes = new List<Plane>();

            foreach (XElement xPlane in XElement.Load(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml")).Elements("Plane"))
            {
                Plane plane = new Plane();

                plane.Name = xPlane.Element("Name").Value;
                plane.EconemySeats = Convert.ToInt16(xPlane.Element("EconemySeats").Value);
                plane.BusinessSeats = Convert.ToInt16(xPlane.Element("BusinessSeats").Value);
                plane.Cargo = Convert.ToInt16(xPlane.Element("Cargo").Value);
                plane.Payloud = Convert.ToInt16(xPlane.Element("Payloud").Value);

                planes.Add(plane);
            }


            return planes;
        }


        public void writeAddPlane(string _Name, int _EconemySeats, int _BusinessSeats, int _Cargo, int _Payloud)
        {
            XDocument xDocument = XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml"));
            XElement plane = xDocument.Element("FTWPlane");
            plane.Add(
                new XElement("Plane",
                    new XElement("Name", _Name),
                    new XElement("EconemySeats", _EconemySeats),
                    new XElement("BusinessSeats", _BusinessSeats),
                    new XElement("Cargo", _Cargo),
                    new XElement("Payloud", _Payloud)


                    )
                );
            //xDocument.Element("FTWPlane").Add(plane);

            xDocument.Save(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml"));
        }

        public void EditPlane(string _Name, int _EconemySeats, int _BusinessSeats, int _Cargo, int _Payloud)
        {
            XDocument xDocument = XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml"));

            var items = (from item in xDocument.Descendants("Plane")
                        where item.Element("Name").Value == _Name
                        select item).ToList();

            foreach (XElement itemElement in items)
            {
                if(itemElement.Element("EconemySeats").Value != _EconemySeats.ToString())
                {
                    itemElement.SetElementValue("EconemySeats", _EconemySeats);
                }

                if(itemElement.Element("BusinessSeats").Value != _BusinessSeats.ToString())
                {
                    itemElement.SetElementValue("BusinessSeats", _BusinessSeats);
                }

                if (itemElement.Element("Cargo").Value != _Cargo.ToString())
                {
                    itemElement.SetElementValue("Cargo", _Cargo);
                }

                if (itemElement.Element("Payloud").Value != _Payloud.ToString())
                {
                    itemElement.SetElementValue("Payloud", _Payloud);
                }
            }
            xDocument.Save(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml"));
        }

        public void deletePlane(string _Name)
        {
            XDocument xDocument = XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Document/FTWPlane.xml"));
        }
    }
}
