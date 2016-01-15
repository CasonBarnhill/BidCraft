using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{
    public class PostIndexVM
    {
        public int Id { get; set; }
        public DateTime? PostedOn { get; set; }
        public DateTime? StartDate { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public int NumberOfBids { get; set; }
        public bool AreMaterialsIncluded { get; set; }
        public bool IsMyPost { get; set; }
        public string Creator { get; set; }
        public string Buyer { get; set; }
        public string Description { get; set; }

        public ICollection<BidIndexVM> Bids { get; set; } = new List<BidIndexVM>();

    }

    public class BidIndexVM
    {
        public int PostId { get; set; }
        public int BidId { get; set; }
        public string Description { get; set; }
        public bool IsMyBid { get; set; }
        public decimal Amount { get; internal set; }
        public DateTime ProjectFinishByDate { get; internal set; }
    }
}