using CsvHelper;
using System.Globalization;
using System.IO;


namespace ForumScraper
{
    internal class Writer
    {
        public void WriteToFile(Selenium sel, Stock stocks)
        {
            string filename = sel.UrlTuples[0].title;

            using (var writer = new StreamWriter($"C:\\Users\\allan\\OneDrive\\ForumBreakdowns\\{filename}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(sel.StocksList);
                csv.WriteComment($" { stocks.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");
            }
        }
    }
}
