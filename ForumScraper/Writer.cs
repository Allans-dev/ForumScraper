using CsvHelper;
using System.Globalization;
using System.IO;


namespace ForumScraper
{
    internal class Writer
    {
        public void WriteToFile(UI ui, Selenium sel, Stock stocks)
        {

            using (var writer = new StreamWriter($"C:\\Users\\allan\\OneDrive\\ForumBreakdowns\\{ui.FileName}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(sel.StocksList);
                csv.WriteComment($" { stocks.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");
            }
        }
    }
}
