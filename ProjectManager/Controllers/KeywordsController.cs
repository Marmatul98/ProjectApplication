using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManager.DAL;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class KeywordsController : Controller
    {
        private ManagerContext db = new ManagerContext();

        // GET: Keywords
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Keywords.ToList());
        }

        // GET: Keywords/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = db.Keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // GET: Keywords/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Keywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeywordID,Name")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Keywords.Add(keyword);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(keyword);
        }

        // GET: Keywords/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = db.Keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: Keywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeywordID,Name")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyword).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: Keywords/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = db.Keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: Keywords/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Keyword keyword = db.Keywords.Find(id);
            db.Keywords.Remove(keyword);
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
