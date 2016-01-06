using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Project { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public string Buyer { get; set; }
        public string Creator { get; set; }
    }
}