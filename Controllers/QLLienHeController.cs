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
    public class QLLienHeController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLLienHe
        public ActionResult Index()
        {
            return View(db.LienHes.OrderByDescending(p => p.NgayDang).ToList());
        }

        // GET: QLLienHe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // GET: QLLienHe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLLienHe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLH,Ten,ND,DC,Phone,Email,NgayDang,Show,TrangThaiXem")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                db.LienHes.Add(lienHe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lienHe);
        }

        // GET: QLLienHe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // POST: QLLienHe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLH,Ten,ND,DC,Phone,Email,NgayDang,Show,TrangThaiXem")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lienHe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lienHe);
        }

        // GET: QLLienHe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // POST: QLLienHe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LienHe lienHe = db.LienHes.Find(id);
            db.LienHes.Remove(lienHe);
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
