using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;
using Newtonsoft.Json;

namespace KhoaXayDung.Controllers
{
    public class LienHeController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: LienHe
        //public ActionResult Index()
        //{
        //    return View(db.LienHes.ToList());
        //}

        // GET: LienHe/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LienHe lienHe = db.LienHes.Find(id);
        //    if (lienHe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lienHe);
        //}

        // GET: LienHe/Create
        public ActionResult Create()
        {
            Info iff = db.Infoes.FirstOrDefault(x => x.Ma == 1);
            ViewBag.Title = iff.Ten;
            return View();
        }

        // POST: LienHe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLH,Ten,ND,DC,Phone,Email,NgayDang,Show,TrangThaiXem")] LienHe lienHe)
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6LddowoTAAAAACOCZTRDUJqM59uOWs3DPxtImbeS";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return View();

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ViewBag.Message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        ViewBag.Message = "The secret parameter is invalid or malformed.";
                        break;

                    case ("missing-input-response"):
                        ViewBag.Message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        ViewBag.Message = "The response parameter is invalid or malformed.";
                        break;

                    default:
                        ViewBag.Message = "Error occured. Please try again";
                        break;
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    lienHe.Show = true;
                    lienHe.TrangThaiXem = false;
                    lienHe.NgayDang = DateTime.Now;
                    db.LienHes.Add(lienHe);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    ViewBag.tbdung = "Gửi thành công";
                }
                else
                {
                    ViewBag.tbsai = "Gửi thất bại";
                }
            }
            return View();
        }

        // GET: LienHe/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LienHe lienHe = db.LienHes.Find(id);
        //    if (lienHe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lienHe);
        //}

        // POST: LienHe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaLH,Ten,ND,DC,Phone,Email,NgayDang,Show,TrangThaiXem")] LienHe lienHe)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lienHe).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(lienHe);
        //}

        // GET: LienHe/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LienHe lienHe = db.LienHes.Find(id);
        //    if (lienHe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lienHe);
        //}

        // POST: LienHe/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LienHe lienHe = db.LienHes.Find(id);
        //    db.LienHes.Remove(lienHe);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
