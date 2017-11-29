using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class ScrapList
    {
        public int Id { set; get; }
        //[ForeignKey("UerId")]
        public virtual ApplicationUser User { get; set; }
        //[ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public ScrapList()
        {   User = new ApplicationUser();
            Product = new Product();
        }
    }
}