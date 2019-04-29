using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using FTWManager.Type;
using System.Windows;
using System.Threading;

namespace FTWManager.Class
{
    class FTWSelenium
    {

        static IWebDriver driver = new FirefoxDriver();

        public FTWSelenium()
        {
            driver.Navigate().GoToUrl("http://www.ftw-sim.de:8080/FlyTheWorld/");
            WaitPageLoaded();

            try
            {
                driver.FindElement(By.Name("j_security_check:j_username")).SendKeys("WhoNeedsSkill");
            }
            catch (Exception e)
            {
                MessageBox.Show("FTW Loggin Seite konnte die Benutzername Textbox nicht gefunden werden         " + e);
                return;
            }

            try
            {
                driver.FindElement(By.Name("j_security_check:j_password")).SendKeys("WhoNeedsSkill1234");
            }
            catch (Exception e)
            {
                MessageBox.Show("FTW Loggin Seite konnte die Passwort Textbox nicht gefunden werden         " + e);
                return;
            }


        }


        public List<SummaryWindowsAssignment> getSummaryWindowsAssignment(string departureICAO)
        {
            List<SummaryWindowsAssignment> summaryWindowsAssignmentList = new List<SummaryWindowsAssignment>();

            if (driver.PageSource != "http://www.ftw-sim.de:8080/FlyTheWorld/users/assignments_meineAuftraege.xhtml?jftfdi=&jffi=%2Fusers%2Fassignments_meineAuftraege.xhtml")
            {
                driver.Navigate().GoToUrl("http://www.ftw-sim.de:8080/FlyTheWorld/users/assignments.xhtml?jftfdi=&jffi=%2Fusers%2Fassignments.xhtml");
                Wait();
            }





            //git den icao  ein
            try
            {
                driver.FindElement(By.Name("frm_daten:favoriten_editableInput")).SendKeys(departureICAO);
            }
            catch (Exception e)
            {
                MessageBox.Show("In der Auftragsplannung konnte die Textbox Abflug_ICAO nicht gefunden werden         " + e);
                return null;
            }

            try
            {
                driver.FindElement(By.Id("frm_daten:j_idt138")).Click();
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                MessageBox.Show("In der Auftragsplannung konnte den Suchbutton Abflug_ICAO nicht gefunden werden         " + e);
                return null;
            }

            try
            {
                driver.FindElement(By.Id("frm_daten:selto")).Click();
                Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show("In der Auftragsplannung konnte den Such ComboBox Ziel_ICAO nicht gefunden werden         " + e);
                return null;
            }

            int amountOfDestinations;

            try
            {
                amountOfDestinations = driver.FindElement(By.Id("frm_daten:selto_items")).FindElements(By.TagName("li")).Count - 1;
                
            }

            catch (Exception e)
            {

                MessageBox.Show("In der Auftragsplannung konnte die Ziele in der ListBox Ziel_ICAO nicht gefunden werden         " + e);

                return null;
            }



            for (int i = 1; i <= amountOfDestinations; i++)
            {
                SummaryWindowsAssignment summaryWindowsAssignment = new SummaryWindowsAssignment();

                summaryWindowsAssignment.DepartureICAO = departureICAO;

                summaryWindowsAssignment.ArrivalICAO = driver.FindElement(By.Id("frm_daten:selto_items")).FindElements(By.TagName("li"))[i].Text;
                Wait();
                driver.FindElement(By.Id("frm_daten:selto_items")).FindElements(By.TagName("li"))[i].Click();
                Wait();

                try
                {
                    driver.FindElement(By.Id("frm_daten:j_idt144")).Click();
                    Thread.Sleep(3000);
                }

                catch (Exception e)
                {

                    MessageBox.Show("In der Auftragsplannung konnte der Jobs Suchen Button nicht gefunden werden         " + e);

                    return null;
                }
                try
                {
                    summaryWindowsAssignment.EconomyPax = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[1].FindElements(By.TagName("td"))[1].Text;
                    summaryWindowsAssignment.EconomPaxMoney = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[1].FindElements(By.TagName("td"))[2].Text;

                    summaryWindowsAssignment.BusinessPax = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[2].FindElements(By.TagName("td"))[1].Text;
                    summaryWindowsAssignment.BusinessPaxMoney = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[2].FindElements(By.TagName("td"))[2].Text;

                    summaryWindowsAssignment.CargoMoney = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[5].FindElements(By.TagName("td"))[2].Text;
                    summaryWindowsAssignment.Cargo = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[5].FindElements(By.TagName("td"))[3].Text;

                    summaryWindowsAssignment.Gesamtwert = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[6].FindElements(By.TagName("td"))[2].Text;
                    summaryWindowsAssignment.GesamtwertGewicht = driver.FindElement(By.Id("frm_Summen:j_idt480")).FindElements(By.TagName("tr"))[6].FindElements(By.TagName("td"))[3].Text;

                    summaryWindowsAssignmentList.Add(summaryWindowsAssignment);
                    driver.FindElement(By.Id("frm_daten:selto")).Click();
                }
                catch (Exception e)
                {
                    MessageBox.Show(""+ e);
                    return null;
                }
                

            }

            return summaryWindowsAssignmentList;
        }

        public static void Wait()
        {
            Thread.Sleep(2000);
        }

        private static void WaitPageLoaded()
        {
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

    }
}
