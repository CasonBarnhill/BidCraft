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

        // GET: Bids/Create
        public ActionResult CreateBid(int postid)
        {
            var model = new BidIndexVM();
            model.PostId = postid;
            model.ProjectFinishByDate = DateTime.Now;


            return View(model);
        }

        // POST: Bids/Create
        [HttpPost]
        public ActionResult CreateBid(BidEditVM bid)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(currentUserId);
            var post = db.Posts.Find(bid.PostId);
            var newBid = new Bid()
            {
                Post = post,
                Bidder = currentUser,
                Amount = bid.Amount,
                ProjectFinishByDate = bid.FinishDate,
            };

            currentUser.MyBids.Add(newBid);
            post.Bids.Add(newBid);


            db.SaveChanges();
            return RedirectToAction("Details", "Posts", new { id = newBid.Post.Id });

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

            var model = new BidEditVM { BidId = bid.Id, Amount = bid.Amount, FinishDate = bid.ProjectFinishByDate };

            return View(model);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BidEditVM bid)
        {
            if (ModelState.IsValid)
            {
                var existingBid = db.Bids.Find(bid.BidId);
                existingBid.Amount = bid.Amount;
                existingBid.ProjectFinishByDate = bid.FinishDate;
                db.SaveChanges();

                return RedirectToAction("Details", "Posts", new { id = existingBid.Post.Id });
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
            var postId = bid.Post.Id;
            db.Bids.Remove(bid);
            db.SaveChanges();
            return RedirectToAction("Details", "Posts", new { id = postId });
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
