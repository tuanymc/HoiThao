using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;

namespace KhoaXayDung.Controllers
{
    public class QLController : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        // GET: QL
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            ViewBag.anhbia = Cus.Spotlight();
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string ten = f["MaND"].ToString();
            string mk = Cus.MaHoa(f["MatKhau"].ToString());
            
            NguoiDung nd = db.NguoiDungs.SingleOrDefault(n => n.MaND == ten && n.MatKhau == mk && n.Show == true);
            //NguoiDung nd = db.NguoiDungs.SingleOrDefault(n => n.MaND == ten && n.Show == true);
            if (nd != null)
            {
                Session["TaiKhoan"] = nd;
                Session["tendangnhap"] = ten;

                // begin kiem tra quyen
                var ngdung = db.NguoiDungs.Where(x => x.MaND == ten && x.MaQuyen == 1).ToList();
                if (ngdung.Count != 0)
                {
                    Session["Quyen"] = "full";
                }
                else
                {
                    Session["Quyen"] = "dangtin";
                }
                // end kiem tra quyen

                return RedirectToAction("Index");

            }
            ViewBag.anhbia = Cus.Spotlight();
            ViewBag.thongbao = "<div class='alert alert-danger' role='alert'>Sai tên đăng nhập hoặc mật khẩu!</div>";
            return View();
        }
        public ActionResult Thoat()
        {
            Session.Clear();
            return RedirectToAction("DangNhap", "QL");
        }
        public PartialViewResult _PartialLogin()
        {
            if (Session["TaiKhoan"] == null || Session["Quyen"] == null)
            {
                Response.Redirect("~/QL/DangNhap");
            }
            else
            {
                NguoiDung nd = Session["TaiKhoan"] as NguoiDung;
                ViewBag.HT = nd.HT;
            }
            return PartialView();
        }
        public PartialViewResult _PartialTenInFo()
        {
            Info info = db.Infoes.Find(1);
            ViewBag.Ten = info.Ten;            
            return PartialView();
        }
    }
}