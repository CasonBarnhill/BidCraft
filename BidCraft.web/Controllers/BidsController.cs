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
    public class BidsController : Controller
    {
        private BidCraftDbContext db = new BidCraftDbContext();


        // GET: Bids
        [HttpGet]
        public ActionResult Index(int postId)
        {
            var currentUserId = User.Identity.GetUserId();
            var model = db.Posts.Find(postId).Bids.Select(x => new BidIndexVM()
            {
                PostId = x.Id,
                Amount = x.Amount,
                ProjectFinishByFinishDate = x.ProjectFinishByDate
            }).ToList();
            return View(model);
        }

        // GET: Bids/Details/5
        public ActionResult Details(int? id)
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

        // GET: Bids/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bids/Create
        [HttpPost]
        public ActionResult Create(BidIndexVM bid)
        {
            var currentUserId = User.Identity.GetUserId();

            var newBid = new Bid()
            {
                Amount = bid.Amount,
                ProjectFinishByDate = bid.ProjectFinishByFinishDate,
            };

            db.Posts.Find(bid.PostId).Bids.Add(newBid);
            db.SaveChanges();
            return RedirectToAction("Posts", "Posts");

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
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,ProjectFinishByDate,IsWinningBid,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] Post bid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
