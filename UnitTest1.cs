using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TripAdvisorTests
{
    [TestFixture]
    public class Tests
    {
        private string URL;
        private IWebDriver driver;

        [OneTimeSetUp]
        public void setUpTests()
        {
            URL = "https://pl.tripadvisor.com/";
            driver = new ChromeDriver();
        }

        //[OneTimeTearDown]
        //public void tearDownTest()
        //{
        //    driver.Close();
        //}

        [Test(Description = "Czy to tripadvisor?")]
        public void searchTest_TripAdvisorConfirmed()
        {
            driver.Navigate().GoToUrl(URL);
            Assert.AreSame("https://pl.tripadvisor.com/", URL);
        }

        [Test(Description = "logowanie")]
        public void searchTest_LoginSuccessful()
        {
            IWebElement cookieButton = driver.FindElement(By.("evidon-banner-acceptbutton"));
            cookieButton.Click();

            IWebElement loginButton = driver.FindElement(By.XPath("//*[@id=\"lithium - root\"]/header/div/nav/div[2]/a[3]"));
            loginButton.Click();
            IWebElement emailButton = driver.FindElement(By.XPath("//*[@id=\"ssoButtons\"]/button"));
            emailButton.Click();
            IWebElement emailInput = driver.FindElement(By.Id("regSignIn.emailInput"));
            emailInput.SendKeys("piwniczka30@gmail.com");
            IWebElement passInput = driver.FindElement(By.Id("regSignIn.passwordInput"));
            passInput.SendKeys("zaq1@WSX");
            passInput.Submit();
        
        }
    }
}