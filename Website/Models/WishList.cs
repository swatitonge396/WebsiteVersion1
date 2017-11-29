using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class WishList
    {
        public int Id { set; get; }
        //[ForeignKey("UerId")]
        public string ASIN { set; get; }
        public virtual ApplicationUser User { get; set; }
        //[ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public WishList()
        {
            User = new ApplicationUser();
            Product = new Product();
        }
    }
}