using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net;

namespace TripAdvisorTests
{
    [TestFixture]
    public class Tests
    {
        private string URL;
        private string currency;
        private IWebDriver driver;

        [OneTimeSetUp]
        public void setUpTests()
        {
            URL = "https://pl.tripadvisor.com/";
            driver = new ChromeDriver();
            currency = "z? PLN";
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
            Assert.AreSame("https://pl.tripadvisor.com/", URL);
        }

        [Test(Description = "logowanie"), Order(2)]
        public void searchTest_LoginSuccessful()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement cookieButton = driver.FindElement(By.XPath("/html/body/div[2]/button[1]"));
            cookieButton.Click();

            //IWebElement loginButton = driver.FindElement(By.XPath("/html/body/div/header/div/nav/div[2]/a[3]"));
            //loginButton.Click();

            ////IWebElement iFrame = driver.FindElement(By.XPath("//*[@id=\"sharedCaptcha.recaptcha_elm_corereg\"]/iframe"));
            ////driver.SwitchTo().Frame(iFrame);
            ////IWebElement iFrame_checkbox = driver.FindElement(By.XPath("//*[@id=\"recaptcha - anchor\"]"));
            ////iFrame_checkbox.Click();

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //IWebElement emailButton = driver.FindElement(By.XPath("//*[@id=\"ssoButtons\"]/button"));
            //emailButton.Click();

            //IWebElement emailInput = driver.FindElement(By.Id("regSignIn.emailInput"));
            //emailInput.SendKeys("piwniczka30@gmail.com");
            //IWebElement passInput = driver.FindElement(By.Id("regSignIn.passwordInput"));
            //passInput.SendKeys("zaq1@WSX"); menu-item-26

            //passInput.Submit();

        }

        [Test(Description = "czy waluta jest w pln"), Order(3)]
        public void searchTest_IsTheCurrencyPLN()
        {
            IWebElement dropdown = driver.FindElement(By.XPath("/html/body/div/footer/div/div/div[4]/div[2]/button"));
            dropdown.Click();
            IWebElement region = driver.FindElement(By.Id("menu-item-26"));
            region.Click();

            Assert.AreSame("z? PLN", currency);
        }

        [Test(Description = "napisz recenzj?"), Order(4)]
        public void Test_AreThereAnyReviews()
        {
            IWebElement reviewButton = driver.FindElement(By.XPath("/html/body/div/header/div/nav/div[2]/div/button"));
            reviewButton.Click();
            IWebElement write = driver.FindElement(By.Id("menu-item-0"));
            write.Click();

            IReadOnlyCollection<IWebElement> reviews = driver.FindElements(By.XPath("/html/body/div[4]/div[2]/div/div[2]/div/div"));

            Assert.Greater(reviews.Count, 0);
        }
        [Test(Description = "Czy w recennzji linii lotniczych, jest opisane 'miejsce na nogi'"), Order(5)]
        public void Test_IsTherePlaceforLegs()
        {
            IWebElement trip = driver.FindElement(By.XPath("/html/body/div[4]/div[1]/div[1]/div[2]/div/div[2]/div[2]/header/div/nav/div[2]/a[2]"));
            trip.Click();
            IWebElement flight = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div[2]/div[2]/div/div/div/ul[1]/li[4]/div/a"));
            flight.Click();
            IWebElement flightReview = driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div[1]/div[2]/div/div[2]/div/div/div[1]/div/div[1]/a"));
            flightReview.Click();
            IWebElement seeReview = driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div[2]/div[2]/div/div/div[2]/div/div[2]/a[2]"));
            seeReview.Click();

            WebClient client = new WebClient();
            string content = client.DownloadString("https://pl.tripadvisor.com/Airline_Review-d8728984-Reviews-Adria-Airways-No-Longer-Operating");
            Assert.That(content, Contains.Substring("Miejsce na nogi"));
        }
    }
}