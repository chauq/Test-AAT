namespace Test_AAT.Selectors
{
    public class SharedSelectors
    {
        public string ATag(string link)
        {
            return string.Format("//a[contains(text(),'{0}')]", link);
        }

        public string H1PageTitle(string title)
        {
            return string.Format("//h1[contains(text(),'{0}')]", title);
        }

        public string H2PageTitle(string title)
        {
            return string.Format("//h2[contains(text(),'{0}')]", title);
        }

        public string HAnyPageTitle(string hTitle, string title)
        {
            return string.Format("//h{0}[contains(text(),'{1}')]", hTitle, title);
        }
    }
}
