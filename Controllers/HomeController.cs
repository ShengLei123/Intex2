using Intex2.Models;
using Microsoft.AspNetCore.Authorization;
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

            // Get Year
            var unique_year = new List<int>();
            var raw_date_list = _repo.Utah_Crash
                .Select(x => x.CRASH_DATETIME)
                .OrderBy(x => x)
                .ToList();
            var min_date_list = new List<DateTime>();
            var min_date_list_percent = new List<string>();
            var min_date_list_int = new List<double>();
            foreach (DateTime d in raw_date_list)
            {
                unique_year.Add(d.Year);
            }
            unique_year = unique_year.Distinct().ToList();
            foreach (int y in unique_year)
            {
                var temp_min_year = raw_date_list.FindAll(x => x.Value.Year == y).ToList();
                double temp_year_count = temp_min_year.Count();
                double temp_year_count_percent = temp_year_count / total_count;
                min_date_list_percent.Add(temp_year_count_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty));
                min_date_list_int.Add(Math.Round(temp_year_count_percent * 100, 0, MidpointRounding.AwayFromZero));
                min_date_list.Add((DateTime)temp_min_year.Min());
            }
            var year_view = unique_year.Zip(min_date_list, (n, w) => new { Number = n, Word = w });

            //Get County
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var salt_lake_county = crash_list.FindAll(x => x.COUNTY_NAME == "SALT LAKE");
            double salt_lake_count = salt_lake_county.Count();
            double salt_lake_percent = salt_lake_count / total_count;
            var raw_unique_county = crash_list.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            var unique_county = new List<string>();
            var unique_county_percent = new List<string>();
            foreach (string c in raw_unique_county)
            {
                unique_county.Add(textInfo.ToTitleCase(c.ToLower()));
                double temp_city_count = crash_list.FindAll(x => x.COUNTY_NAME == c).Count();
                double temp_city_percent = temp_city_count / total_count;
                unique_county_percent.Add(temp_city_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty));
            }

            // Get Severity
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

            //Get Tags
            var intersection_related_list = crash_list.FindAll(x => x.INTERSECTION_RELATED == 1).ToList();
            double intersection_related_count = intersection_related_list.Count();
            var night_dark_condition_list = crash_list.FindAll(x => x.NIGHT_DARK_CONDITION == 1).ToList();
            double night_dark_condition_count = night_dark_condition_list.Count();
            var roadway_departure_list = crash_list.FindAll(x => x.ROADWAY_DEPARTURE == 1).ToList();
            double roadway_departure_count = roadway_departure_list.Count();
            var distracted_driving_list = crash_list.FindAll(x => x.DISTRACTED_DRIVING == 1).ToList();
            double distracted_driving_count = distracted_driving_list.Count();
            var wild_animal_list = crash_list.FindAll(x => x.WILD_ANIMAL_RELATED == 1).ToList();
            double wild_animal_count = wild_animal_list.Count();
            var drowsy_driving_list = crash_list.FindAll(x => x.DROWSY_DRIVING == 1).ToList();
            double drowsy_driving_count = drowsy_driving_list.Count();
            double intersection_related_percent = intersection_related_count / total_count;
            double night_dark_condition_percent = night_dark_condition_count / total_count;
            double roadway_departure_percent = roadway_departure_count / total_count;
            double distracted_driving_percent = distracted_driving_count / total_count;
            double wild_animal_percent = wild_animal_count / total_count;
            double drowsy_driving_percent = drowsy_driving_count / total_count;

            ViewData["Total_Num_Records"] = crash_list.Count();
            ViewData["Salt_Lake_Percent"] = salt_lake_percent.ToString("P0", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Salt_Lake_Int"] = Math.Round(salt_lake_percent * 100, 0, MidpointRounding.AwayFromZero);
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
            ViewBag.Unique_Year = unique_year;
            ViewBag.Min_Date_List_Percent = min_date_list_percent;
            ViewBag.Min_Date_List_Int = min_date_list_int;
            ViewBag.Min_Date_List = min_date_list;
            ViewData["Intersection_Related"] = intersection_related_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Night_Dark_Condition"] = night_dark_condition_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Road_Departure"] = roadway_departure_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Distracted_Driving"] = distracted_driving_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Wild_Animal"] = wild_animal_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewData["Drowsy_Driving"] = drowsy_driving_percent.ToString("P1", CultureInfo.InvariantCulture).Replace(" ", string.Empty);
            ViewBag.Raw_Unique_County = raw_unique_county;
            ViewBag.Unique_County = unique_county;
            ViewBag.Unque_County_Percent = unique_county_percent;

            return View();
        }

        public IActionResult Output(Utah_Crash c)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Accidents()
        {
            var blah = _repo.Utah_Crash.OrderByDescending(x => x.CRASH_ID).Take(10).ToList();
            return View(blah);
        }

        [HttpGet]
        public IActionResult AddAccident()
        {
            var blah = _repo.Utah_Crash.Select(x => x.CRASH_ID);

            var hah = new Utah_Crash
            {
                CRASH_ID = blah.Max() + 1
            };

            return View("AccidentForm", hah);
        }

        [HttpPost]
        public IActionResult AddAccident(Utah_Crash c)
        {
            var blah = _repo.Utah_Crash.Select(x => x.CRASH_ID);

            if (blah.Contains(c.CRASH_ID))
            {
                _repo.SaveAccident(c);
            }
            else
            {
                _repo.CreateAccident(c);
            }

            return RedirectToAction("Accidents");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blah = _repo.Utah_Crash.Where(x => x.CRASH_ID == id).FirstOrDefault();
            return View("AccidentForm", blah);
        }

        public IActionResult Delete(int id)
        {
            var blah = _repo.Utah_Crash.Where(x => x.CRASH_ID == id).FirstOrDefault();
            _repo.DeleteAccident(blah);
            return RedirectToAction("Accidents");
        }
    }
}
