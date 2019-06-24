using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FTWManager.Type;

namespace FTWManager.Class
{
    class FTWJobMarket
    {
        FTWSelenium ftwSelenium;
        FTWCSV ftwCSV;


        public FTWJobMarket()
        {
            ftwSelenium = new FTWSelenium();
            ftwCSV = new FTWCSV();

        }



        public Trip GetBestTrip(string departure, bool withPlaneLoad = false, Plane _plane = null, int maxArrivals = 3, int maxHops = 3)
        {
            //Rückgabewert Bester Trip
            Trip bestTrip = new Trip();

            List<Trip> listTrip = new List<Trip>();
            List<AssignmentsFromDeparture> listAssignmentsFromDepartures = new List<AssignmentsFromDeparture>();

            //holt sich die Assignemts CSV-Datei vom Startflughafen
            ftwSelenium.GetAssignmentsByAirport(departure);

            //liest die CSV Datei von dem StartflughafenFTWJobsRoute
            ftwCSV.ReadAssignmentCSV(ref listAssignmentsFromDepartures);

            while (listAssignmentsFromDepartures.Count() == 0)
            {
                ftwSelenium.GetAssignmentsByAirport(departure,1);

                //liest die CSV Datei von dem StartflughafenFTWJobsRoute
                ftwCSV.ReadAssignmentCSV(ref listAssignmentsFromDepartures);
            }

            //Sortiert alle Aufträge eines Flughafens nacht Typ und Ammount um die Beladung des Flugzeuges Wenn ausgeählt besser und einfacher läuft
            foreach (AssignmentsFromDeparture tempassignmentsFromDeparture in listAssignmentsFromDepartures)
            {
                tempassignmentsFromDeparture.OrderByTypeAndAmount();
            }


            if (withPlaneLoad == true)
            {
                foreach (AssignmentsFromDeparture assignmentsFromDeparture in listAssignmentsFromDepartures)
                {
                    _plane.reset();
                    foreach (Assignment assignment in assignmentsFromDeparture.ListAssignments)
                    {
                        if (_plane.LoadAssignment(assignment) == false && assignment.Type == 3)
                        {


                            break;
                        }
                    }
                    assignmentsFromDeparture.TotalEconomyPax = _plane.occupiedSeats;
                    assignmentsFromDeparture.TotalEconomPaxMoney = _plane.paxMoney;
                    assignmentsFromDeparture.TotalCargo = _plane.occupiedCargo;
                    assignmentsFromDeparture.TotalCargoMoney = _plane.cargoMoney;
                    assignmentsFromDeparture.ListAssignments = _plane.load;
                }
            }


            // Müll Variable um die besten 3 vom start flughafen zu bekommen 
            Trip tempTrrp = new Trip();
            tempTrrp.Hop = listAssignmentsFromDepartures;
            tempTrrp.OrderArrivalsByTotalMoney(maxArrivals);
            //

            //


            //Alle Trips Obejte die Benötigt werden Für Alle Routen Werden Erstellt
            foreach (AssignmentsFromDeparture startHop in tempTrrp.Hop)
            {

                for (int i = 0; i < Math.Pow(maxArrivals, maxHops - 1); i++)
                {
                    Trip temp = new Trip();

                    temp.Hop.Add(startHop);
                    listTrip.Add(temp);
                }

            }



            for (int aktuellerhop = 0; aktuellerhop < 2; aktuellerhop++)
            {
                // hier werden die 2 Hops hinzugefügt
                int momentan = 0;

                for (int i = 0; i < listTrip.Count; i += 9)
                {
                    listAssignmentsFromDepartures.Clear();


                    ftwSelenium.GetAssignmentsByAirport(listTrip[i].Hop[aktuellerhop].ArrivalICAO);

                    //liest die CSV Datei von dem StartflughafenFTWJobsRoute
                    ftwCSV.ReadAssignmentCSV(ref listAssignmentsFromDepartures);

                    // Anti Steffan Abfrage timer Solange bis Assignnets Zurpck gegeben werden
                    while (listAssignmentsFromDepartures.Count() == 0)
                    {
                        ftwSelenium.GetAssignmentsByAirport(listTrip[i].Hop[aktuellerhop].ArrivalICAO,1);

                        //liest die CSV Datei von dem StartflughafenFTWJobsRoute
                        ftwCSV.ReadAssignmentCSV(ref listAssignmentsFromDepartures);
                    }


                    //erstellt den trip
                    foreach (AssignmentsFromDeparture tempassignmentsFromDeparture in listAssignmentsFromDepartures)
                    {
                        tempassignmentsFromDeparture.OrderByTypeAndAmount();
                    }


                    if (withPlaneLoad == true)
                    {
                        foreach (AssignmentsFromDeparture assignmentsFromDeparture in listAssignmentsFromDepartures)
                        {
                            _plane.reset();
                            foreach (Assignment assignment in assignmentsFromDeparture.ListAssignments)
                            {
                                if (_plane.LoadAssignment(assignment) == false && assignment.Type == 3)
                                {


                                    break;
                                }
                            }
                            assignmentsFromDeparture.TotalEconomyPax = _plane.occupiedSeats;
                            assignmentsFromDeparture.TotalEconomPaxMoney = _plane.paxMoney;
                            assignmentsFromDeparture.TotalCargo = _plane.occupiedCargo;
                            assignmentsFromDeparture.TotalCargoMoney = _plane.cargoMoney;
                            assignmentsFromDeparture.ListAssignments = _plane.load;
                        }
                    }


                    // Müll Variable um die besten 3 vom start flughafen zu bekommen y
                    tempTrrp = new Trip();
                    tempTrrp.Hop = listAssignmentsFromDepartures;
                    tempTrrp.OrderArrivalsByTotalMoney(maxArrivals);
                    //

                    for (int a = 0; a < tempTrrp.Hop.Count; a++, momentan++)
                    {

                        if (aktuellerhop == 0)
                        {

                            for (int b = 0; b < 3; b++, momentan++)
                            {

                              

                                listTrip[momentan].Hop.Add(tempTrrp.Hop[a]);

                            }
                            momentan -= 1;
                        }
                        else
                        {
                            int y = listTrip[momentan].Hop.Count();
                            listTrip[momentan].Hop.Add(tempTrrp.Hop[a]);

                            if (y >= listTrip[momentan].Hop.Count())
                            {
                                bool s = true;
                            }
                        }
                    }


                    if (aktuellerhop != 0)
                    {
                        i -= 6;
                    }
                }
               
            }

            foreach(Trip trip in listTrip)
            {
                if(trip.Hop[0].ArrivalICAO != trip.Hop[1].DepartureICAO)
                {
                    int a = 0;
                }

                if (trip.Hop[1].ArrivalICAO != trip.Hop[2].DepartureICAO)
                {
                    int a = 0;
                }
            }
            
            /// Muss noch getestet werden ob es funktioniert
            /// // Hier soll der Trip Herausgesuh
            /// cht werden der am meisten Geld gibt
            foreach(Trip findBestTrip in listTrip)
            {
                if(bestTrip == null)
                {
                    bestTrip = findBestTrip;
                }
                else
                {
                    if(bestTrip.getMoney() < findBestTrip.getMoney())
                    {
                        bestTrip = findBestTrip;
                    }
                }
            }

            return bestTrip;

        }


        public void LoadPlane()
        {

        }


    }
}
