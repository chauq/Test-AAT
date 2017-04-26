using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;
using Test_AAT.Helpers;
using Test_AAT.Selectors;

namespace Test_AAT.StepDefinitions
{
    [Binding]
    public class SharedSteps
    {
        private readonly IWebDriver _driver;

        private readonly SharedSelectors _sharedSelectors;

        public SharedSteps(IWebDriver driver)
        {
            _driver = driver;

            _sharedSelectors = new SharedSelectors();
            PageFactory.InitElements(_driver, _sharedSelectors);
        }


        //[Then(@"Validate H2 page displayed '(.*)'")]
        //public void ThenValidateHPageDisplayed(string pageTitle)
        //{
        //    TaskHelper.ExecuteTask(() =>
        //    {
        //        new WebDriverExtensions(_driver).Hilight(_driver.FindElement(By.XPath(_sharedSelectors.H2PageTitle(pageTitle))));
        //        new WebDriverExtensions(_driver).WaitForPresence(
        //            _driver.FindElement(By.XPath(_sharedSelectors.H2PageTitle(pageTitle))));
        //    });
        //}

        [When(@"Click on link '(.*)'")]
        public void WhenClickOnLink(string link)
        {
            TaskHelper.ExecuteTask(() =>
            {
                new WebDriverExtensions(_driver).WaitForPresence(
                    _driver.FindElement(By.XPath(_sharedSelectors.ATag(link))));
                _driver.FindElement(By.XPath(_sharedSelectors.ATag(link))).Click();
            });
        }

        [Then(@"Validate H(.*) page displayed '(.*)'")]
        public void ThenValidateHPageDisplayed(string hTitle, string pageTitle)
        {
            TaskHelper.ExecuteTask(() =>
            {
                new WebDriverExtensions(_driver).Hilight(_driver.FindElement(By.XPath(_sharedSelectors.HAnyPageTitle(hTitle, pageTitle))));
                new WebDriverExtensions(_driver).WaitForPresence(
                    _driver.FindElement(By.XPath(_sharedSelectors.HAnyPageTitle(hTitle, pageTitle))));
            });
        }
    }
}