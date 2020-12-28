using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Controllers
{
    public class ReportController : Controller
    {
        public async Task<ActionResult> Index()
        {

            return View();
        }

    }
}
