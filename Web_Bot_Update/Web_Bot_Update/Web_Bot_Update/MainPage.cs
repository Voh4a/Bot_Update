using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace Web_Bot_Update
{
    class MainPage : Driver
    {
        // Xpath
        const string XPath_B_New = "/html/body/div[@class='container']/div[@class='col-md-10 col-md-offset-1 col-sm-12 col-sm-offset-0']/button[@class='btn btn-default']";
        const string XPath_B_Create = "/html/body[@class='modal-open']/div[@class='container']/div[@class='modal fade in']/div[@class='modal-dialog']/div[@class='modal-content']/div[@class='modal-footer']/button[@class='btn btn-primary']";
        const string XPath_Email = "/html/body/div[@class='container']/div[@class='col-md-8 col-md-offset-2 col-sm-12']/div[@class='summary-container']/div[@class='row details-list']/dl[@class='col-sm-6'][1]/dd";
        const string XPath_Gender = "/html/body/div[@class='container']/div[@class='col-md-8 col-md-offset-2 col-sm-12']/div[@class='summary-container']/div[@class='row details-list']/dl[@class='col-sm-6'][3]/dd";
        const string XPath_Office = "/html/body/div[@class='container']/div[@class='col-md-8 col-md-offset-2 col-sm-12']/div[@class='summary-container']/div[@class='row details-list']/dl[@class='col-sm-6'][2]/dd";
        //Explanation in the property B_View
        string XPath_B_View = "/html/body/div[@class='container']/div[@class='col-md-10 col-md-offset-1 col-sm-12 col-sm-offset-0']/div[@class='table-responsive']/table[@class='table table-hover']/tbody/tr[]/td[@class='actions-column user-actions-column']/div[@class='btn-group btn-group-sm']/a[@class='btn btn-default']";

        public MainPage(string url)
        {
            // Open Chrome
            base.SetChromeDriver();
            // Go to url
            base.Navigate(url);
        }

        // for storing information about office, gender
        private string data;

        // Elements to athorization
        #region Properties Athorization
        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement T_Email { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement T_Password { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value='Sign In']")]
        public IWebElement B_SignIn { get; set; }
        #endregion

        // Elements to create new user
        #region Properties Create New User
        [FindsBy(How = How.XPath, Using = XPath_B_New)]
        public IWebElement B_New { get; set; }

        public IWebElement T_FirstName
        {
            get
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(By.Id("first-name")));
            }
        }

        [FindsBy(How = How.Id, Using = "last-name")]
        public IWebElement T_LastName { get; set; }

        public IWebElement DL_Office
        {
            get
            {
                return webDriver.FindElement(By.Id("office")).FindElement(By.CssSelector("option[value='" + data + "']"));
            }
        }

        public IWebElement RB_Gender
        {
            get
            {
                return webDriver.FindElement(By.CssSelector("input[value='" + data + "']"));
            }
        }

        [FindsBy(How = How.XPath, Using = XPath_B_Create)]
        public IWebElement B_Create { get; set; }
        #endregion

        // Elements to verify the data
        #region Properties Verify User Data
        public IWebElement B_View
        {
            get
            {
                // Get the last number 
                // because after aadding a new user it is placed in the end
                int number = webDriver.FindElement(By.CssSelector("tbody")).FindElements(By.CssSelector("tr")).ToList().Count();

                // Set the number in Xpath
                XPath_B_View = XPath_B_View.Replace("tr[]", "tr["+ number +"]");

                return webDriver.FindElement(By.XPath(XPath_B_View));
            }
        }

        [FindsBy(How = How.CssSelector, Using = "h1[class='text-center']")]
        public IWebElement H_First_Last_Name { get; set; }

        [FindsBy(How = How.XPath, Using = XPath_Email)]
        public IWebElement L_Email { get; set; }

        [FindsBy(How = How.XPath, Using = XPath_Gender)]
        public IWebElement L_Gender { get; set; }

        [FindsBy(How = How.XPath, Using = XPath_Office)]
        public IWebElement L_Office { get; set; }
        #endregion

        public void Login(string email, string password)
        {
            PageFactory.InitElements(webDriver, this);
            T_Email.SendKeys(email);
            T_Password.SendKeys(password);
            B_SignIn.Click();
        }

        public void CreateNewUser(string firstName, string lastName, string email, Office office, Gender gender)
        {
            PageFactory.InitElements(webDriver, this);

            B_New.Click();

            T_FirstName.SendKeys(firstName);

            PageFactory.InitElements(webDriver, this);

            T_LastName.SendKeys(lastName);
            T_Email.SendKeys(email);

            data = office.ToString("g");
            DL_Office.Click();
            data = gender.ToString("g");
            RB_Gender.Click();

            B_Create.Click();
        }

        public void VerifyUserData(string firstName, string lastName, string email, Office office, Gender gender)
        {
            B_View.Click();

            PageFactory.InitElements(webDriver, this);

            // Check First and Last Name
            if (H_First_Last_Name.Text != firstName + " " + lastName)
                throw new Exception("Don't valid First and Last Name");
            // Check Email
            if(L_Email.Text != email)
                throw new Exception("Don't valid Email");
            // Check Gender
            if (L_Gender.Text != gender.ToString("g"))
                throw new Exception("Don't valid Gender");
            // Check Office
            if (L_Office.Text != office.ToString("g"))
                throw new Exception("Don't valid Office");

            // For performing javaScript
            IJavaScriptExecutor js = webDriver as IJavaScriptExecutor;
            // alert message
            js.ExecuteScript("alert('Everything Good! User is verified')");

        }
    }
}
