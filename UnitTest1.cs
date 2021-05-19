using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

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

        [Test(Description = "Czy to tripadvisor?"), Order(1)]
        public void searchTest_TripAdvisorConfirmed()
        {
            driver.Navigate().GoToUrl(URL);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            IWebElement cookieButton = driver.FindElement(By.XPath("/html/body/div[2]/button[1]")); //Z jakiegoœ powodu, xpath skrótowy nie dzia³a + musia³em daæ timespan, nie wiem jak u ciebie, ale u mnie inf. o cookie nie wyœwietla³a siê wystarczaj¹co szybko
            cookieButton.Click();

            Assert.AreSame("https://pl.tripadvisor.com/", URL);
        }
        [Test(Description = "Does top place contains obligatory attractions?"), Order(2)]
        public void searchTest_ObligatoryAttractionsExist()
        {
            driver.Navigate().GoToUrl(URL);

            driver.FindElement(By.XPath("/html/body/div/main/div[4]/div[1]/div/div[2]/div/div[1]/div/ul/li[1]/a")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IReadOnlyCollection<IWebElement> attractions = driver.FindElements(By.XPath("/html/body/div/main/div[6]/div[2]/div/div[2]/div/div[1]/div/ul/li/div[1]/a/div[1]/div[1]/div/img"));
            Assert.IsTrue(attractions.Count > 1);
        }
        [Test(Description ="Look for hotel nearby with additional filters"),Order(3)]
        public void searchTest_HotelFound()
        {
            driver.Navigate().GoToUrl(URL);

            driver.FindElement(By.ClassName("_1ulyogkG")).Click(); //Hotels
            driver.FindElement(By.ClassName("_1c2ocG0l")).Click(); //NearbyButton

            driver.FindElement(By.XPath("/html/body/div[4]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[3]/div[3]/div[2]")).Click(); //dateIn
            driver.FindElement(By.XPath("html/body/div[4]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[3]/div[3]/div[3]")).Click(); //dateOut
            //driver.FindElement(By.CssSelector("ui_button primary fullwidth")).Submit();

            //TBC
        }
        [Test(Description = "Does searchbar searches for typed in location"),Order(4)]
        public void searchTest_SearchSuccessfull()
        {
            driver.Navigate().GoToUrl(URL);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement searchInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div[2]/form/input[1]")));
            searchInput.SendKeys("Meksyk");
            searchInput.Submit();

            string searchTitle = driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div[1]/div/div[1]/div/div[3]/div/div[1]/div/div[1]/div/div/div/span")).Text;
            IReadOnlyCollection<IWebElement> searchResults = driver.FindElements(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div[1]/div/div[1]/div/div[3]/div/div[1]/div/div[2]/div/div/div"));
            Assert.IsTrue(searchTitle.Contains("Meksyk") && searchResults.Count > 1);
        }
        //[Test, Order(5)]
        //public void searchTest_LoginSuccessful()
        //{
        //    IWebElement loginButton = driver.FindElement(By.XPath("/html/body/div/header/div/nav/div[2]/a[3]"));
        //    loginButton.Click();
            
        //    driver.SwitchTo().Frame(driver.FindElement(By.ClassName("_30pzQStV")));
        //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        //    var emailButton = driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[3]/div/div[1]/button")); //Powodzenie znalezienia emailButton zale¿y tylko i wy³¹cznie od tego, czy reCAPTCHA siê pojawi (XD!) no i od iFrame

        //    IWebElement emailInput = driver.FindElement(By.Id("regSignIn.emailInput"));
        //    emailInput.SendKeys("piwniczka30@gmail.com");
        //    IWebElement passInput = driver.FindElement(By.Id("regSignIn.passwordInput"));
        //    passInput.SendKeys("zaq1@WSX");
        //    passInput.Submit(); 

        //}
    }
}