namespace Website.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Website.DBService;
    using Website.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Website.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Website.Models.MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            Category c1 = new Category() { Code = "1000", Name = "Books" };
            Category c2 = new Category() { Code = "2619526011", Name = "Appliances" };
            Category c3 = new Category() { Code = "2617942011", Name = "Arts, Crafts & Sewing" };
            Category c4 = new Category() { Code = "165797011", Name = "Baby" };
            Category c5 = new Category() { Code = "15690151", Name = "Automotive" };
            Category c6 = new Category() { Code = "11055981", Name = "Beauty" };

            context.Category.AddOrUpdate(c1);
            context.Category.AddOrUpdate(c2);
            context.Category.AddOrUpdate(c3);
            context.Category.AddOrUpdate(c4);
            context.Category.AddOrUpdate(c5);
            context.Category.AddOrUpdate(c6);
            //context.SaveChanges();
            SeedMyDb seed = new SeedMyDb();
            seed.FillDB(context);


        }
    }
}

