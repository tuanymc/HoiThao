using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;
using System.Xml;

namespace KhoaXayDung.Controllers
{
    public class WebsiteController : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        // GET: Website
        public ActionResult Index()
        {
            var info = db.Infoes.Find(1);
            ViewBag.Info = info.Ten;
            return View();
        }

        public new ActionResult Content(string link)
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

        public ActionResult ListNotification(string khong_dau)
        {
            var lt = db.LoaiTins.Where(x => x.khongdau.Equals(khong_dau)).FirstOrDefault();
            if (lt == null)
            {
                return HttpNotFound();
            }
            if (lt.MaLT == 1060)
            {
                ViewBag.LoaiTin = lt.khongdau;
                ViewBag.TenLoaiTin = lt.TenLT;
                var list = db.Tins.Where(x => (x.MaLT == 2075 || x.MaLT == 2076 || x.MaLT == 2077 || x.MaLT == 2078) && x.Show == true).OrderByDescending(x => x.NgayDang).ToList();
                return View(list);
            }
            else
            {
                ViewBag.LoaiTin = lt.khongdau;
                ViewBag.TenLoaiTin = lt.TenLT;
                var list = db.Tins.Where(x => x.MaLT == lt.MaLT && x.Show == true).OrderByDescending(x => x.NgayDang).ToList();
                return View(list);
            }

        }

        public ActionResult ListNews(string khong_dau)
        {
            var lt = db.LoaiTins.Where(x => x.khongdau.Equals(khong_dau)).FirstOrDefault();
            if (lt == null)
            {
                return HttpNotFound();
            }

            ViewBag.LoaiTin = lt.khongdau;
            ViewBag.TenLoaiTin = lt.TenLT;
            var list = db.Tins.Where(x => x.MaLT == lt.MaLT && x.Show == true).OrderByDescending(x => x.NgayDang).ToList();
            return View(list);
        }

        public PartialViewResult _PartialNews()
        {
            var listNews = db.Tins.Where(m => m.MaLT == 2064).Take(10).OrderByDescending(m => m.NgayDang).ToList();
            if (listNews.Count > 0)
            {
                ViewBag.LoaiTin = listNews.FirstOrDefault().LoaiTin.TenLT;
                ViewBag.LinkLoaiTin = listNews.FirstOrDefault().LoaiTin.khongdau;
                return PartialView(listNews);
            }
            else
            {
                return PartialView(listNews);
            }
        }

        public PartialViewResult _MenuNgang()
        {
            return PartialView();
        }

        public PartialViewResult _Carousel()
        {
            var slide = db.Slides.Where(x => x.Show == true && x.MaLoai == 1006).OrderByDescending(x => x.MaSlide).Take(5).ToList();
            return PartialView(slide);
        }

        public PartialViewResult _PartialNotification()
        {
            var listNotification = db.Tins.Where(m => m.LoaiTin.IDcha == 1060).Take(10).OrderByDescending(m => m.NgayDang).ToList();

            var idCha = listNotification.FirstOrDefault().LoaiTin.IDcha;

            var titleNotification = db.LoaiTins.Where(m => m.MaLT == idCha).FirstOrDefault().TenLT;

            var linkLoaiTinCha = db.LoaiTins.Find(idCha).khongdau;

            if (listNotification.Count > 0)
            {
                ViewBag.LoaiTin = titleNotification;
                ViewBag.LinkLoaiTin = linkLoaiTinCha;
                return PartialView(listNotification);
            }
            else
            {
                return PartialView(listNotification);
            }

        }

        public PartialViewResult _PartialUpcomingEvents()
        {
            var listUpcomingEvents = db.Tins.Where(m => m.MaLT == 2059).OrderByDescending(m => m.MaT).ToList();
            if (listUpcomingEvents.Count > 0)
            {
                ViewBag.LoaiTin = listUpcomingEvents.FirstOrDefault().LoaiTin.TenLT;
                return PartialView(listUpcomingEvents);
            }
            else
            {
                return PartialView(listUpcomingEvents);
            }
        }

        public PartialViewResult _PartialLinks()
        {
            var listLinks = db.Tins.Where(m => m.MaLT == 2060).Take(4).OrderByDescending(m => m.MaT).ToList();
            if (listLinks.Count > 0)
            {
                ViewBag.LoaiTin = listLinks.FirstOrDefault().LoaiTin.TenLT;
                return PartialView(listLinks);
            }
            else
            {
                return PartialView(listLinks);
            }
        }

        public PartialViewResult _PartialStudentEnterpriseTradition()
        {
            // Return all list off category Student, Enterprise, Tradition
            var listStudentEnterpriseTradition = db.LoaiTins.OrderBy(m => m.MaLT).ToList();
            return PartialView(listStudentEnterpriseTradition);
        }

        public PartialViewResult _PartialHuongNghiep()
        {
            var listHuongNghiep = db.Tins.Where(m => m.MaLT == 2161).Take(3).OrderByDescending(m => m.NgayDang).ToList();
            if (listHuongNghiep.Count > 0)
            {
                ViewBag.LoaiTin = listHuongNghiep.FirstOrDefault().LoaiTin.TenLT;
                ViewBag.Anh = listHuongNghiep.FirstOrDefault().H;
                ViewBag.LinkLoaiTin = listHuongNghiep.FirstOrDefault().LoaiTin.khongdau;
                return PartialView(listHuongNghiep);
            }
            else
            {
                return PartialView(listHuongNghiep);
            }
        }

        public PartialViewResult _PartialDoiSongSinhVien()
        {
            var listDoiSongSinhVien = db.Tins.Where(m => m.MaLT == 2162).Take(3).OrderByDescending(m => m.NgayDang).ToList();
            if (listDoiSongSinhVien.Count > 0)
            {
                ViewBag.LoaiTin = listDoiSongSinhVien.FirstOrDefault().LoaiTin.TenLT;
                ViewBag.Anh = listDoiSongSinhVien.FirstOrDefault().H;
                ViewBag.LinkLoaiTin = listDoiSongSinhVien.FirstOrDefault().LoaiTin.khongdau;
                return PartialView(listDoiSongSinhVien);
            }
            else
            {
                return PartialView(listDoiSongSinhVien);
            }
        }

        public PartialViewResult _PartialVideo()
        {
            var listVideo = db.AlbumVideoHinhAnhs.Where(m => m.LinkNhung != null).OrderByDescending(m => m.IdAlbum).Take(3).ToList();
            return PartialView(listVideo);
        }

        public PartialViewResult _PartialPictures()
        {
            var listPictures = db.AlbumVideoHinhAnhs.Where(m => m.LinkNhung == null).OrderByDescending(m => m.IdVideoHinhAnh).Take(5).ToList();
            if (listPictures.Count > 0)
            {
                ViewBag.LinkAnh = listPictures.FirstOrDefault().IdAlbum;
            }
            return PartialView(listPictures);
        }

        public PartialViewResult _PartialDoiTac()
        {
            var listDoiTac = db.Slides.Where(m => m.MaLoai == 1007).OrderByDescending(m => m.MaSlide).ToList();
            return PartialView(listDoiTac);
        }

        public ActionResult AlbumHoatDong(int? ids)
        {
            if (ids != null)
            {
                var listHoatDong = db.AlbumVideoHinhAnhs.Where(m => m.IdVideoHinhAnh == ids).ToList();
                if (listHoatDong.FirstOrDefault().LinkNhung == null)
                {
                    ViewBag.TenBreadcrumb = "Hình ảnh hoạt động";
                }
                else
                {
                    ViewBag.TenBreadcrumb = "Video hoạt động";
                    ViewBag.LinkNhung = listHoatDong.FirstOrDefault().LinkNhung;
                }
                return View(listHoatDong);
            }
            else
            {
                ViewBag.TenBreadcrumb = "Video và hình ảnh hoạt động";
                var listHoatDong = db.AlbumVideoHinhAnhs.Where(m => m.IdVideoHinhAnh == ids).ToList();
                return View(listHoatDong);
            }
        }

        public PartialViewResult _MenuTop()
        {
            return PartialView();
        }
    }
}
