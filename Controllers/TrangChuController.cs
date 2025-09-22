using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;

namespace KhoaXayDung.Controllers
{
    public class TrangChuController : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        // GET: TrangChu
        public ActionResult Index()
        {
            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = iff.Ten;
            return View();
        }
        public PartialViewResult _PartialSlideShow()
        {
            //var show = db.Lideshow1();
            //return PartialView(show);
            return PartialView();
        }
        public PartialViewResult _PartialThongBaoSV()
        {
            var tin = db.Tins.Where(x => x.MaLT == 2 && x.Show == true).OrderByDescending(x => x.MaT).Take(5).ToList();
            return PartialView(tin);
        }
        public PartialViewResult _PartialThongBaoGV()
        {
            var tin = db.Tins.Where(x => x.MaLT == 3 && x.Show == true).OrderByDescending(x => x.MaT).Take(5).ToList();
            return PartialView(tin);
        }
        public PartialViewResult _PartialNCKH()
        {
            //var tin = db.Tins.Where(x => x.Show == true && (x.LoaiTin.MaNT==4)).OrderByDescending(x => x.NgayDang).Take(1).FirstOrDefault();
            //return PartialView(tin);
            return PartialView();
        }
        public PartialViewResult _PartialTinTongHop()
        {
            //var tin = db.Tins.Where(x => x.Show == true && x.LoaiTin.NhomTin.MaNT==1).OrderByDescending(x => x.MaT).Take(5).ToList();
            //return PartialView(tin);
            return PartialView();
        }
        public PartialViewResult _Partialfooter()
        {
            Info info = db.Infoes.Find(1);
            return PartialView(info);
        }
        public PartialViewResult _PartialTieuDiem()
        {
            var tin = db.Tins.Where(x => x.Show == true && x.MaLT==1).OrderByDescending(x => x.MaT).Take(1).ToList();
            return PartialView(tin);            
        }
        public PartialViewResult _Partialhddt()
        {
            var tin = db.Tins.Where(x => x.Show == true && (x.MaLT == 2 || x.MaLT==3)).OrderByDescending(x => x.MaT).Take(3).ToList();
            return PartialView(tin); 
        }
        public PartialViewResult _PartialThongTinDaoTao()
        {
            var tin = db.Tins.Where(x => x.Show == true && (x.MaLT == 1 || x.MaLT==39)).OrderByDescending(x => x.NgayDang).Take(1).FirstOrDefault();
            return PartialView(tin);
        }
        public PartialViewResult _PartialThongTinKhoa()
        {
            var tin = db.Tins.Where(x => x.Show == true && (x.MaLT == 2)).OrderByDescending(x => x.NgayDang).Take(1).FirstOrDefault();
            return PartialView(tin);
        }
        public PartialViewResult _PartialHoatDongDoanHoi()
        {
            var tin = db.Tins.Where(x => x.Show == true && (x.MaLT == 9 || x.MaLT==8 || x.MaLT==34)).OrderByDescending(x => x.NgayDang).Take(1).FirstOrDefault();
            return PartialView(tin);
        }
        public PartialViewResult _PartialThuVienThongTin()
        {
            var tin = db.Tins.Where(x => x.Show == true && (x.MaLT == 3)).OrderByDescending(x => x.NgayDang).Take(1).FirstOrDefault();
            return PartialView(tin);
        }
        public PartialViewResult _PartialLichCongTac()
        {
            var tin = db.Tins.Where(x => x.Show == true && (x.MaLT == 25)).OrderByDescending(x => x.NgayDang).Take(1).FirstOrDefault();
            return PartialView(tin);
        }

        public PartialViewResult _PartialLienKet()
        {
            var lstQC = db.QCLinks.Where(q => q.Show==true).OrderBy(q => q.STT).ToList();
            return PartialView(lstQC);
        }
    }
}