using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Test_AAT.Selectors
{
    public class HeaderSelectors
    {
        [FindsBy(How = How.Id, Using = "logo")]
        public IWebElement SiteLogo { get; set; }

        public string HeaderMenu(string item)
        {
            return string.Format("//li/a[contains(text(),'{0}')]", item);
        }

        [FindsBy(How = How.TagName, Using = "h1")]
        public IWebElement H1PageTitle { get; set; }

        public void CheckH1PageTitleIsShowingAndHasText()
        {
            Assert.IsTrue(H1PageTitle.Displayed && H1PageTitle.Text.Length > 2, "H1 title not showing with text.");
        }
    }
}
