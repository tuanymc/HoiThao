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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace KhoaXayDung.Controllers
{
    public class QLTinController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLTin
        public ActionResult Index(int? id)
        {
            var loaitins = db.LoaiTins.ToList();
            if (id == null)
            {
                ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT");
                ViewBag.LoaiTin = 0;
            }
            else
            {
                ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT", id);
                ViewBag.LoaiTin = id;
            }
            return View();
        }

        public PartialViewResult PartialLoadTin(int? id)
        {
            var tins = db.Tins.Include(t => t.LoaiTin).Include(t => t.NguoiDung).OrderByDescending(x => x.MaT).Where(x => x.MaLT == id && x.Show == true);
            return PartialView(tins.ToList());
        }
        // GET: QLTin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tin tin = db.Tins.Find(id);
            if (tin == null || tin.Show == false)
            {
                return HttpNotFound();
            }
            return View(tin);
        }

        // GET: QLTin/Create
        public ActionResult Create(int? id)
        {
            //var loaitins = db.LoaiTins.ToList();
            if (id == null)
            {
                ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT");
            }
            else
            {
                ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT", id);
            }

            return View();
        }


        // POST: QLTin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tin tin, string H, HttpPostedFileBase F)
        {
            if (ModelState.IsValid)
            {
                NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
                tin.MaND = ng.MaND;
                tin.Show = true;
                tin.Xem = 1;
                tin.NgayDang = DateTime.Now;

                //khong dau
                tin.Link = Cus.RewriteUrl(tin.TenT);
                var test = db.Tins.Where(b => b.Link.Contains(tin.Link));
                if (test != null)
                {
                    int count = test.ToList().Count;
                    string khongdau = "";
                    Tin bd = new Tin();
                    for (int i = 0; i <= count; i++)
                    {
                        khongdau = tin.Link;
                        if (i > 0)
                        {
                            khongdau = khongdau + "-" + i.ToString();
                        }
                        bd = db.Tins.Where(b => b.Link.Equals(khongdau)).FirstOrDefault();
                        if (bd == null)
                        {
                            tin.Link = khongdau;
                            break;
                        }
                    }
                }

                if (H != "")
                {
                    byte[] imageBytes = Convert.FromBase64String(H);
                    // Convert byte[] to Image
                    Image image;
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        image = Image.FromStream(ms);

                        var myfilename = string.Format(@"{0}", Guid.NewGuid());

                        using (Bitmap bm2 = new Bitmap(image, new Size(263, 124)))
                        {
                            bm2.SetResolution(72, 72);
                            bm2.Save(Server.MapPath("~/img/ckeditor/Images/" + myfilename + ".jpg"));
                        }

                        // First load the image somehow
                        Image myImage = Image.FromFile(Server.MapPath("~/img/ckeditor/Images/" + myfilename + ".jpg"));
                        // Save the image with a quality of 60% 
                        SaveJpeg(Server.MapPath("~/img/ckeditor/Images/thumb/" + myfilename + ".jpg"), myImage, 60);

                        tin.H = myfilename + ".jpg";
                    }
                }

                if (F != null)
                {
                    if (F.FileName.Split('.')[1].ToLower() == "pdf")
                    {
                        var tenfile = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(F.FileName);
                        var ddfile = Path.Combine(Server.MapPath("~/img/ckeditor/Files"), tenfile);

                        tin.F = tenfile;
                        F.SaveAs(ddfile);
                    }
                    else
                    {
                        ViewBag.ThongBaoFile = "File không hợp lệ";
                        ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT", tin.MaLT);
                        return View(tin);
                    }

                }

                db.Tins.Add(tin);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = tin.MaLT });
            }

            ViewBag.MaLT = new SelectList(db.LoaiTins, "MaLT", "TenLT", tin.MaLT);
            return View(tin);
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


        // GET: QLTin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tin tin = db.Tins.Find(id);
            if (tin == null || tin.Show == false)
            {
                return HttpNotFound();
            }
            var loaitins = db.LoaiTins.Where(w => w.MaLT != 40).ToList();
            ViewBag.MaLT = new SelectList(loaitins, "MaLT", "TenLT", tin.MaLT);
            return View(tin);
        }

        // POST: QLTin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tin tin, string H, HttpPostedFileBase F, bool Xoafile, string EditIMG)
        {
            if (ModelState.IsValid)
            {
                NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
                Tin edit = db.Tins.Find(tin.MaT);
                string tieude = edit.TenT;
                if (!tieude.Equals(tin.TenT))//Có thay đổi tiêu đề
                {
                    edit.TenT = tin.TenT;
                    edit.Link = Cus.RewriteUrl(edit.TenT);
                    var test = db.Tins.Where(b => b.Link.Contains(edit.Link));
                    if (test != null)
                    {
                        int count = test.ToList().Count;
                        string khongdau = "";
                        Tin check = new Tin();
                        for (int i = 0; i <= count; i++)
                        {
                            khongdau = edit.Link;
                            if (i > 0)
                            {
                                khongdau = khongdau + "-" + i.ToString();
                            }
                            check = db.Tins.Where(b => b.Link.Equals(khongdau)).FirstOrDefault();
                            if (check == null)
                            {
                                edit.Link = khongdau;
                                break;
                            }
                        }
                    }
                }
                edit.MaND = ng.MaND;
                edit.MT = tin.MT;
                edit.ND = tin.ND;
                edit.MaT = tin.MaT;
                edit.NgayDang = edit.NgayDang;
                edit.MaLT = tin.MaLT;
                if(EditIMG == "off")
                {
                    if (H != "")
                    {
                        byte[] imageBytes = Convert.FromBase64String(H);
                        // Convert byte[] to Image
                        Image image;
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            if (edit != null)
                            {
                                if (edit.H != null)
                                {
                                    System.GC.Collect();
                                    System.GC.WaitForPendingFinalizers();
                                    string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images/"), edit.H);
                                    if (System.IO.File.Exists(xoa))
                                    {
                                        System.IO.File.Delete(xoa);
                                    }
                                    //Đợi cho ảnh chính xóa xong
                                    System.GC.Collect();
                                    System.GC.WaitForPendingFinalizers();
                                    string xoaThumb = Path.Combine(Server.MapPath("~/img/ckeditor/Images/thumb/"), edit.H);
                                    if (System.IO.File.Exists(xoaThumb))
                                    {
                                        System.IO.File.Delete(xoaThumb);
                                    }
                                }

                                image = Image.FromStream(ms);

                                var myfilename = string.Format(@"{0}", Guid.NewGuid());


                                using (Bitmap bm2 = new Bitmap(image, new Size(263, 124)))
                                {
                                    bm2.SetResolution(72, 72);
                                    bm2.Save(Server.MapPath("~/img/ckeditor/Images/" + myfilename + ".jpg"));
                                }

                                // First load the image somehow
                                Image myImage = Image.FromFile(Server.MapPath("~/img/ckeditor/Images/" + myfilename + ".jpg"));
                                // Save the image with a quality of 60% 
                                SaveJpeg(Server.MapPath("~/img/ckeditor/Images/thumb/" + myfilename + ".jpg"), myImage, 60);

                                edit.H = myfilename + ".jpg";
                            }
                        }
                    }
                }

                if (F != null)
                {
                    if (F.FileName.Split('.')[1].ToLower() == "pdf")
                    {
                        if (edit.F != null)
                        {
                            string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Files"), edit.F);
                            if (System.IO.File.Exists(xoa))
                            {

                                System.IO.File.Delete(xoa);
                            }
                        }
                        var tenfile = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(F.FileName);
                        var ddfile = Path.Combine(Server.MapPath("~/img/ckeditor/Files"), tenfile);

                        edit.F = tenfile;
                        F.SaveAs(ddfile);
                    }
                    else
                    {
                        ViewBag.ThongBaoFile = "File không hợp lệ";
                        var loaitins1 = db.LoaiTins.Where(w => w.MaLT != 40).ToList();
                        ViewBag.MaLT = new SelectList(loaitins1, "MaLT", "TenLT", tin.MaLT);
                        return View(tin);
                    }
                }
                else
                {
                    edit.F = edit.F;
                }


                if (Xoafile == true)
                {
                    if (edit.F != null)
                    {
                        string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Files"), edit.F);
                        if (System.IO.File.Exists(xoa))
                        {
                            System.IO.File.Create(xoa).Dispose();
                            System.IO.File.Delete(xoa);

                        }
                        edit.F = null;
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tin.MaLT });
            }
            var loaitins = db.LoaiTins.Where(w => w.MaLT != 40).ToList();
            ViewBag.MaLT = new SelectList(loaitins, "MaLT", "TenLT", tin.MaLT);
            return View(tin);
        }

        // GET: QLTin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tin tin = db.Tins.Find(id);
            if (tin == null || tin.Show == false)
            {
                return HttpNotFound();
            }
            return View(tin);
        }

        // POST: QLTin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tin tin = db.Tins.Find(id);
            //if (tin.H != null)
            //{
            //    string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tin.H);
            //    string xoaThumb = Path.Combine(Server.MapPath("~/img/ckeditor/Images/thumb"), tin.H);

            //    if (System.IO.File.Exists(xoa) || System.IO.File.Exists(xoaThumb))
            //    {
            //        System.IO.File.Delete(xoa);

            //        //Đợi cho ảnh chính xóa xong
            //        System.GC.Collect();
            //        System.GC.WaitForPendingFinalizers();

            //        //Xóa ảnh thumb
            //        System.IO.File.Delete(xoaThumb);
            //    }
            //}
            //if (tin.F != null)
            //{
            //    string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Files"), tin.F);
            //    if (System.IO.File.Exists(xoa))
            //    {
            //        System.IO.File.Delete(xoa);
            //    }
            //}
            //db.Tins.Remove(tin);

            tin.Show = false;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = tin.MaLT });
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
