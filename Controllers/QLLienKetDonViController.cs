using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace KhoaXayDung.Controllers
{
    public class QLLienKetDonViController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: /QLSlide/
        public ActionResult Index()
        {
            return View(db.Slides.Where(m => m.MaLoai == 1007).ToList());
        }

        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

        // GET: /QLSlide/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSlides, "MaLoaiSlide", "TenLoaiSlide");
            return View();
        }

        // POST: /QLSlide/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSlide,MaLoai,TenSlide,LinkSide,MoTaSlide,Show")] Slide slide, HttpPostedFileBase AnhSlide)
        {
            if (ModelState.IsValid)
            {
                if (AnhSlide != null)
                {
                    var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(AnhSlide.FileName);
                    var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images/"), tenhinh);

                    slide.AnhSlide = tenhinh;
                    AnhSlide.SaveAs(ddhinh);
                }
                slide.MaLoai = 1007;
                db.Slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: /QLSlide/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSlides, "MaLoaiSlide", "TenLoaiSlide", slide.MaLoai);
            return View(slide);
        }

        // POST: /QLSlide/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSlide,MaLoai,TenSlide,LinkSide,MoTaSlide,Show")] Slide slide, string H)
        {
            if (ModelState.IsValid)
            {
                ViewBag.MaLoai = new SelectList(db.LoaiSlides, "MaLoaiSlide", "TenLoaiSlide", slide.MaLoai);
                Slide edit = db.Slides.Find(slide.MaSlide);
                edit.MaLoai = slide.MaLoai;
                edit.TenSlide = slide.TenSlide;
                edit.LinkSide = slide.LinkSide;
                edit.MoTaSlide = slide.MoTaSlide;
                edit.Show = slide.Show;

                if (H != "")
                {
                    byte[] imageBytes = Convert.FromBase64String(H);
                    // Convert byte[] to Image
                    Image image;
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        if (edit != null)
                        {
                            if (edit.AnhSlide != null)
                            {
                                //Xóa ảnh chính
                                string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images/"), edit.AnhSlide);
                                if (System.IO.File.Exists(xoa))
                                {
                                    System.IO.File.Delete(xoa);
                                }

                                //Xóa ảnh gốc
                                var tenAnhGoc = edit.AnhSlide.Split('.')[0];
                                string xoaJpg = Path.Combine(Server.MapPath("~/img/ckeditor/Images/" + tenAnhGoc + ".jpg"));
                                if (System.IO.File.Exists(xoaJpg))
                                {
                                    System.IO.File.Delete(xoaJpg);
                                }
                            }

                            image = Image.FromStream(ms);

                            var myfilename = string.Format(@"{0}", Guid.NewGuid());


                            using (Bitmap bm2 = new Bitmap(image, new Size(1600, 600)))
                            {
                                bm2.SetResolution(72, 72);
                                bm2.Save(Server.MapPath("~/img/ckeditor/Images/" + myfilename + ".jpg"));
                            }

                            // First load the image somehow
                            Image myImage = Image.FromFile(Server.MapPath("~/img/ckeditor/Images/" + myfilename + ".jpg"));
                            // Save the image with a quality of 90% 
                            SaveJpeg(Server.MapPath("~/img/ckeditor/Images/" + myfilename + ".png"), myImage, 100);

                            edit.AnhSlide = myfilename + ".png";
                        }
                    }
                }


                //if (AnhSlide != null)
                //{
                //    Stream s = AnhSlide.InputStream;
                //    Image i = System.Drawing.Image.FromStream(s);

                //    int width = i.Width;
                //    int height = i.Height;
                //    float res = i.HorizontalResolution;

                //    if (width != 1170 || height != 300 || res != 72)
                //    {
                //        ViewBag.error = "<div class='alert alert-danger'>Chọn ảnh có kích thước 1170x300(pixel) và độ phân giải 72(dpi)!</div>";
                //        return View(slide);
                //    }
                //    else
                //    {
                //        if (slide.AnhSlide != null)
                //        {
                //            string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), slide.AnhSlide);
                //            if (System.IO.File.Exists(xoa))
                //            {
                //                System.IO.File.Delete(xoa);
                //            }
                //        }
                //        var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(AnhSlide.FileName);
                //        var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);

                //        edit.AnhSlide = tenhinh;
                //        AnhSlide.SaveAs(ddhinh);
                //    }
                //}
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: /QLSlide/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: /QLSlide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);
            if (slide.AnhSlide != null)
            {
                //Xóa ảnh chính
                string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images/"), slide.AnhSlide);

                //Xóa ảnh gốc
                var tenAnhGoc = slide.AnhSlide.Split('.')[0];
                string xoaJpg = Path.Combine(Server.MapPath("~/img/ckeditor/Images/" + tenAnhGoc + ".jpg"));

                if (System.IO.File.Exists(xoa) || System.IO.File.Exists(xoaJpg))
                {
                    System.IO.File.Delete(xoa);

                    //Đợi cho ảnh chính xóa xong
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();

                    //Xóa ảnh gốc
                    System.IO.File.Delete(xoaJpg);
                }
            }
            db.Slides.Remove(slide);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
