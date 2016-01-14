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
    public class BidsController : Controller
    {
        private BidCraftDbContext db = new BidCraftDbContext();


        // GET: Bids
        [HttpGet]
        
        public ActionResult Index(int postId)
        {
            ViewBag.PostId = postId;
            var currentUserId = User.Identity.GetUserId();
            var model = db.Posts.Find(postId).Bids.Select(x => new BidIndexVM()
            {
                PostId = postId,
                BidId = x.Id,
                Amount = x.Amount,
                ProjectFinishByFinishDate = x.ProjectFinishByDate
            }).ToList();
            return View(model);
        }


        //// GET: Bids/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bid bid = db.Bids.Find(id);
        //    if (bid == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bid);
        //}


        // GET: Bids/Create
        public ActionResult Create(int postid)
        {
            ViewBag.PostId = postid;
            var model = new BidIndexVM();
            model.PostId = postid;
            model.ProjectFinishByFinishDate = DateTime.Now; 


            return View(model);
        }

        // POST: Bids/Create
        [HttpPost]
        public ActionResult Create(BidIndexVM bid)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(currentUserId);
            var post = db.Posts.Find(bid.PostId);
            var newBid = new Bid()
            {
                Post = post,
                Bidder = currentUser,
                Amount = bid.Amount,
                ProjectFinishByDate = bid.ProjectFinishByFinishDate,
            };
            currentUser.MyBids.Add(newBid);
            post.Bids.Add(newBid);

          
            db.SaveChanges();
            return RedirectToAction("AllPosts", "Posts");

        }

        // GET: Bids/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            if (bid.Bidder.Id != currentUserId)
            {
                return new HttpUnauthorizedResult("This is not yours.");
            }


            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { postId = bid.Id } );
            }
            return View(bid);
        }

        // GET: Bids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bid bid = db.Bids.Find(id);
            db.Bids.Remove(bid);
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
