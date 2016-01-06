using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{
    public class PostIndexVM
    {
        public int Id { get; set; }
        public string Buyer { get; set; }
        public string Creator { get; set; }
        public DateTime PostedOn { get; set; }
        public int MyProperty { get; set; }
        public int PostId { get; set; }
        public bool IsTrackingProject { get; set; }
    }
}