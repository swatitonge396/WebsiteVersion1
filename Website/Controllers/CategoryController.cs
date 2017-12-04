using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Category

        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(
                 new UserStore<ApplicationUser>(
                     new MyDbContext()));
            int UserCategory = manager.FindById(User.Identity.GetUserId()).Category;
            var list = (from cat in db.Category select cat).ToList();
            foreach (Category cat in list)
            {
                if (cat.Id == UserCategory)
                {
                    cat.Selected = true;
                }
            }
            return View(list);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Index(int Category)
        {   //changes the category of user
            MyDbContext db = new MyDbContext();
            string CurrentUser = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MyDbContext()));
            ApplicationUser user = manager.FindById(CurrentUser);
            user.Category = Category;
            var result = await manager.UpdateAsync(user);
            return RedirectToAction("Index", "Product");
        }


    }
}