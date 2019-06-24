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
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace FTWManager.Class
{
    class FTWSelenium
    {

        static IWebDriver driver;
        Actions builder;

        public FTWSelenium()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.SetPreference("browser.download.dir", Path.Combine(Environment.CurrentDirectory, "Download"));
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.useDownloadDir", true);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/csv");
  
            driver = new FirefoxDriver(options);
            builder = new Actions(driver);
          
            driver.Navigate().GoToUrl("http://www.ftw-sim.de:8080/FlyTheWorld/");
            WaitPageLoaded();

            try
            {
                driver.FindElement(By.Name("j_security_check:j_username")).SendKeys("Germanair");
            }
            catch (Exception e)
            {
                MessageBox.Show("FTW Loggin Seite konnte die Benutzername Textbox nicht gefunden werden         " + e);
                return;
            }

            try
            {
                driver.FindElement(By.Name("j_security_check:j_password")).SendKeys("737");
            }
            catch (Exception e)
            {
                MessageBox.Show("FTW Loggin Seite konnte die Passwort Textbox nicht gefunden werden         " + e);
                return;
            }


        }


        public void GetAssignmentsByAirport(string departureICAO,int flag = 0)
        {
            List<AssignmentsFromDeparture> summaryWindowsAssignmentList = new List<AssignmentsFromDeparture>();

            if (driver.PageSource != "http://www.ftw-sim.de:8080/FlyTheWorld/users/assignments_meineAuftraege.xhtml?jftfdi=&jffi=%2Fusers%2Fassignments_meineAuftraege.xhtml")
            {
                driver.Navigate().GoToUrl("http://www.ftw-sim.de:8080/FlyTheWorld/users/assignments.xhtml?jftfdi=&jffi=%2Fusers%2Fassignments.xhtml");
              
            }

            //git den icao  ein
            try
            {
                driver.FindElement(By.Name("frm_daten:favoriten_editableInput")).Clear();
                Wait();
                driver.FindElement(By.Name("frm_daten:favoriten_editableInput")).SendKeys(departureICAO);                          
            }
            catch (Exception e)
            {
                MessageBox.Show("In der Auftragsplannung konnte die Textbox Abflug_ICAO nicht gefunden werden      " + e);
                return ;
            }
            try
            {
                Wait();
                driver.FindElement(By.Name("frm_daten:j_idt138")).Click();

                if(flag == 1)
                {
                    driver.FindElement(By.Id("frm_daten:assignement_Table:0:logo"), 20);
                }
                else
                {
                    Wait();
                }
               

            }
            catch (Exception e)
            {
               // MessageBox.Show("In der Auftragsplannung konnte die Textbox Abflug_ICAO nicht gefunden werden         " + e);
                return;
            }

            try
            {
                driver.FindElement(By.Id("frm_daten:j_idt144")).Click();
                // Wait();
              
                    Wait();
                



            }
            catch (Exception e)
            {
              //  MessageBox.Show("In der Auftragsplannung konnte die Button sucheICAO nicht gefunden werden         " + e);
                return ;
            }

            try
            {
                driver.FindElement(By.Id("frm_daten:j_idt152")).Click();
            
                
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
            Thread.Sleep(3000);

        }

        public static void Wait2()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);

        }

        private static void WaitPageLoaded()
        {
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

    }

    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            driver.FindElement(By.Name("frm_daten:j_idt138")).Click();
            Thread.Sleep(10000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes(typeof(TargetInvocationException), typeof(NoSuchElementException), typeof(InvalidOperationException));          
            wait.Until(ExpectedConditions.ElementExists(by));

            

            return driver.FindElement(by);
        }
    }

}
