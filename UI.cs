using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumScraper
{
    internal class UI
    {
        public string Path { get; set; }

        public int InputPostsNumber { get; set; } = 0;

        public string FileName { get; set; }

        public UI()
        {
            

            Console.WriteLine("Enter Forum URL: ");
            Path = Console.ReadLine();

            Console.WriteLine("Enter number of Posts: ");
            Console.WriteLine("Enter {0} for all");
            InputPostsNumber = int.Parse(Console.ReadLine()) | 0;

            Console.WriteLine("Please Enter File Name: ");
            FileName = Console.ReadLine(); ;
        }

        
    }
}
