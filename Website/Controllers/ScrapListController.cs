using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class ScrapListController : Controller
    {
        private MyDbContext db = new MyDbContext();
                
        //[ValidateAntiForgeryToken]
        public ActionResult Create(int Id)
        {
            if (ModelState.IsValid)
            {
                //WishList w = db.WishList.FirstOrDefault(rec => rec.Product.Id == Id);
                //if (w == null)
                //{
                    Product p = db.Products.Find(Id);
                  //  if (p == null)
                   // {
                    //    Console.WriteLine("no product");
                    //}
                    ApplicationUser u = db.Users.Find(User.Identity.GetUserId());
                    ScrapList rec = new ScrapList()
                    {
                        Product = p,
                        User = u
                    };

                    db.ScrapList.Add(rec);
                    db.SaveChanges();

                }

            //}

            return RedirectToAction("Index", "Product");
        }

    }
}