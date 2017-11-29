using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        
        public ActionResult Index()
        {
            return View();
        }
    }
}