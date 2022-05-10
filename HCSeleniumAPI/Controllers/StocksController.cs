using HCSeleniumAPI.Models;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace HCSeleniumAPI.Controllers
{
    public class StocksController : Controller
    {
        public Selenium sel { get; set; } = new Selenium();

        public StocksController()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            chromeOptions.AddArgument("headless");
            //chromeOptions.BinaryLocation = "/user/bin/google-chrome";
            //IWebDriver driver = new ChromeDriver(chromeOptions);
            IWebDriver driver = new RemoteWebDriver(new Uri("http://localhost:4444"), chromeOptions);

            sel.GetUrls(driver);

            sel.GetPostsRemaining(driver);

            sel.GetStocksList(driver);

            driver.Close();

            Stock stock = new Stock();

            stock.CountEachStock(sel.StocksList);

            stock.DisplayStocks(sel.StocksList);

            Console.WriteLine($" { stock.TotalReturned } out of { sel.NumberOfPosts - sel.PostsRemaining } returned Stock Code");
            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        // GET: StocksController
        [HttpGet("list")]
        public IActionResult Get()
        {
            List<string> titleList = new List<string>();
            List<string> urlList = new List<string>();

            foreach ((string url, string title) in sel.UrlTuples) 
            {
                titleList.Add(title);
                urlList.Add(url);
            }

            return sel.StocksList.Count() > 0 ? Ok(sel.StocksList.ToList()) : Ok(titleList);
        }

        // GET: StocksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StocksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StocksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StocksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StocksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StocksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StocksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
