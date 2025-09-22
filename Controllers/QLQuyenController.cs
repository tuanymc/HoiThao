using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;

namespace KhoaXayDung.Controllers
{
    public class QLQuyenController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLQuyen
        public ActionResult Index()
        {
            if (Session["Quyen"]==null || Session["Quyen"].ToString() != "full")
            {
                return RedirectToAction("index", "QL");
            }
            return View(db.Quyens.ToList());
        }

        // GET: QLQuyen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quyen quyen = db.Quyens.Find(id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }

        // GET: QLQuyen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLQuyen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQuyen,TenQuyen,Show")] Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                db.Quyens.Add(quyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quyen);
        }

        // GET: QLQuyen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quyen quyen = db.Quyens.Find(id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }

        // POST: QLQuyen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQuyen,TenQuyen,Show")] Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quyen);
        }

        // GET: QLQuyen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quyen quyen = db.Quyens.Find(id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }

        // POST: QLQuyen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quyen quyen = db.Quyens.Find(id);
            db.Quyens.Remove(quyen);
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
