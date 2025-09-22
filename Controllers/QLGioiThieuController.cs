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
    public class QLGioiThieuController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLGioiThieu
        public ActionResult Index()
        {
            var tins = db.Tins.Include(t => t.LoaiTin).Include(t => t.NguoiDung).Where(x=>x.MaLT==10);
            return View(tins.ToList());
        }

        // GET: QLGioiThieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tin tin = db.Tins.Find(id);
            if (tin == null)
            {
                return HttpNotFound();
            }
            return View(tin);
        }

        // GET: QLGioiThieu/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT");
        //    ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "HT");
        //    return View();
        //}

        //// POST: QLGioiThieu/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaT,TenT,MT,ND,H,F,NgayDang,Xem,MaLT,Link,Show,MaND")] Tin tin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tins.Add(tin);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT", tin.MaLT);
        //    ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "HT", tin.MaND);
        //    return View(tin);
        //}

        // GET: QLGioiThieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tin tin = db.Tins.Find(id);
            if (tin == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT", tin.MaLT);
            ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "HT", tin.MaND);
            return View(tin);
        }

        // POST: QLGioiThieu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaT,TenT,MT,ND,H,F,NgayDang,Xem,MaLT,Link,Show,MaND")] Tin tin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT", tin.MaLT);
            ViewBag.MaND = new SelectList(db.NguoiDungs, "MaND", "HT", tin.MaND);
            return View(tin);
        }

        // GET: QLGioiThieu/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tin tin = db.Tins.Find(id);
        //    if (tin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tin);
        //}

        //// POST: QLGioiThieu/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tin tin = db.Tins.Find(id);
        //    db.Tins.Remove(tin);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
