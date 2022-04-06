using Intex2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Controllers
{
    public class HomeController : Controller
    {
        private ICrashRepository _repo { get; set; }
        public HomeController (ICrashRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Analytics()
        {
            var crash_list = _repo.Utah_Crash.ToList();
            double total_count = crash_list.Count();

            var unique_year = new List<int>();
            var raw_date_list = _repo.Utah_Crash
                .Select(x => x.CRASH_DATETIME)
                .OrderBy(x => x)
                .ToList();
            foreach (DateTime d in raw_date_list)
            {
                unique_year.Add(d.Year);
            }
            unique_year = unique_year.Distinct().ToList();

            var salt_lake_county = crash_list.FindAll(x => x.COUNTY_NAME == "SALT LAKE");
            double salt_lake_count = salt_lake_county.Count();
            double salt_lake_percent = salt_lake_count / total_count;

            var severity_list = _repo.Utah_Crash
                .Select(x => x.CRASH_SEVERITY_ID)
                .OrderBy(x => x)
                .ToList();
            double serverity_sum = (double)crash_list.Sum(x => x.CRASH_SEVERITY_ID);
            double severity_ave = serverity_sum / total_count;

            

            ViewData["Total_Num_Records"] = crash_list.Count();
            ViewData["Salt_Lake_Percent"] = salt_lake_percent.ToString("P1", CultureInfo.InvariantCulture);
            ViewData["Average_Severity"] = Math.Round(severity_ave, 1, MidpointRounding.AwayFromZero);
            ViewData["Begin_Year"] = unique_year.Min();
            ViewData["End_Year"] = unique_year.Max();


            return View();
        }

        public IActionResult Output()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
