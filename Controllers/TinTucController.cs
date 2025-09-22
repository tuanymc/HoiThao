using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;
using System.Net;
using System.Data;
using System.Data.Entity;

namespace KhoaXayDung.Controllers
{
    public class TinTucController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();
        // GET: TinTuc
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tin tin = db.Tins.Single(p => p.MaT == id);
            if (tin == null)
            {
                return HttpNotFound();
            }
            else
            {
                tin.Xem = tin.Xem + 1;
                db.Entry(tin).State = EntityState.Modified;
                db.SaveChanges();
            }
            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = iff.Ten;
            return View(tin);
        }
        public PartialViewResult _PartialTinCungLoai(int? id)
        {
            Tin tin = db.Tins.Single(p => p.MaT == id);

            int? maloai = tin.MaLT;
            int? matin = tin.MaT;

            var tins = db.Tins.Where(x => x.MaLT == maloai && x.Show == true && x.MaT != matin).OrderByDescending(x => x.MaT).Take(10).ToList();
            return PartialView(tins);
        }
        public ActionResult DanhSach(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tin = db.Tins.Where(x => x.MaLT == id).Where(x => x.Show == true).OrderByDescending(x => x.MaT).ToList();
            if (tin == null)
            {
                return HttpNotFound();
            }
            LoaiTin lt = db.LoaiTins.Single(xx => xx.MaLT == id);
            ViewBag.tenloai = lt.TenLT;
            //ViewBag.tennhom = lt.NhomTin.TenNT;

            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = iff.Ten;
            return View(tin);
        }
    }
}