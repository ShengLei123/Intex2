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
            double severity_sum = (double)crash_list.Sum(x => x.CRASH_SEVERITY_ID);
            double severity_ave = severity_sum / total_count;
            
            double serverity_1 = 0;
            double serverity_2 = 0;
            double serverity_3 = 0;
            double serverity_4 = 0;
            double serverity_5 = 0;
            foreach (double s in severity_list)
            {
                if (s == 1)
                {
                    serverity_1 += 1;
                }
                else if (s == 2)
                {
                    serverity_2 += 1;
                }
                else if (s == 3)
                {
                    serverity_3 += 1;
                }
                else if (s == 4)
                {
                    serverity_4 += 1;
                }
                else
                {
                    serverity_5 += 1;
                }
            }
            double serverity_1_percent = serverity_1 / total_count;
            double serverity_2_percent = serverity_2 / total_count;
            double serverity_3_percent = serverity_3 / total_count;
            double serverity_4_percent = serverity_4 / total_count;
            double serverity_5_percent = serverity_5 / total_count;

            ViewData["Total_Num_Records"] = crash_list.Count();
            ViewData["Salt_Lake_Percent"] = salt_lake_percent.ToString("P1", CultureInfo.InvariantCulture);
            ViewData["Average_Severity"] = Math.Round(severity_ave, 1, MidpointRounding.AwayFromZero);
            ViewData["Begin_Year"] = unique_year.Min();
            ViewData["End_Year"] = unique_year.Max();
            ViewData["Severity_1_Percent"] = serverity_1_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Severity_2_Percent"] = serverity_2_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Severity_3_Percent"] = serverity_3_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Severity_4_Percent"] = serverity_4_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Severity_5_Percent"] = serverity_5_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Severity_1_Int"] = Math.Round(serverity_1_percent * 100, 0, MidpointRounding.AwayFromZero);
            ViewData["Severity_2_Int"] = Math.Round(serverity_2_percent * 100, 0, MidpointRounding.AwayFromZero);
            ViewData["Severity_3_Int"] = Math.Round(serverity_3_percent * 100, 0, MidpointRounding.AwayFromZero);
            ViewData["Severity_4_Int"] = Math.Round(serverity_4_percent * 100, 0, MidpointRounding.AwayFromZero);
            ViewData["Severity_5_Int"] = Math.Round(serverity_5_percent * 100, 0, MidpointRounding.AwayFromZero);

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
