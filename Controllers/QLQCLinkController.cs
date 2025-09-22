using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;
using System.IO;
using System.Drawing;

namespace KhoaXayDung.Controllers
{
    public class QLQCLinkController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLQCLink
        public ActionResult Index()
        {
            var qCLinks = db.QCLinks.Include(q => q.NguoiDung).OrderBy(q=>q.STT);
            return View(qCLinks.ToList());
        }

        // GET: QLQCLink/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QCLink qCLink = db.QCLinks.Find(id);
            if (qCLink == null)
            {
                return HttpNotFound();
            }
            return View(qCLink);
        }

        // GET: QLQCLink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLQCLink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQC,TenQC,Link,H,Show,STT")] QCLink qCLink, HttpPostedFileBase H)
        {
            if (ModelState.IsValid)
            {
                NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
                qCLink.MaND = ng.MaND;
                if (H != null)
                {
                    //Stream s = H.InputStream;
                    //Image i = System.Drawing.Image.FromStream(s);

                    //int width = i.Width;
                    //int height = i.Height;
                    //float res = i.HorizontalResolution;

                    //if (width < height || width > height*2 || res != 72)
                    //{
                    //    ViewBag.imgErr = "<div class='alert alert-danger'>Chọn ảnh có kích thước !</div>";
                    //    return View(qCLink);
                    //}
                    //else
                    //{
                    //    var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(H.FileName);
                    //    var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);
                    //    qCLink.H = tenhinh;
                    //    H.SaveAs(ddhinh);
                    //}

                    var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(H.FileName);
                    var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);
                    qCLink.H = tenhinh;
                    H.SaveAs(ddhinh);
                }
                db.QCLinks.Add(qCLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qCLink);
        }

        // GET: QLQCLink/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QCLink qCLink = db.QCLinks.Find(id);
            if (qCLink == null)
            {
                return HttpNotFound();
            }
            return View(qCLink);
        }

        // POST: QLQCLink/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQC,TenQC,Link,H,Show,STT")] QCLink qCLink, HttpPostedFileBase H)
        {
            if (ModelState.IsValid)
            {
                NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
                QCLink qc = db.QCLinks.Find(qCLink.MaQC);
                qc.TenQC = qCLink.TenQC;
                qc.Link = qCLink.Link;
                qc.Show = qCLink.Show;
                qc.MaND = ng.MaND;
                qc.STT = qCLink.STT;
                if (H != null)
                {
                    //Stream s = H.InputStream;
                    //Image i = System.Drawing.Image.FromStream(s);

                    //int width = i.Width;
                    //int height = i.Height;
                    //float res = i.HorizontalResolution;

                    //if (width < height || width > height * 2 || res != 72)
                    //{
                    //    ViewBag.imgErr = "<div class='alert alert-danger'>Chọn ảnh có kích thước 320x200(pixel) và độ phân giải 72(dpi)!</div>";
                    //    return View(qCLink);
                    //}
                    //else
                    //{
                    //    var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(H.FileName);
                    //    var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);
                    //    qc.H = tenhinh;
                    //    H.SaveAs(ddhinh);
                    //}

                    var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(H.FileName);
                    var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);
                    qc.H = tenhinh;
                    H.SaveAs(ddhinh);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qCLink);
        }

        // GET: QLQCLink/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QCLink qCLink = db.QCLinks.Find(id);
            if (qCLink == null)
            {
                return HttpNotFound();
            }
            return View(qCLink);
        }

        // POST: QLQCLink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QCLink qCLink = db.QCLinks.Find(id);
            db.QCLinks.Remove(qCLink);
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
