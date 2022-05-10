namespace HCSeleniumAPI.Models
{
    public class Stock
    {
        private int _timesRepeated = 0;

        internal string ?Name { get; set; }

        internal string ?Text { get; set; }
        //public int NumberOfUpvotes { get; set; }

        //public int NumberOfLightbulbs { get; set; }

        public int TimesRepeated { get => _timesRepeated; set { _timesRepeated = value; } }

        public int TotalReturned { get; set; }

        public void CountEachStock(List<Stock> stocksList)
        {
            var q = stocksList.GroupBy(x => x.Name)
                        .Select(x => new {
                            Count = x.Count(),
                            Name = x.Key
                        })
                        .OrderByDescending(x => x.Count);

            foreach (var x in q)
            {
                foreach (Stock s in stocksList)
                {
                    if (x.Name == s.Name && x.Name != null)
                    {
                        s.TimesRepeated = x.Count;
                    }
                }
            }
            TotalReturned = q.Count();
        }

        public void DisplayStocks(List<Stock> stocksList)
        {
            var unique = stocksList.DistinctBy(x => x.Name).ToList();
            unique.Sort((x, y) => {
                return y.TimesRepeated.CompareTo(x.TimesRepeated);
            });

            foreach (var u in unique)
            {
                Console.WriteLine($"{u.Name}  | {u.TimesRepeated} ");
                if (u.TimesRepeated >= 3)
                {
                    //Console.WriteLine(u.Text);
                }

            }

        }
    }
}
