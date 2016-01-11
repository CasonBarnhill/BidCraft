using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{
    public class PostIndexVM
    {
        public int Id { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime StartDate { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public int NumberOfBids { get; set; }
        public bool AreMaterialsIncluded { get; set; }
        public bool IsMine { get; set; }



    }

    public class PostDetailsVM
    {
        public int Id { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime StartDate { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public bool AreMaterialsIncluded { get; set; }
        public string Description { get; set; } 
        public bool IsMine { get; set; }

        public ICollection<BidDetailsVM> Bids{ get; set; } = new List<BidDetailsVM>();

    }

    public class BidDetailsVM
    {
        public int Id { get; set; }
        public DateTime ProjectFinishByDate { get; set; }
        public decimal Amount { get; set; }



    }
}