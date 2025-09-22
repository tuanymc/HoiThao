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
    public class QLLoaiSlideController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: /QLLoaiSlide/
        public ActionResult Index()
        {
            return View(db.LoaiSlides.ToList());
        }

        // GET: /QLLoaiSlide/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LoaiSlide loaislide = db.LoaiSlides.Find(id);
        //    if (loaislide == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(loaislide);
        //}

        // GET: /QLLoaiSlide/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /QLLoaiSlide/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaLoaiSlide,TenLoaiSlide")] LoaiSlide loaislide)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSlides.Add(loaislide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaislide);
        }

        // GET: /QLLoaiSlide/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSlide loaislide = db.LoaiSlides.Find(id);
            if (loaislide == null)
            {
                return HttpNotFound();
            }
            return View(loaislide);
        }

        // POST: /QLLoaiSlide/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaLoaiSlide,TenLoaiSlide")] LoaiSlide loaislide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaislide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaislide);
        }

        // GET: /QLLoaiSlide/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSlide loaislide = db.LoaiSlides.Find(id);
            if (loaislide == null)
            {
                return HttpNotFound();
            }
            return View(loaislide);
        }

        // POST: /QLLoaiSlide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSlide loaislide = db.LoaiSlides.Find(id);
            if(loaislide.Slides.ToList().Count()>0)
            {
                ViewBag.thongbao = "<div class='alert alert-danger'>Không thể xóa loại có mục con.</div>";
                return View(loaislide);
            }
            else
            {
                db.LoaiSlides.Remove(loaislide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
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
