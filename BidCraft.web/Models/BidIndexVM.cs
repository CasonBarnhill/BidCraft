using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class BidEditVM
    {
        [Required]
        public int BidId
        {
            get; set;
        }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Bid Amount Must Be Between $1 and $1,000,000")]
        [Display(Name = "Proposed Bid Amount")]
        public decimal Amount
        {
            get; set;
        }

        [Required]
        [FutureDate(ErrorMessage = "Finish Date must be set within the 2 yeasrs")]
        [Display(Name = "Proposed Finish Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FinishDate
        {
            get; set;
        }
    }

    public class FutureDateAttribute : RangeAttribute
    {
        public FutureDateAttribute()
          : base(typeof(DateTime),
                  DateTime.Now.ToShortDateString(),
                  DateTime.Now.AddYears(2).ToShortDateString())
        { }
    }
}