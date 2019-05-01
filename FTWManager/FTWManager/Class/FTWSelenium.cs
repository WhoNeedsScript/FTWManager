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
using OpenQA.Selenium.Interactions;
using System.IO;

namespace FTWManager.Class
{
    class FTWSelenium
    {

        static IWebDriver driver;
        Actions builder;

        public FTWSelenium()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.SetPreference("browser.download.dir", Path.Combine(Environment.CurrentDirectory, "Downloud"));
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.useDownloadDir", true);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/csv");
  
            driver = new FirefoxDriver(options);
            builder = new Actions(driver);
          
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


        public void getAssignmentsByAirport(string departureICAO)
        {
            List<SummaryAssignment> summaryWindowsAssignmentList = new List<SummaryAssignment>();

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
                return ;
            }
            try
            {
                driver.FindElement(By.Name("frm_daten:j_idt138")).Click();
                Thread.Sleep(2000);
                Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show("In der Auftragsplannung konnte die Textbox Abflug_ICAO nicht gefunden werden         " + e);
                return;
            }

            try
            {
                driver.FindElement(By.Id("frm_daten:j_idt144")).Click();
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                MessageBox.Show("In der Auftragsplannung konnte die Button sucheICAO nicht gefunden werden         " + e);
                return ;
            }

            try
            {
                driver.FindElement(By.Id("frm_daten:j_idt152")).Click();
                Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show("In der Auftragsplannung konnte CSV Downloud button nicht gefunden werden         " + e);
                return ;
            }

            return ;
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
