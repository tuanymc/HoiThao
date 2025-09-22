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
    public class QLLoaiTinController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLLoaiTin
        public ActionResult Index()
        {
            //if (Session["Quyen"] == null || Session["Quyen"].ToString() != "full")
            //{
            //    return RedirectToAction("index", "QL");
            //}
            var loaiTins = db.LoaiTins.Where(m => m.Show == true && m.IDcha == null).ToList();
            return View(loaiTins.ToList());
        }

        // GET: QLLoaiTin/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LoaiTin loaiTin = db.LoaiTins.Find(id);
        //    if (loaiTin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(loaiTin);
        //}

        // GET: QLLoaiTin/Create
        public ActionResult Create()
        {
            ViewBag.IDcha = new SelectList(db.LoaiTins, "MaLT", "TenLT");
            return View();
        }

        // POST: QLLoaiTin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLT,TenLT,IDcha")] LoaiTin loaiTin)
        {
            if (ModelState.IsValid)
            {
                loaiTin.Show = true;
                loaiTin.khongdau = Cus.RewriteUrl(loaiTin.TenLT);
                var test = db.LoaiTins.Where(l => l.khongdau.Contains(loaiTin.khongdau));
                if (test != null)
                {
                    int count = test.ToList().Count;
                    string khongdau = "";
                    LoaiTin loai = new LoaiTin();
                    for (int i = 0; i <= count; i++)
                    {
                        khongdau = loaiTin.khongdau;
                        if (i > 0)
                        {
                            khongdau = khongdau + "-" + i.ToString();
                        }
                        loai = db.LoaiTins.Where(l => l.khongdau.Equals(khongdau)).FirstOrDefault();
                        if (loai == null)
                        {
                            loaiTin.khongdau = khongdau;
                            break;
                        }
                    }
                }
                db.LoaiTins.Add(loaiTin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDcha = new SelectList(db.LoaiTins, "MaLT", "TenLT", loaiTin.IDcha);
            return View(loaiTin);
        }

        // GET: QLLoaiTin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTin loaiTin = db.LoaiTins.Find(id);
            if (loaiTin == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDcha = new SelectList(db.LoaiTins.Where(x => x.MaLT != loaiTin.MaLT), "MaLT", "TenLT", loaiTin.IDcha);
            return View(loaiTin);
        }

        // POST: QLLoaiTin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLT,TenLT,IDcha")] LoaiTin loaiTin)
        {
            if (ModelState.IsValid)
            {
                LoaiTin edit = db.LoaiTins.Find(loaiTin.MaLT);
                string tenloai = edit.TenLT;
                if (!tenloai.Equals(loaiTin.TenLT))
                {
                    edit.TenLT = loaiTin.TenLT;
                    edit.khongdau = Cus.RewriteUrl(edit.TenLT);
                    var test = db.LoaiTins.Where(b => b.khongdau.Contains(edit.khongdau));
                    if (test != null)
                    {
                        int count = test.ToList().Count;
                        string khongdau = "";
                        LoaiTin check = new LoaiTin();
                        for (int i = 0; i <= count; i++)
                        {
                            khongdau = edit.khongdau;
                            if (i > 0)
                            {
                                khongdau = khongdau + "-" + i.ToString();
                            }
                            check = db.LoaiTins.Where(l => l.khongdau.Equals(khongdau)).FirstOrDefault();
                            if (check == null)
                            {
                                edit.khongdau = khongdau;
                                break;
                            }
                        }
                    }
                }
                edit.IDcha = loaiTin.IDcha;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDcha = new SelectList(db.LoaiTins.Where(x => x.MaLT != loaiTin.MaLT), "MaLT", "TenLT", loaiTin.IDcha);
            return View(loaiTin);
        }

        // GET: QLLoaiTin/Delete/5
        public ActionResult Delete(int?[] MaLT)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //LoaiTin loaiTin = db.LoaiTins.Find(id);
            //if (loaiTin == null)
            //{
            //    return HttpNotFound();
            //}
            return View(/*loaiTin*/);
        }

        // POST: QLLoaiTin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int?[] MaLT)
        {
            using (var dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in MaLT)
                    {
                        var listTin = db.Tins.Where(x => x.MaLT == item);
                        db.Tins.RemoveRange(listTin);
                        db.SaveChanges();


                        LoaiTin loaiTin = db.LoaiTins.Find(item);
                        db.LoaiTins.Remove(loaiTin);
                        db.SaveChanges();
                        
                    }
                    dbTrans.Commit();
                    return RedirectToAction("Index");


                    //if (loaiTin.LoaiTin1.ToList().Count() > 0 || loaiTin.Tins.Where(x => x.Show == true).ToList().Count() > 0)
                    //{
                    //    ViewBag.thongbao = "<div class='alert alert-danger'>Không thể xóa mục có chứa mục con hoặc bài đăng.</div>";
                    //    return View(loaiTin);
                    //}
                    //else
                    //{
                    //    var lst = db.Tins.Where(x => x.MaLT == loaiTin.MaLT);
                    //    foreach (var sub in lst)
                    //    {
                    //        db.Tins.Remove(sub);
                    //    }
                    //    db.LoaiTins.Remove(loaiTin);
                    //    db.SaveChanges();
                    //    dbTrans.Commit();
                    //    return RedirectToAction("Index");
                    //}
                }
                catch
                {
                    //ViewBag.thongbao = "<div class='alert alert-danger'><strong>Lỗi! </strong>Kiểm tra lại thông tin.</div>";
                    dbTrans.Rollback();
                    return RedirectToAction("Index");
                }
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
