using System;

namespace ForumScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            UI ui = new UI();

            Selenium sel = new Selenium();

            sel.GetPostsRemaining(ui.Path);

            sel.GetStocksList(ui.Path, ui.InputPostsNumber);

            Stock stock = new Stock();

            stock.CountEachStock(sel.StocksList);

            stock.DisplayStocks(sel.StocksList);

            Console.WriteLine($" { stock.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");

            Console.ReadKey();

            Writer writer = new Writer();
            writer.WriteToFile(ui, sel, stock);
        }

    }
}
