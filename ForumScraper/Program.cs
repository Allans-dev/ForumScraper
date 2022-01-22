using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace ForumScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter Forum URL: ");
            string path = Console.ReadLine();

            Console.WriteLine("Enter number of Posts: ");
            Console.WriteLine("Enter {0} for all");
            int numberOfPosts = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter File Name to be written: ");
            string fileName = Console.ReadLine();


            Selenium s = new Selenium();
            int posts = s.getPostsRemaining(path);
            List<Stocks> stocksList = s.getStocksList(path, posts, numberOfPosts);

            Stocks stock = new Stocks();

            List<Stocks> countedStocks = stock.countEachStock(stocksList);

            stock.DisplayStocks(countedStocks);

            string displayPosts = numberOfPosts == 0 ? posts.ToString() : numberOfPosts.ToString();

            Console.WriteLine($" { stock.TotalReturned } out of { displayPosts } returned Stock Code");

            Console.ReadKey();
           
            using (var writer = new StreamWriter($"/Users/allancheung/OneDrive/ForumBreakdowns/{fileName}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(stocksList);
                csv.WriteComment($" { stock.TotalReturned } out of { displayPosts } returned Stock Code");
            }
        }

    }
}
