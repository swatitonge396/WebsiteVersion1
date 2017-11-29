using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    // [Authorize]
    public class ProductController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Products
        //[ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            string CurrentUser=User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(
                 new UserStore<ApplicationUser>(
                     new MyDbContext()));
            int Category=manager.FindById(CurrentUser).Category;
            //ViewBag.User = manager.FindById(CurrentUser).Name;
            var Rec = (from rec in db.ScrapList where rec.User.Id==CurrentUser select rec.Product.Id).ToList();
            var Rec1=(from rec in db.WishList where rec.User.Id == CurrentUser select rec.Product.Id).ToList();
                    

            var list = (from product in db.Products
                        where (product.Category == Category)&&(!Rec.Contains(product.Id))&& (!Rec1.Contains(product.Id))
                        select product).FirstOrDefault();
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

         

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
