using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;

namespace KhoaXayDung.Controllers
{
    public class Home2022Controller : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: Home2022
        public ActionResult Index(int? id)
        {

            if (id == null)
            {
                var getPage = db.Pages.Find(3098);
                return View(getPage);
            }
            else
            {
                var getPage = db.Pages.Find(id);
                return View(getPage);
            }
        }

        public PartialViewResult _Menu()
        {
            return PartialView();
        }

        public ActionResult List(int? id)
        {
            var list = db.LayDanhSachTinTucTheoLoai(id).ToList();
            return View(list);
        }

        public ActionResult Detail(int? id)
        {

            var detail = db.Tins.Find(id);
            detail.Xem = detail.Xem + 1;
            db.SaveChanges();
            return View(detail);
        }
    }
}