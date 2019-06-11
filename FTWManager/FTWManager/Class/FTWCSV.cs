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

        public void ReadAssignmentCSV(ref List<AssignmentsFromDeparture> refSummaryAssignments)
        {
            try
            {
                reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Download/Auftragsuebersicht.csv"));
                bool found;
                bool firstLine = true;

                string line;


                while ((line = reader.ReadLine()) != null)
                {
                    if (firstLine == true)
                    {
                        firstLine = false;
                        continue;
                    }

                    found = false;
                    string[] temp = line.Replace('"', ' ').Replace('€', ' ').Replace('k', ' ').Replace('g', ' ').Replace('.', ' ').Replace(" ", string.Empty).Split(',');

                    //Erstellt Assignment aus CSV Zeile
                    Assignment assignment = new Assignment();

                    assignment.Departure = temp[1].Replace(" ", string.Empty);
                    assignment.Arrival = temp[3].Replace(" ", string.Empty);

                    if (temp[7] != "-")
                    {
                        assignment.Type = 1;

                        assignment.paxWeight = Convert.ToInt16(temp[7].Replace(" ", string.Empty));
                        assignment.paxCargo = Convert.ToInt16(temp[8].Replace(" ", string.Empty));
                    }
                    else
                    {
                        assignment.Type = 3;
                    }

                    assignment.Amount = Convert.ToInt16(temp[4]);
                    assignment.Money = Convert.ToDouble(temp[9]);

                    //////////////////////////////////////////////////////////////////7

                    foreach (AssignmentsFromDeparture tempSummaryAssignment in refSummaryAssignments)
                    {
                        if (tempSummaryAssignment.ArrivalICAO == temp[3])
                        {
                            tempSummaryAssignment.addAssignment(assignment);

                            found = true;
                            break;
                        }
                    }

                    if (found == false)
                    {
                        AssignmentsFromDeparture summaryAssignment = new AssignmentsFromDeparture();

                        summaryAssignment.DepartureICAO = temp[1].Replace(" ", string.Empty);
                        summaryAssignment.ArrivalICAO = temp[3].Replace(" ", string.Empty);

                        summaryAssignment.addAssignment(assignment);

                        refSummaryAssignments.Add(summaryAssignment);
                    }

                }

                reader.Close();
                DeleteAuftragsuebersicht();
            }
            catch
            {
                return;
            }
            



        }

        public void DeleteAuftragsuebersicht()
        {
            File.Delete(Path.Combine(Environment.CurrentDirectory, "Download/Auftragsuebersicht.csv"));
        }
    }














    /*public void ReadAssignmentCSV(ref List<SummaryAssignment> refSummaryAssignments)
    {
        reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Downloud/Auftrasuebersicht.csv"));
        bool found;
        bool firstLine = true;

        string line;


        while ((line = reader.ReadLine()) != null)
        {
            if (firstLine == true)
            {
                firstLine = false;
                continue;
            }

            found = false;
            string[] temp = line.Replace('"', ' ').Replace('€', ' ').Replace(" ", string.Empty).Split(',');




            foreach (SummaryAssignment tempSummaryAssignment in refSummaryAssignments)
            {
                if (tempSummaryAssignment.ArrivalICAO == temp[3])
                {
                    if (temp[7] != "-")
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
                    break;
                }
            }

            if (found == false)
            {
                SummaryAssignment summaryAssignment = new SummaryAssignment();

                summaryAssignment.DepartureICAO = temp[1].Replace(" ", string.Empty);
                summaryAssignment.ArrivalICAO = temp[3].Replace(" ", string.Empty);

                // ob cargo doer pax
                if (temp[7] != "-")
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
    }*/
}
