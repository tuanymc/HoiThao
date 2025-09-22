using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;

namespace KhoaXayDung.Controllers
{
    public class Home2019Controller : Controller
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GopYPhanHoi()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GopYPhanHoi([Bind(Include = "MaLH,Ten,ND,DC,Phone,Email,NgayDang,Show,TrangThaiXem")] LienHe lienHe)
        {
            string[] arrhoten = lienHe.Ten.Trim().Split(' ');
            if (arrhoten.Count() < 2)
            {
                ViewBag.thongbao = "<div class='alert alert-danger'>Nhập đầy đủ họ và tên.</div>";
                return View(lienHe);
            }
            string[] arremail = lienHe.Email.Split('@');
            string email = arremail[0];
            if (email.Length < 4)
            {
                ViewBag.thongbao = "<div class='alert alert-danger'>Email không hợp lệ!</div>";
                return View(lienHe);
            }
            LienHe test = db.LienHes.Where(l => l.Email == lienHe.Email || l.Phone == lienHe.Phone).OrderByDescending(l => l.NgayDang).FirstOrDefault();
            if (test != null)
            {
                TimeSpan span = DateTime.Now - (DateTime)test.NgayDang;
                double mins = span.TotalMinutes;
                if (mins <= 30)
                {
                    int conlai = Convert.ToInt32(30 - mins);
                    ViewBag.thongbao = "<div class='alert alert-danger'>Bạn vừa gửi nội dung liên hệ. Thử lại sau " + conlai + " phút.</div>";
                    ModelState.Clear();
                    return View();
                }
            }

            lienHe.Show = true;
            lienHe.TrangThaiXem = false;
            lienHe.NgayDang = DateTime.Now;
            db.LienHes.Add(lienHe);
            var res = db.SaveChanges();
            ModelState.Clear();

            if (res > 0)
            {
                ViewBag.thongbao = "<div class='alert alert-success'>Gửi nội dung liên hệ thành công!</div>";
                return View();
            }
            else
            {
                ViewBag.thongbao = "<div class='alert alert-danger'>Gửi nội dung liên hệ thất bại!</div>";
            }

            //string html = "";
            //html += "<h3>Liên hệ</h3>";
            //html += @"<table style='border:0px; width:auto;text-align:left;' cellspacing=0 cellpadding=5  '>
            //                 <tbody>";
            //html += "<tr><th style='width:15%;'>Họ tên:</th><td>" + lienHe.Ten + "</td></tr>";
            //html += "<tr><th>Số điện thoại:</th><td>" + lienHe.Phone + "</td></tr>";
            //html += "<tr><th>Email:</th><td>" + lienHe.Email + "</td></tr>";
            //html += "<tr><th>Địa chỉ:</th><td>" + lienHe.DC + "</td></tr>";
            //html += "<tr><th valign=top>Nội dung liên hệ:</th><td align=justify>" + lienHe.ND + "</td></tr>";
            //html += @"</tbody>
            //            </table>";
            //string sub = lienHe.Ten + " - " + DateTime.Now;

            //var sendMail = new SendMail();
            //sendMail.to = "phongdaotao@tdmu.edu.vn";
            //sendMail.message = html;
            //sendMail.subject = sub;

            //bool res = Cus.Send(sendMail);

            //if (res != false)
            //{
            //    lienHe.Show = true;
            //    lienHe.TrangThaiXem = false;
            //    lienHe.NgayDang = DateTime.Now;
            //    db.LienHes.Add(lienHe);
            //    db.SaveChanges();
            //    ModelState.Clear();
            //    ViewBag.thongbao = "<div class='alert alert-success'>Gửi nội dung liên hệ thành công!</div>";
            //    return View();
            //}
            //else
            //{
            //    ViewBag.thongbao = "<div class='alert alert-danger'>Gửi nội dung liên hệ thất bại!</div>";
            //}

            return View();
        }

        public ActionResult _Header()
        {
            return PartialView();
        }

        public ActionResult _Footer()
        {
            var info = db.Infoes.Find(1);
            return PartialView(info);
        }

        public PartialViewResult _Menu()
        {
            return PartialView();
        }

        public ActionResult List(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }
            ViewBag.IdNhom = id;
            return View();
        }

        public ActionResult Content(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var content = db.Tins.Where(m => m.Show == true && m.MaT == id).FirstOrDefault();
            if (content == null)
            {
                return RedirectToAction("Index");
            }
            if (content.F != null)
            {
                ViewBag.pageCount = Cus.pageCount(Path.Combine(Server.MapPath("~/img/bt3/files/"), content.F));
            }
            return View(content);
        }
    }
}