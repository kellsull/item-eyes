using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ItemEyes.Data;
using ItemEyes.Models;

namespace ItemEyes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ItemContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ItemContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations
                .Include(l => l.Items)
                .ToListAsync();
            locations.ForEach(l => l.Items = l.Items.OrderByDescending(i => i.ReceivedOn).ToList());
            List<List<int>> monthlyTotals = new List<List<int>>();
            foreach(Location location in locations)
            {
                IEnumerable<Item> items = location.Items.Where(i => (DateTime.Now >= i.ReceivedOn) && (i.ReceivedOn >= DateTime.Now.AddMonths(-5)));
                var localTotals = new List<int>();
                for (var i = -5; i <= 0; i++)
                {
                    var sum = 0;
                    foreach (var item in items)
                    {
                        if (item.ReceivedOn.Month == DateTime.Now.AddMonths(i).Month)
                            sum += 1;
                    }
                    localTotals.Add(sum);
                }
                monthlyTotals.Add(localTotals);
            }
            ViewData["monthlyTotals"] = monthlyTotals;
            return View(locations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
