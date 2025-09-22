using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhoaXayDung.Models;
using System.IO;
namespace KhoaXayDung.Controllers
{
    public class QLPagesController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLPages
        public async Task<ActionResult> Index()
        {
            var pages = db.Pages.Include(p => p.NguoiDung);
            return View(await pages.ToListAsync());
        }

        // GET: QLPages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = await db.Pages.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: QLPages/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: QLPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaT,TenT,MT,ND,H,F,NgayDang,Xem,Link,Show,MaND")] Page page, HttpPostedFileBase H, HttpPostedFileBase F)
        {
            if (ModelState.IsValid)
            {
                NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
                page.MaND = ng.MaND;
                page.Show = true;
                page.Xem = 1;
                page.NgayDang = DateTime.Now;
                //khong dau
                page.Link = Cus.RewriteUrl(page.TenT);
                var test = db.Pages.Where(b => b.Link.Contains(page.Link));
                if (test != null)
                {
                    int count = test.ToList().Count;
                    string khongdau = "";
                    Page bd = new Page();
                    for (int i = 0; i <= count; i++)
                    {
                        khongdau = page.Link;
                        if (i > 0)
                        {
                            khongdau = khongdau + "-" + i.ToString();
                        }
                        bd = db.Pages.Where(b => b.Link.Equals(khongdau)).FirstOrDefault();
                        if (bd == null)
                        {
                            page.Link = khongdau;
                            break;
                        }
                    }
                }
                if (H != null)
                {
                    var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(H.FileName);
                    var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);

                    page.H = tenhinh;
                    H.SaveAs(ddhinh);
                }

                if (F != null)
                {
                    var tenfile = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(F.FileName);
                    var ddfile = Path.Combine(Server.MapPath("~/img/ckeditor/Files"), tenfile);

                    page.F = tenfile;
                    F.SaveAs(ddfile);
                }
                db.Pages.Add(page);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(page);
        }

        // GET: QLPages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = await db.Pages.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }

            return View(page);
        }

        // POST: QLPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaT,TenT,MT,ND,H,F,NgayDang,Xem,Link,Show,MaND")] Page page, HttpPostedFileBase H, HttpPostedFileBase F, bool Xoafile)
        {
            if (ModelState.IsValid)
            {
                NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
                Page edit = db.Pages.Find(page.MaT);
                string tieude = edit.TenT;
                if (!tieude.Equals(page.TenT))//Có thay đổi tiêu đề
                {
                    edit.TenT = page.TenT;
                    edit.Link = Cus.RewriteUrl(edit.TenT);
                    var test = db.Pages.Where(b => b.Link.Contains(edit.Link));
                    if (test != null)
                    {
                        int count = test.ToList().Count;
                        string khongdau = "";
                        Page check = new Page();
                        for (int i = 0; i <= count; i++)
                        {
                            khongdau = edit.Link;
                            if (i > 0)
                            {
                                khongdau = khongdau + "-" + i.ToString();
                            }
                            check = db.Pages.Where(b => b.Link.Equals(khongdau)).FirstOrDefault();
                            if (check == null)
                            {
                                edit.Link = khongdau;
                                break;
                            }
                        }
                    }
                }
                edit.MaND = ng.MaND;
                edit.MT = page.MT;
                edit.ND = page.ND;
                edit.MaT = page.MaT;
                if (H != null)
                {
                    if (edit.H != null)
                    {
                        string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), edit.H);
                        if (System.IO.File.Exists(xoa))
                        {
                            System.IO.File.Delete(xoa);
                        }
                    }
                    var tenhinh = Cus.ChuyenKoDau(DateTime.Now.ToString()) + Path.GetFileName(H.FileName);
                    var ddhinh = Path.Combine(Server.MapPath("~/img/ckeditor/Images"), tenhinh);

                    edit.H = tenhinh;
                    H.SaveAs(ddhinh);
                }

                if (F != null)
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


                if (Xoafile == true)
                {
                    if (edit.F != null)
                    {
                        string xoa = Path.Combine(Server.MapPath("~/img/ckeditor/Files"), edit.F);
                        if (System.IO.File.Exists(xoa))
                        {
                            System.IO.File.Delete(xoa);

                        }
                        edit.F = null;
                    }

                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET: QLPages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = await db.Pages.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: QLPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Page page = await db.Pages.FindAsync(id);
            db.Pages.Remove(page);
            await db.SaveChangesAsync();
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
