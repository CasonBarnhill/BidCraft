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

            var currentUserId = User.Identity.GetUserId();

            var query = db.Posts.AsQueryable();

            if (userOnly.GetValueOrDefault())
            {
                query = query.Where(x => x.ProjectOwner.Id == currentUserId);
            }


            var model = query.Select(x => new PostIndexVM()
            {
                Id = x.Id,
                PostedOn = x.CreatedOn,
                Url = x.Url,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                NumberOfBids = x.Bids.Count(),
                StartDate = x.StartDate,
                AreMaterialsIncluded = x.AreMaterialsIncluded,
                IsMyPost = x.ProjectOwner.Id == currentUserId
            }).ToList();
            return View("Index", model);
        }


    }
}