using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace HCSeleniumAPI.Models
{
    public class Selenium
    {
        public List<Stock> StocksList { get; set; } = new List<Stock>();
        internal int PostsRemaining { get; set; }

        internal int NumberOfPosts { get; set; }

        private int CountPosts { get; set; }
        public List<(string title, string url)> UrlTuples { get; set; } = new List<(string title, string url)>();
        public string SelectedUrl { get; set; }

        public void GetUrls(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://hotcopper.com.au/discussions/asx---day-trading/?post_view=0");
            IReadOnlyCollection<IWebElement> elementCollection = driver.FindElements(By.ClassName("subject-td"));

            var i = 0;

            foreach (IWebElement webElement in elementCollection)
            {

                if (i < 10)
                {

                    IWebElement link = webElement.FindElement(By.TagName("strong")).FindElement(By.TagName("a"));
                    string threadTitle = link.GetAttribute("title");
                    string href = link.GetAttribute("href");

                    UrlTuples.Add((threadTitle, href));

                    Console.WriteLine(UrlTuples[i].title);
                    Console.WriteLine(UrlTuples[i].url);

                    i++;
                }

            }

            SelectedUrl = UrlTuples[0].url;

        }


        public void GetPostsRemaining(IWebDriver driver)
        {

            driver.Navigate().GoToUrl(SelectedUrl);
            IWebElement element = driver.FindElement(By.ClassName("postsRemaining"));

            string sEle = element.Text;

            string pattern = @"\d";
            Regex rg = new Regex(pattern);

            MatchCollection numGroup = rg.Matches(sEle);

            string numberString = "";
            int i = 0;
            while (i < numGroup.Count)
            {
                numberString += numGroup[i].Value;
                i++;
            }


            NumberOfPosts = int.Parse(numberString);


        }

        //public void GetStocksList(int inputPostNumber, IWebDriver driver)
        public void GetStocksList(IWebDriver driver)
        {

            List<Stock> stocksList = new List<Stock>();
            //if (inputPostNumber != 0)
            //{
            //    PostsRemaining = NumberOfPosts - inputPostNumber;
            //}
            //else PostsRemaining = 0;

            PostsRemaining = NumberOfPosts - 20;

            //PostsRemaining = 0;

            CountPosts = NumberOfPosts;

            while (CountPosts > PostsRemaining)
            {
                Stock stock = new Stock();
                string page = "page-" + CountPosts.ToString();
                string uri = Path.Combine(SelectedUrl, page);
                driver.Navigate().GoToUrl(uri);
                IWebElement element;

                try
                {
                    element = driver.FindElement(By.ClassName("message-text"));
                    string[] strArr1 = element.Text.Split(" ");
                    string[] strArr2 = element.Text.Split("\n");
                    IEnumerable<string> strArr = strArr1.Concat(strArr2).ToArray().Distinct();
                    foreach (string str in strArr)
                    {
                        if (str.Trim().Length == 3)
                        {
                            char[] charArr = str.ToCharArray();
                            bool caseCheck = false;
                            foreach (char k in charArr)
                            {
                                caseCheck = char.IsUpper(k);
                            }

                            if (caseCheck)
                            {
                                if (stock.Name == null)
                                {
                                    stock.Name = str;
                                }
                                else stock.Name += "," + str;
                            }

                        }

                    }

                    stock.Text = element.Text;
                    if (stock.Name != null & stocksList.Contains(stock) == false)
                    {
                        StocksList.Add(stock);
                    }

                }
                catch (NoSuchElementException) { continue; }
                finally { CountPosts--; }
            }

        }
    }
}
