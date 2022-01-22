using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ForumScraper
{
    public class Selenium
    {

        public List<Stocks> getStocksList(string path, int posts, int numberOfPosts)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            IWebDriver driver = new ChromeDriver(chromeOptions);

            List<Stocks> stocksList = new List<Stocks>();
            if (numberOfPosts != 0)
            {
                numberOfPosts = posts - numberOfPosts;
            }            
            var i = posts;
            //while (i < posts)
            while (i > numberOfPosts)
            {
                Stocks stock = new Stocks();
                string page = "page-" + i.ToString();
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
                        stocksList.Add(stock);
                    }

                }
                catch (NoSuchElementException) { continue; }
                finally { i--; }    
            }
            driver.Close();

            return stocksList;
        }

        public int getPostsRemaining(string path)
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
            driver.Close();
            return int.Parse(numberString);
        }
    }

    
}
