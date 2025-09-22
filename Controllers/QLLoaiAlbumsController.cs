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
    public class QLLoaiAlbumsController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLLoaiAlbums
        public ActionResult Index()
        {
            return View(db.LoaiAlbums.ToList());
        }

        // GET: QLLoaiAlbums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiAlbum loaiAlbum = db.LoaiAlbums.Find(id);
            if (loaiAlbum == null)
            {
                return HttpNotFound();
            }
            return View(loaiAlbum);
        }

        // GET: QLLoaiAlbums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLLoaiAlbums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAlbum,TenAlbum")] LoaiAlbum loaiAlbum)
        {
            if (ModelState.IsValid)
            {
                db.LoaiAlbums.Add(loaiAlbum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiAlbum);
        }

        // GET: QLLoaiAlbums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiAlbum loaiAlbum = db.LoaiAlbums.Find(id);
            if (loaiAlbum == null)
            {
                return HttpNotFound();
            }
            return View(loaiAlbum);
        }

        // POST: QLLoaiAlbums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAlbum,TenAlbum")] LoaiAlbum loaiAlbum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiAlbum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiAlbum);
        }

        // GET: QLLoaiAlbums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiAlbum loaiAlbum = db.LoaiAlbums.Find(id);
            if (loaiAlbum == null)
            {
                return HttpNotFound();
            }
            return View(loaiAlbum);
        }

        // POST: QLLoaiAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiAlbum loaiAlbum = db.LoaiAlbums.Find(id);
            db.LoaiAlbums.Remove(loaiAlbum);
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
