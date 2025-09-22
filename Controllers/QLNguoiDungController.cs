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
    public class QLNguoiDungController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLNguoiDung
        public ActionResult Index()
        {
            if (Session["Quyen"] == null || Session["Quyen"].ToString() != "full")
            {
                return RedirectToAction("index", "QL");
            }
            var nguoiDungs = db.NguoiDungs.Include(n => n.Quyen);
            return View(nguoiDungs.ToList());
        }

        // GET: QLNguoiDung/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: QLNguoiDung/Create
        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        // POST: QLNguoiDung/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaND,HT,MatKhau,Email,GT,DT,DC,NS,MaQuyen,Show")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                nguoiDung.MatKhau = Cus.MaHoa(nguoiDung.MatKhau);
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", nguoiDung.MaQuyen);
            return View(nguoiDung);
        }

        // GET: QLNguoiDung/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", nguoiDung.MaQuyen);
            return View(nguoiDung);
        }

        // POST: QLNguoiDung/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaND,HT,MatKhau,Email,GT,DT,DC,NS,MaQuyen,Show")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                nguoiDung.MatKhau = Cus.MaHoa(nguoiDung.MatKhau);
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", nguoiDung.MaQuyen);
            return View(nguoiDung);
        }

        // GET: QLNguoiDung/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: QLNguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nguoiDung);
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
