namespace BidCraft.web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BidCraft.web.Models.BidCraftDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BidCraft.web.Models.BidCraftDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var userStore = new UserStore<SiteUser>(context);
            var userManager = new UserManager<SiteUser>(userStore);

            if (!(context.Users.Any(u => u.UserName == "casonbarnhill@yahoo.com")))
            {
                var userToInsert = new SiteUser
                {
                    UserName = "casonbarnhill@yahoo.com",
                    PhoneNumber = "5015555555",
                    City = "Little Rock",
                    State = "AR",
                    Zip = "72203",
                    FirstName = "Cason",
                    LastName = "Barnhill",
                    Email = "casonbarnhill@yahoo.com",
                    Street = "123 Main St",
                    UserType = UserType.Buyer
                };
                userManager.Create(userToInsert, "Password@1234");
            }

            if (!(context.Users.Any(u => u.UserName == "jimmy@yahoo.com")))
            {
                var userToInsert = new SiteUser
                {
                    UserName = "jimmy@yahoo.com",
                    PhoneNumber = "5015557777",
                    City = "Little Rock",
                    State = "AR",
                    Zip = "72203",
                    FirstName = "Jimmy",
                    LastName = "Nuetron",
                    Email = "jimmy@yahoo.com",
                    Street = "234 S. Main St",
                    UserType = UserType.Creator
                };
                userManager.Create(userToInsert, "Password@1234");
            }



        }
    }
}
