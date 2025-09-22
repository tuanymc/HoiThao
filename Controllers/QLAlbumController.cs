using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;

namespace KhoaXayDung.Controllers
{
    public class QLAlbumController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLAlbum
        public ActionResult Index()
        {
            var albumVideoHinhAnhs = db.AlbumVideoHinhAnhs.Include(a => a.LoaiAlbum);
            return View(albumVideoHinhAnhs.ToList());
        }

        // GET: QLAlbum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumVideoHinhAnh albumVideoHinhAnh = db.AlbumVideoHinhAnhs.Find(id);
            if (albumVideoHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(albumVideoHinhAnh);
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

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        // GET: QLAlbum/Create
        public ActionResult Create()
        {
            ViewBag.IdAlbum = new SelectList(db.LoaiAlbums, "IdAlbum", "TenAlbum");
            return View();
        }

        // POST: QLAlbum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumVideoHinhAnh albumVideoHinhAnh, List<HttpPostedFileBase> Files)
        {
            if (ModelState.IsValid)
            {
                var path = "";
                int result = 0;
                if (Files.Count > 0)
                {
                    foreach (var item in Files)
                    {
                        if (item.ContentLength > 0)
                        {
                            if (Path.GetExtension(item.FileName).ToLower() == ".jpg" || Path.GetExtension(item.FileName).ToLower() == ".png" || Path.GetExtension(item.FileName).ToLower() == ".jpeg")
                            {
                                var tenhinh = "";
                                tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(item.FileName);
                                path = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);
                                item.SaveAs(path);

                                // First load the image somehow
                                Image myImage = Image.FromFile(Server.MapPath("~/img/ckeditor/Images/" + tenhinh));                    
                                // Save the image with a quality of 80% 
                                SaveJpeg(Server.MapPath("~/img/ckeditor/Images/thumb/" + tenhinh), resizeImage(myImage, new Size(120, 68)), 80);

                                albumVideoHinhAnh.Files = tenhinh;
                                albumVideoHinhAnh.NgayDang = DateTime.Now;
                                db.AlbumVideoHinhAnhs.Add(albumVideoHinhAnh);
                                result = db.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    albumVideoHinhAnh.NgayDang = DateTime.Now;
                    db.AlbumVideoHinhAnhs.Add(albumVideoHinhAnh);
                    result = db.SaveChanges();
                }
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.IdAlbum = new SelectList(db.LoaiAlbums, "IdAlbum", "TenAlbum", albumVideoHinhAnh.IdAlbum);
            return View(albumVideoHinhAnh);
        }

        // GET: QLAlbum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumVideoHinhAnh albumVideoHinhAnh = db.AlbumVideoHinhAnhs.Find(id);
            if (albumVideoHinhAnh == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAlbum = new SelectList(db.LoaiAlbums, "IdAlbum", "TenAlbum", albumVideoHinhAnh.IdAlbum);
            return View(albumVideoHinhAnh);
        }

        // POST: QLAlbum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVideoHinhAnh,TenVideoHinhAnh,Mota,Files,NgayDang,Show,IdAlbum")] AlbumVideoHinhAnh albumVideoHinhAnh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumVideoHinhAnh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAlbum = new SelectList(db.LoaiAlbums, "IdAlbum", "TenAlbum", albumVideoHinhAnh.IdAlbum);
            return View(albumVideoHinhAnh);
        }

        // GET: QLAlbum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumVideoHinhAnh albumVideoHinhAnh = db.AlbumVideoHinhAnhs.Find(id);
            if (albumVideoHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(albumVideoHinhAnh);
        }

        // POST: QLAlbum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumVideoHinhAnh albumVideoHinhAnh = db.AlbumVideoHinhAnhs.Find(id);
            if (albumVideoHinhAnh.Files != null)
            {
                //string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), albumVideoHinhAnh.Files);
                //if (System.IO.File.Exists(xoa))
                //{
                //    System.IO.File.Delete(xoa);
                //}

                //Xóa ảnh chính
                string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images/"), albumVideoHinhAnh.Files);

                //Xóa ảnh gốc
                string xoaJpg = Path.Combine(Server.MapPath("~/img/ckeditor/Images/thumb/"), albumVideoHinhAnh.Files);

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
            db.AlbumVideoHinhAnhs.Remove(albumVideoHinhAnh);
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
