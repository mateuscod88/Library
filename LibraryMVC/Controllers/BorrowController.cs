﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class BorrowController : Controller
    {
        // GET: Borrow
        public ActionResult Index()
        {
            return View();
        }
    }
}