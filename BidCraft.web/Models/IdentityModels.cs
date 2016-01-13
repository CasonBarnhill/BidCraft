using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Web;
using System;
using System.Linq;

namespace BidCraft.web.Models
{
    public enum UserType
    {
        Buyer,
        Creator
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class SiteUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }

        public string ProfilePic { get; set; }
        public int CreatorRating { get; set; }

        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<Post> MyPosts { get; set; }
        public virtual ICollection<Bid> MyBids { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<SiteUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


    }

    public class BidCraftDbContext : IdentityDbContext<SiteUser>
    {
        public BidCraftDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static BidCraftDbContext Create()
        {
            return new BidCraftDbContext();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Bid> Bids { get; set; }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = HttpContext.Current?.User?.Identity.Name ?? "Anonymous";

            foreach (var entity in entities)
            {
                var e = (Entity)entity.Entity;
                if (entity.State == EntityState.Added)
                {
                    e.CreatedOn = DateTime.Now;
                    e.CreatedBy = currentUsername;
                }

                e.ModifiedOn = DateTime.Now;
                e.ModifiedBy = currentUsername;
            }

            return base.SaveChanges();
        }

    }
}