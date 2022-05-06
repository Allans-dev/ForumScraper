﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace ForumScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            //UI ui = new UI();

            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            chromeOptions.AddArgument("headless");            
            IWebDriver driver = new ChromeDriver(chromeOptions);

            Selenium sel = new Selenium();

            sel.GetUrls(driver);

            sel.GetPostsRemaining(driver);

            sel.GetStocksList(driver);

            driver.Close();

            Stock stock = new Stock();

            stock.CountEachStock(sel.StocksList);

            stock.DisplayStocks(sel.StocksList);

            Console.WriteLine($" { stock.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");

            Console.ReadKey();

            Writer writer = new Writer();
            writer.WriteToFile(sel, stock);
        }

    }
}
