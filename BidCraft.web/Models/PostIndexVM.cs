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
       

    }
}