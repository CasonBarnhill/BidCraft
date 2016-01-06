using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime StartDate { get; set; }
        public ApplicationUser Buyer { get; set; }
        public ApplicationUser Creator { get; set; }
        public DateTime PostedOn { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public int Bid { get; set; }


        //Todo    These need to be on the creator side????
        //public DateTime FinishByDate { get; set; }
        //public bool IsTrackingProject { get; set; }
        //public int Bid { get; set; }
    }
}