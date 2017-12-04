using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Website.Models
{
    public class MyDbContext: IdentityDbContext<ApplicationUser>

    {
        public MyDbContext() :base("MyConnection1", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<MyDbContext>(new DropCreateDatabaseIfModelChanges<MyDbContext>());

        }
         

        public DbSet<Product> Products { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<ScrapList> ScrapList { get; set; }
        public DbSet<Category> Category { get; set; }
        public static MyDbContext Create()
        {
            return new MyDbContext();
        }
        
         //<span>@Html.RadioButtonFor(m => m.Category, 1000) @Html.Label("Books")</span> <br/>
         //   <span>@Html.RadioButtonFor(m => m.Category, 2619526011)@Html.Label("Appliances")</span><br />
         //   <span>@Html.RadioButtonFor(m => m.Category, 2617942011)@Html.Label("Arts, Crafts & Sewing")</span><br />
         //   <span>@Html.RadioButtonFor(m => m.Category, 165797011)@Html.Label("Baby")</span><br />
         //   <span>@Html.RadioButtonFor(m => m.Category, 15690151)@Html.Label("Automotive")</span><br />
         //   <span>@Html.RadioButtonFor(m => m.Category, 11055981)@Html.Label("Beauty")</span>

    }
}