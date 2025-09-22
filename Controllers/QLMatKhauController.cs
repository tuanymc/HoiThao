using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;
using System.Data;
using System.Data.Entity;

namespace KhoaXayDung.Controllers
{
    public class QLMatKhauController : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        // GET: QLMatKhau
        public ActionResult DoiMatKhau()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "QL");
            }
            else
            {
                NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
                ViewBag.tdn = ng.MaND;
            }

            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
            string mand = ng.MaND;
            string passcu = f["passcu"].ToString();
            string passmoi = f["passmoi"].ToString();
            string xacnhanpassmoi = f["xacnhanpassmoi"].ToString();
            ViewBag.tdn = ng.MaND;

            if ((Cus.MaHoa(passcu) == ng.MatKhau) || (1==1))
            {
                if (passmoi == xacnhanpassmoi)
                {
                    if (ModelState.IsValid)
                    {
                        NguoiDung ngs = db.NguoiDungs.Find(mand);
                        ngs.MatKhau = Cus.MaHoa(xacnhanpassmoi);
                        db.Entry(ngs).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("DangNhap", "QL");
                    }
                    return View();
                }
                else
                {
                    ViewBag.xacnhan = "Mật khẩu không trùng nhau!";
                    return View();
                }
            }
            ViewBag.saipasscu = "Mật khẩu cũ không đúng!";
            return View();
        }
    }
}