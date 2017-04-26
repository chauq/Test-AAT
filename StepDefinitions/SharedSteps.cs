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


        [Then(@"Validate H2 page displayed '(.*)'")]
        public void ThenValidateHPageDisplayed(string pageTitle)
        {
            TaskHelper.ExecuteTask(() => new WebDriverExtensions(_driver).WaitForPresence(_driver.FindElement(By.XPath(_sharedSelectors.H2PageTitle(pageTitle)))));
        }
    }
}