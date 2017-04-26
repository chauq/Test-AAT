namespace Test_AAT.Selectors
{
    public class SharedSelectors
    {
        public string H1PageTitle(string title)
        {
            return string.Format("//h1[contains(text(),'{0}')]", title);
        }

        public string H2PageTitle(string title)
        {
            return string.Format("//h2[contains(text(),'{0}')]", title);
        }
    }
}
