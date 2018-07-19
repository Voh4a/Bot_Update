using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Bot_Update
{
    class Driver
    {
        protected IWebDriver webDriver;
        protected WebDriverWait wait; // wait for the element

        public void SetChromeDriver()
        {
            webDriver = new ChromeDriver();
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
        }

        public void Navigate(string url)
        {
            if (webDriver != null)
                webDriver.Navigate().GoToUrl(url);
            else throw new NullReferenceException("webDriver is null");
        }
    }
}
