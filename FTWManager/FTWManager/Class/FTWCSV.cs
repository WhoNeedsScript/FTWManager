using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using FTWManager.Type;

namespace FTWManager.Class
{
    class FTWCSV
    {
        StreamReader reader;

        public FTWCSV()
        {
           
        }

        public void ReadAssignmentCSV(ref List<SummaryAssignment> refSummaryAssignments)
        {
            reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Downloud/Auftrasuebersicht.csv"));
            bool found;
            bool firstLine = true;

            string line;

           
            while ((line = reader.ReadLine()) != null)
            {
                if(firstLine == true)
                {
                    firstLine = false;
                    continue;
                }

                found = false;
                string[] temp = line.Replace('"',' ').Replace('€',' ').Split(',');


               

                foreach(SummaryAssignment tempSummaryAssignment in refSummaryAssignments)
                {
                    if(tempSummaryAssignment.ArrivalICAO == temp[3])
                    {
                        if (temp[7] !=" - ")
                        {
                            tempSummaryAssignment.EconomyPax += Convert.ToInt16(temp[4]);
                            tempSummaryAssignment.EconomPaxMoney += Convert.ToDouble(temp[9]);

                        }
                        else
                        {
                            tempSummaryAssignment.Cargo += Convert.ToInt16(temp[4]);
                            tempSummaryAssignment.CargoMoney += Convert.ToDouble(temp[9]);

                        }

                        found = true;
                    }
                }
                
                if(found == false)
                {
                    SummaryAssignment summaryAssignment = new SummaryAssignment();

                    summaryAssignment.DepartureICAO = temp[1];
                    summaryAssignment.ArrivalICAO = temp[3];

                    // ob cargo doer pax
                    if(temp[7] != " - ")
                    {
                        summaryAssignment.EconomyPax = Convert.ToInt16(temp[4]);
                        summaryAssignment.EconomPaxMoney = Convert.ToDouble(temp[9]);
                      
                    }
                    else
                    {
                        summaryAssignment.Cargo = Convert.ToInt16(temp[4]);
                        summaryAssignment.CargoMoney = Convert.ToDouble(temp[9]);
                        
                    }
                    refSummaryAssignments.Add(summaryAssignment);
                }

            }

            reader.Close();
            File.Delete(Path.Combine(Environment.CurrentDirectory, "Downloud/Auftrasuebersicht.csv"));
        }
    }
}
