using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace ForumScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            

            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            chromeOptions.AddArgument("headless");
            //chromeOptions.BinaryLocation = "/user/bin/google-chrome";
            IWebDriver driver = new ChromeDriver(chromeOptions);
            //IWebDriver driver = new RemoteWebDriver(new Uri("http://localhost:4444"), chromeOptions);

            Selenium sel = new Selenium();
            sel.GetUrls(driver);

            UI ui = new UI(sel.UrlTuples);
            ui.GetSelectedUrl();

            string selectedUrl = sel.UrlTuples[ui.InputPostNumber].url;

            string selectedThread = sel.UrlTuples[ui.InputPostNumber].title;

            sel.GetPostsRemaining(driver, selectedUrl);

            sel.GetStocksList(driver, selectedUrl);

            driver.Close();

            Stock stock = new Stock();

            stock.CountEachStock(sel.StocksList);

            stock.DisplayStocks(sel.StocksList);

            Console.WriteLine($" { stock.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");
            

            Console.WriteLine("Do you want to save to CSV? y/N");
            string csvChoice = Console.ReadLine();
            if(csvChoice == "Y" || csvChoice == "y")
            {
                Writer writer = new Writer();
                writer.WriteToFile(sel, stock, selectedThread);

                Console.WriteLine("Finished, saved to csv");
            } else
            {
                Console.WriteLine("Finished, results not saved");
            }

        }

    }
}
