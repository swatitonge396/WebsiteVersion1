﻿using Microsoft.AspNet.Identity;
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
    public class WishListsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: WishLists
        public ActionResult Index()
        {
            var Id = User.Identity.GetUserId();
            var list = (from rec in db.WishList where rec.User.Id == Id select rec.Product.Id).ToList();

            if (list == null)
                return View(list);
            var listWish = (from rec in db.Products where list.Contains(rec.Id) select rec).ToList();

            return View(listWish);
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(int Id)
        {
            if (ModelState.IsValid)
            {
                //WishList w = db.WishList.FirstOrDefault(rec => rec.Product.Id == Id);
                //if (w == null)
                //{
                    Product p = db.Products.Find(Id);
                    //if (p == null)
                    //{
                    //    Console.WriteLine("no product in Db");
                    //    //need to call api
                    //}

                    ApplicationUser u = db.Users.Find(User.Identity.GetUserId());
                   // Console.WriteLine("user id" + u.Email);

                    WishList wish = new WishList()
                    {
                        ASIN = p.ASIN,

                        Product = p,

                        User = u
                    };

                    db.WishList.Add(wish);
                    db.SaveChanges();

                //}

            }

            return RedirectToAction("Index", "Product");
        }


        // POST: WishLists/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            WishList wishList = db.WishList.FirstOrDefault(rec => rec.Product.Id == id);
            wishList.Product = null;
            wishList.User = null;
            db.WishList.Remove(wishList);
            db.SaveChanges();
            return RedirectToAction("Index");
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
