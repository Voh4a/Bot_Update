using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Bot_Update
{
    [TestFixture]
    public class NUnitTest
    {
        MainPage mainPage;

        [OneTimeSetUp]
        public void Start()
        {
            mainPage = new MainPage("https://atata-framework.github.io/atata-sample-app/#!/signin");
        }

        [Test]
        public void _00_Login()
        {
            mainPage.Login("admin@mail.com", "abc123");
        }

        [Test]
        public void _01_CreateUser()
        {
            mainPage.CreateNewUser("Volodia", "Vasylyshyn", "example@mail.to", (Office)1, (Gender)0);
        }
         
        [Test]
        public void _02_VerifyUser()
        {
            mainPage.VerifyUserData("Volodia", "Vasylyshyn", "example@mail.to", (Office)1, (Gender)0);
        }
    }
}
