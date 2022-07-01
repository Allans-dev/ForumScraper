using CsvHelper;
using System.Globalization;
using System.IO;


namespace ForumScraper
{
    internal class Writer
    {
        public void WriteToFile(Selenium sel, Stock stocks, string selectedThread)
        {

            using (var writer = new StreamWriter($"C:\\Users\\allan\\OneDrive\\ForumBreakdowns\\{selectedThread}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(sel.StocksList);
                csv.WriteComment($" { stocks.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");
            }
        }
    }
}
