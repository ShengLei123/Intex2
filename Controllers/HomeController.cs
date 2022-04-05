using Intex2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var blah = _repo.Utah_Crash.ToList();
            ViewData["Total_Num_Records"] = blah.Count();

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
