using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{
    public class BidIndexVM
    {
        public decimal Amount { get; set; }
        public DateTime ProjectFinishByFinishDate { get; set; }
        public int PostId { get; set; }
        public int BidId { get; set; }
        public string Description { get; set; }
        public bool IsMine { get; set; }


    }
}