using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

    }


    public class Post : Entity
    {
        public virtual SiteUser ProjectOwner { get; set; }
        public DateTime StartDate { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool AreMaterialsIncluded { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
  

    }

    public class Bid : Entity
    {
        public virtual SiteUser Bidder { get; set; }
        public virtual Post Post { get; set; }
        public int Amount { get; set; }
        public DateTime ProjectFinishByDate { get; set; }
        
        public virtual bool IsWinningBid { get; set; }

    }
}
