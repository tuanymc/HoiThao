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
    public class QLInfoController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: Info
        public ActionResult Index()
        {
            return View(db.Infoes.ToList());
        }

        // GET: Info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infoes.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // GET: Info/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Info/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Ma,Ten,DC,Phone,Tel,Mota,Logan")] Info info)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Infoes.Add(info);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(info);
        //}

        // GET: Info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infoes.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // POST: Info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma,Ten,DC,Phone,Tel,Mota,Logan,Email")] Info info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(info);
        }

        // GET: Info/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Info info = db.Infoes.Find(id);
        //    if (info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(info);
        //}

        //// POST: Info/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Info info = db.Infoes.Find(id);
        //    db.Infoes.Remove(info);
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
