using KhoaXayDung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KhoaXayDung.Controllers
{
    public class HomeController : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        // GET: Home
        public ActionResult Index()
        {
            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(iff.Ten.ToLower());
            return View();
        }
        public PartialViewResult _MenuPhai()
        {
            return PartialView();
        }
        public PartialViewResult _MenuPhai1()
        {
            return PartialView();
        }

        public PartialViewResult _ThongKeTruyCapHome()
        {
            return PartialView();
        }
        public PartialViewResult _MenuNgang()
        {
            return PartialView();
        }
        public PartialViewResult _TinTuc()
        {
            var tin = db.Tins.Where(x => (x.MaLT == 41 || x.MaLT == 49 || x.MaLT == 50) && x.Show == true).Take(4).OrderByDescending(x => x.NgayDang).ToList();
            return PartialView(tin);
        }
        public PartialViewResult _ThongBao()
        {
            var tin = db.Tins.Where(x => x.MaLT == 46 && x.Show == true).Take(5).OrderByDescending(x => x.NgayDang).ToList();
            return PartialView(tin);
        }
        public PartialViewResult _LienKetWeb()
        {
            var lk = db.QCLinks.Where(x=>x.Show==true).ToList();
            return PartialView(lk);
        }
        public PartialViewResult _Slide()
        {
            var slide = db.Slides.Where(x=>x.Show==true).OrderByDescending(x => x.MaSlide).Take(5).ToList();
            return PartialView(slide);
        }
    }
}