﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BidCraft.web.Models;
using Microsoft.AspNet.Identity;

namespace BidCraft.web.Controllers
{
    public class PostsController : Controller
    {
        private BidCraftDbContext db = new BidCraftDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var model = db.Posts.Select(x => new PostIndexVM()
            {
                Id = x.Id,
                PostedOn = x.CreatedOn,
                Url = x.Url,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                NumberOfBids = x.Bids.Count(),
                StartDate = x.StartDate

            }).ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Create(string description)
        {
            var currentUserId = User.Identity.GetUserId();

            var post = new Post()
            {
                PostedOn = DateTime.Now,
                ProjectOwner = db.Users.Find(currentUserId),
                
                //TODO make these fields be there
                
                //Url = Url,
                //ImageUrl = ImageUrl,
                //Bid = Bid,
                //StartDate = StartDate,
                //AreMaterialsIncluded = AreMaterialsIncluded,
                Description = description
            };

            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }




        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Project,CreatedOn,ProjectStartDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Project,CreatedOn,ProjectStartDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
