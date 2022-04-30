using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ForumScraper
{
    public class Selenium
    {
        public List<Stock> StocksList { get; set; } = new List<Stock>();
        public int PostsRemaining { get; set; }

        public int NumberOfPosts { get; set; }

        public int CountPosts { get; set; }

        public Selenium()
        {

        }

        public void GetPostsRemaining(string path)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            IWebDriver driver = new ChromeDriver(chromeOptions);

            driver.Navigate().GoToUrl(path);
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

            driver.Close();
        }

        public void GetStocksList(string path, int inputPostNumber)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            IWebDriver driver = new ChromeDriver(chromeOptions);

            List<Stock> stocksList = new List<Stock>();
            if (inputPostNumber != 0)
            {
                PostsRemaining = NumberOfPosts - inputPostNumber;
            }
            else PostsRemaining = 0;

            CountPosts = NumberOfPosts;

            while (CountPosts > PostsRemaining)
            {
                Stock stock = new Stock();
                string page = "page-" + CountPosts.ToString();
                string uri = Path.Combine(path, page);
                driver.Navigate().GoToUrl(uri);
                IWebElement element;

                try
                {
                    element = driver.FindElement(By.ClassName("message-text"));
                    string[] strArr = element.Text.Split(" ");
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

            driver.Close();
        }

    }


}
