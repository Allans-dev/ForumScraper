using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace ForumScraper
{
    public class Stocks
    {
        private int _timesRepeated = 0;

        public string Name { get; set; }

        public string Text { get; set; }

        public int TimesRepeated { get=>_timesRepeated; set { _timesRepeated = value; } }
        
        public int TotalReturned { get; set; }

        public List<Stocks> countEachStock(List<Stocks> stocksList)
        {
            var q = stocksList.GroupBy(x => x.Name)
                        .Select(x => new {
                            Count = x.Count(),
                            Name = x.Key
                        })
                        .OrderByDescending(x => x.Count);

            foreach (var x in q)
            {
                foreach (Stocks s in stocksList)
                {
                    if (x.Name == s.Name && x.Name != null)
                    {
                        s.TimesRepeated = x.Count;
                    }
                }
            }
            TotalReturned = q.Count();
           return stocksList;
        }

        public void DisplayStocks(List<Stocks> stocksList)
        {
            var unique = stocksList.DistinctBy(x => x.Name).ToList();
            unique.Sort((x, y)=> {
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
