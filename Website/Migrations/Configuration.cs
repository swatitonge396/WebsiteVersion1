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

            Category c1 = new Category() { Id = 1, Code = "2619526011", Name = "Appliances" };
            Category c2 = new Category() { Id = 2, Code = "2617942011", Name = "Arts, Crafts & Sewing" };
            Category c3 = new Category() { Id = 3, Code = "15690151", Name = "Automotive" };

            Category c4 = new Category() { Id = 4, Code = "165797011", Name = "Baby" };
            Category c5 = new Category() { Id = 5, Code = "11055981", Name = "Beauty" };
            Category c6 = new Category() { Id = 6, Code = "1000", Name = "Books" };

            Category c7 = new Category() { Id = 7, Code = "7141124011", Name = "Clothing, Shoes & Jewelry" };

            Category c8 = new Category() { Id = 8, Code = "493964", Name = "Electronics" };


            Category c9 = new Category() { Id = 9, Code = "3760931", Name = "Health & Personal Care" };

            Category c10 = new Category() { Id = 10, Code = "165795011", Name = "Toys & Games" };

            context.Category.AddOrUpdate(c1);
            context.Category.AddOrUpdate(c2);
            context.Category.AddOrUpdate(c3);
            context.Category.AddOrUpdate(c4);
            context.Category.AddOrUpdate(c5);
            context.Category.AddOrUpdate(c6);
            context.Category.AddOrUpdate(c7);
            context.Category.AddOrUpdate(c8);
            context.Category.AddOrUpdate(c9);
            context.Category.AddOrUpdate(c10);

            //context.SaveChanges();
            SeedMyDb seed = new SeedMyDb();
            seed.FillDB(context);


        }
    }
}
