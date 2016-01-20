using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BidCraft.web.Models
{

    public class BidEditVM
    {
        public int PostId
        {
            get; set;
        }
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

    public class PostEditVM
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }

        [Required]
        [Display(Name = "Proposed Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "URL to Example of Project (i.e. Website)")]
        public string Url { get; set; }
        [Required]
        [Display(Name = "URL to Image of Project")]
        public string ImageUrl { get; set; }
        [Required]
        [Display(Name = "Short Title For Project")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Project Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Are you supplying the materials?")]
        public bool AreMaterialsIncluded { get; set; }



    }
}