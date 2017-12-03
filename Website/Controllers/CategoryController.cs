using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        [HttpPost]
        public ActionResult Index(int Category)
        {   //changes the category of user
            MyDbContext db = new MyDbContext();
            string CurrentUser = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MyDbContext()));
            ApplicationUser user = manager.FindById(CurrentUser);
            user.Category = Category;
            manager.UpdateAsync(user);
            //ViewBag.msg = "New Category Selected " + db.Category.Find(Category)?.Name;
            return RedirectToAction("Index", "Product");
        }
    }
}