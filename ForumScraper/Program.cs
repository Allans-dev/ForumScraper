using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ForumScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            //UI ui = new UI();

            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            IWebDriver driver = new ChromeDriver(chromeOptions);

            Selenium sel = new Selenium();

            sel.GetUrls(driver);

            //sel.GetPostsRemaining(ui.Path, driver);

            //sel.GetStocksList(ui.Path, ui.InputPostsNumber, driver);

            driver.Close();

            //Stock stock = new Stock();

            //stock.CountEachStock(sel.StocksList);

            //stock.DisplayStocks(sel.StocksList);

            //Console.WriteLine($" { stock.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");

            Console.ReadKey();

            //Writer writer = new Writer();
            //writer.WriteToFile(ui, sel, stock);
        }

    }
}
