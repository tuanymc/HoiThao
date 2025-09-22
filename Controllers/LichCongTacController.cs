using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;

namespace KhoaXayDung.Controllers
{
    public class LichCongTacController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();
        //
        // GET: /LichCongTac/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string link)
        {
            if (link == null)
            {
                return HttpNotFound();
            }
            var lich = db.Tins.Where(x => x.Show == true && x.MaLT == 45 && x.Link.Equals(link)).FirstOrDefault();
            if (lich == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        public PartialViewResult _PartialMenuLich()
        {
            int year = DateTime.Now.Year;
            int preYear = year - 1;
            var lich = db.Tins.Where(x => x.Show == true && x.MaLT == 45 && (x.NgayDang.Value.Year == year || (x.TenT.Equals("01") && x.NgayDang.Value.Month == 12 && x.NgayDang.Value.Year == preYear))).OrderByDescending(x => x.NgayDang).ToList();
            return PartialView(lich);
        }
        public PartialViewResult _PartialLichIndex()
        {
            int year = DateTime.Now.Year;
            int preyear = year - 1;
            var lich = db.Tins.Where(x => x.Show == true && x.MaLT == 45 && (x.NgayDang.Value.Year == year || (x.TenT.Equals("01") && x.NgayDang.Value.Year == preyear && x.NgayDang.Value.Month == 12))).ToList();
            var output = lich.OrderByDescending(x => x.NgayDang).Take(1).FirstOrDefault();
            if (output != null)
            {
                ViewBag.tenlich = "Lịch công tác tuần " + output.TenT;
                if (ModelState.IsValid)
                {
                    Tin bds = db.Tins.Find(output.MaT);
                    bds.Xem = bds.Xem + 1;
                    db.SaveChanges();
                }
            }
            return PartialView(output);
        }
        public PartialViewResult _PartialDetailIndex(string link)
        {
            var lich = db.Tins.Where(x => x.Show == true && x.MaLT == 45 && x.Link.Equals(link)).FirstOrDefault();
            if (lich != null)
            {
                lich.Xem = lich.Xem + 1;
                db.SaveChanges();
                ViewBag.tenlich = "Lịch công tác tuần " + lich.TenT;
            }
            return PartialView(lich);
        }
    }
}