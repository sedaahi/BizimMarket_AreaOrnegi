using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket_AreaOrnegi.Areas.Admin.Controllers
{
    [Area("Admin")]  //Burası admin controllerının areası dedik
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
