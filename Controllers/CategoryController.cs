using KhoaXayDung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KhoaXayDung.Controllers
{
    public class CategoryController : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        // GET: Category
        public ActionResult Index(string link)
        {
            var tin = db.Tins.Where(x => x.Link.Equals(link)).FirstOrDefault();
            if (tin == null || tin.Show == false || tin.MaLT == 45)
            {
                return HttpNotFound();
            }
            if (tin.F != null)
            {
                ViewBag.pageCount = Cus.pageCount(Server.MapPath("/") + "/img/ckeditor/Files/" + tin.F);
            }
            tin.Xem = tin.Xem + 1;
            db.SaveChanges();
            var lt = db.LoaiTins.Where(x => x.MaLT == tin.MaLT).FirstOrDefault();
            ViewBag.tenlt = lt.TenLT;
            if (lt.MaLT == 1059)
            {
                ViewBag.LinkDanhMuc = "/danh-muc-tin-tuc/" + tin.LoaiTin.khongdau;
            }
            else if (lt.MaLT == 1060)
            {
                ViewBag.LinkDanhMuc = "/danh-muc-thong-bao/" + tin.LoaiTin.khongdau;
            }
            

            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(iff.Ten.ToLower());
            return View(tin);
        }
        public ActionResult Pages(string link)
        {


            var tin = db.Pages.Where(x => x.Link.Equals(link)).FirstOrDefault();
            if (tin == null || tin.Show == false)
            {
                return HttpNotFound();
            }
            if (tin.F != null)
            {
                ViewBag.pageCount = Cus.pageCount(Server.MapPath("/") + "/img/ckeditor/Files/" + tin.F);
            }
            tin.Xem = tin.Xem + 1;
            db.SaveChanges();

            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(iff.Ten.ToLower());
            string breadcrumb = "";
            var menu = db.ChitietMenus.Where(w => w.IdPage == tin.MaT).FirstOrDefault();

            if (menu != null)
            {
                var menucha = db.ChitietMenus.Where(w => w.ID == menu.IdCha).FirstOrDefault();
                if (menucha != null)
                {
                    if (menucha.Page != null)
                    {
                        breadcrumb = menucha.Page.TenT.ToString();
                    }
                    if (menucha.LoaiTin != null)
                    {
                        breadcrumb = menucha.LoaiTin.TenLT.ToString();
                    }
                    if (menucha.LinkTuDo != null)
                    {
                        breadcrumb = menucha.LinkTuDo.TenLink.ToString();
                    }
                }
                else
                {
                    breadcrumb = tin.TenT;
                }
            }



            ViewBag.Breadcrumb = breadcrumb;
            return View(tin);
        }

        public ActionResult List(string khong_dau)
        {


            var lt = db.LoaiTins.Where(x => x.khongdau.Equals(khong_dau)).FirstOrDefault();
            if (lt == null || lt.MaLT == 45)
            {
                return HttpNotFound();
            }
            var list = db.Tins.Where(x => x.MaLT == lt.MaLT && x.Show == true).OrderByDescending(x => x.NgayDang).ToList();
            ViewBag.tenlt = lt.TenLT;
            ViewBag.malt = lt.MaLT;

            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(iff.Ten.ToLower());
            return View(list);
        }
        public PartialViewResult _TinCungLoai(string link)
        {


            var tin = db.Tins.Where(x => x.Link.Equals(link)).FirstOrDefault();
            var lt = db.LoaiTins.Where(x => x.MaLT == tin.MaLT).FirstOrDefault();
            var list = db.Tins.Where(x => x.MaLT == lt.MaLT && x.Show == true && x.MaT != tin.MaT).OrderByDescending(x => x.NgayDang).Take(7).ToList();
            return PartialView(list);
        }
    }
}