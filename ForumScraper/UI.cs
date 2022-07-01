using System;
using System.Collections.Generic;

namespace ForumScraper
{
    internal class UI
    {
        private string ThreadUrl { get; set; }

        private List<(string title, string url)> _urlTuples { get; set; }

        public UI(List<(string title, string url)> urlTuples)
        {
            _urlTuples = urlTuples;
            
        }

        public int InputPostNumber { get; set; }

        internal void GetSelectedUrl()
        {

            int i = 0;
            while (i < _urlTuples.Count)
            {
                Console.WriteLine($"{i+1}. {_urlTuples[i].title}");
                i++;
            }
            Console.WriteLine("");
            Console.WriteLine("Please input the number corresponding to the forum thread you would like to analyse:");
            InputPostNumber = int.Parse(Console.ReadLine())-1 >= 0 && int.Parse(Console.ReadLine())-1 <= _urlTuples.Count ? 
                int.Parse(Console.ReadLine())-1 : 0;
            //Console.WriteLine("Enter Forum URL: ");
            //Path = Console.ReadLine();

            //Console.WriteLine("Enter number of Posts: ");
            //Console.WriteLine("Enter {0} for all");
            //InputPostsNumber = int.Parse(Console.ReadLine()) | 0;

            //Console.WriteLine("Please Enter File Name: ");
            //FileName = Console.ReadLine(); ;
            Console.ReadKey();
        }
    }
}
