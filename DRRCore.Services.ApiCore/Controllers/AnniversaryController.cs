﻿using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    public class AnniversaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
