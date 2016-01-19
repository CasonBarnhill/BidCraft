using System;
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
    [Authorize]
    public class PostsController : Controller
    {
        private BidCraftDbContext db = new BidCraftDbContext();

        // GET: Posts
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
                PostedOn = x.CreatedOn ?? new DateTime(),
                Url = x.Url,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                NumberOfBids = x.Bids.Count(),
                StartDate = x.StartDate,
                AreMaterialsIncluded = x.AreMaterialsIncluded,
                IsMyPost = x.ProjectOwner.Id == currentUserId,
                //Bids = x.Bids.Select(b => new BidIndexVM()).ToList() //TODO fill out??
            }).ToList();


            return View(model);
        }


        [HttpGet]
        //Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(currentUserId);

            var newPost = new Post()
            {
                ProjectOwner = currentUser,
                Url = post.Url,
                ImageUrl = post.ImageUrl,
                StartDate = post.StartDate,
                AreMaterialsIncluded = post.AreMaterialsIncluded,
                Description = post.Description
            };

            currentUser.MyPosts.Add(newPost);

            //db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }


        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            var currentUserId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }


            var model = new PostIndexVM();
            model.Id = post.Id;
            model.IsMyPost = post.ProjectOwner.Id == currentUserId;
            model.Title = post.Title;
            model.Url = post.Url;
            model.ImageUrl = post.ImageUrl;
            model.Description = post.Description;
            model.AreMaterialsIncluded = post.AreMaterialsIncluded;
                


            var bidsQuery = post.Bids.AsQueryable();

            //this isn't my post. then filter the bids shown to be just mine.
            //if (!model.IsMyPost)
            //{
            //    bidsQuery = bidsQuery.Where(x => x.Bidder.Id == currentUserId);
            //}



            model.Bids = bidsQuery.Select(b => new BidIndexVM {
                PostId = b.Post.Id,
                BidId = b.Id,
                Amount = b.Amount,
                ProjectFinishByDate = b.ProjectFinishByDate
            }).ToList();

            return View(model);
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
        public ActionResult Edit(Post post)
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
