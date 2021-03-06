﻿using BidCraft.web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BidCraft.web.Controllers
{
    public class HomeController : Controller
    {
        private BidCraftDbContext db = new BidCraftDbContext();


        public ActionResult Index(bool? userOnly)
        {

            //var currentUserId = User.Identity.GetUserId();

            //var query = db.Posts.AsQueryable();

            //if (userOnly.GetValueOrDefault())
            //{
            //    query = query.Where(x => x.ProjectOwner.Id == currentUserId);
            //}


            //var model = query.Select(x => new PostIndexVM()
            //{
            //    Id = x.Id,
            //    PostedOn = x.CreatedOn,
            //    Url = x.Url,
            //    ImageUrl = x.ImageUrl,
            //    Title = x.Title,
            //    NumberOfBids = x.Bids.Count(),
            //    StartDate = x.StartDate,
            //    AreMaterialsIncluded = x.AreMaterialsIncluded,
            //    IsMyPost = x.ProjectOwner.Id == currentUserId
            //}).ToList();

            return View();
        }

        public ActionResult GetBidsByPost(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var post = db.Posts.Find(id);
            var model = post.Bids.Select(b => new
            {
                PostTitle = b.Post.Title,
                Amount = b.Amount,
                FinishDate = b.ProjectFinishByDate,
                BidderName = b.Bidder.FirstName + " " + b.Bidder.LastName,
                BidId = b.Id,
                BidderId = b.Bidder.Id,
                IsMyBid = b.Bidder.Id == currentUserId
            }).ToList();

            return Json(new { Bids = model, IsMyPost = post.ProjectOwner.Id == currentUserId  } , JsonRequestBehavior.AllowGet);
        }
        //todo ask daniel about this for Show Bids to be wired up
        //public ActionResult ShowBids(int id)
        //{
        //    var model = db.Posts.Find(id).Bids.Select(b => new
        //    {
        //        Amount = b.Amount,
        //        FinishDate = b.ProjectFinishByDate,
        //        BidderName = b.Bidder.FirstName + " " + b.Bidder.LastName,
        //        BidId = b.Id
        //    }).ToList();

        //    return Json(model, JsonRequestBehavior.AllowGet);

        //}

    public ActionResult GetAllPosts(bool? userOnly)
    {

        var currentUserId = User.Identity.GetUserId();

        var query = db.Posts.AsQueryable();

        if (userOnly.GetValueOrDefault())
        {
            query = query.Where(x => x.ProjectOwner.Id == currentUserId);
        }


        var model = query.Select(x => new PostIndexVM()
        {
            Id = x.Id,
            PostedOn = x.CreatedOn ?? DateTime.MinValue,
            Url = x.Url,
            ImageUrl = x.ImageUrl,
            Title = x.Title,
            Description = x.Description,
            NumberOfBids = x.Bids.Count(),
            StartDate = x.StartDate,
            AreMaterialsIncluded = x.AreMaterialsIncluded,
            IsMyPost = x.ProjectOwner.Id == currentUserId,
            Buyer = x.ProjectOwner.FirstName + " " + x.ProjectOwner.LastName
        }).ToList();

        return Json(model, JsonRequestBehavior.AllowGet);
    }

}
}