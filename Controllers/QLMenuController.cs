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

namespace KhoaXayDung.Controllers
{
    public class QLMenuController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLMenu
        public async Task<ActionResult> Index()
        {
            return View(await db.Menus.ToListAsync());
        }

        // GET: QLMenu/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: QLMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TenMenu")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: QLMenu/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: QLMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int idmenu, string tenmenu)
        {

            Menu menu = db.Menus.Find(idmenu);
            menu.TenMenu = tenmenu;
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            return View(menu);
        }
        ///////////////////////////////////////////////////////////////

        [HttpPost]
        public ActionResult CapNhatLink(int menu, int idlink, string tenlink, string link)
        {
            LinkTuDo _link = db.LinkTuDoes.Find(idlink);
            _link.TenLink = tenlink;
            _link.Link = link;
            _link.ID = idlink;
            db.SaveChanges();
            return Redirect("/QLMenu/Edit/" + menu);
        }
        /// <summary>
        /// ////////////////////////

        [HttpPost]
        public ActionResult XoaLink(int menu, int idlink)
        {
            LinkTuDo link = db.LinkTuDoes.Find(idlink);
            db.LinkTuDoes.Remove(link);
            db.SaveChanges();
            return Redirect("/QLMenu/Edit/" + menu);
        }
        /// <summary>
        /// ////////////////

        [HttpPost]
        public ActionResult XoaPage(int menu, int idpage)
        {
            Page page = db.Pages.Find(idpage);
            db.Pages.Remove(page);
            db.SaveChanges();
            return Redirect("/QLMenu/Edit/" + menu);
        }
        [HttpPost]
        public ActionResult CapNhatPage(int menu, int idpage, string tentin)
        {
            NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
            Page edit = db.Pages.Find(idpage);
            string tieude = edit.TenT;
            if (!tieude.Equals(tentin))//Có thay đổi tiêu đề
            {
                edit.TenT = tentin;
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
            edit.MaT = idpage;
            db.SaveChanges();

            return Redirect("/QLMenu/Edit/" + menu);
        }
        [HttpPost]
        public ActionResult XoaChiTietMenu(int? id, int menu)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.ChitietMenus.RemoveRange(db.ChitietMenus.Where(w => w.IdCha == id));
            db.SaveChanges();
            ChitietMenu chitietMenu = db.ChitietMenus.Find(id);
            if (chitietMenu != null)
            {
                db.ChitietMenus.Remove(chitietMenu);
                db.SaveChanges();
            }

            return Redirect("/QLMenu/Edit/" + menu);
        }
        [HttpPost]
        public ActionResult ThemMenuLoaiTins(int idmenu, int loaitin, int stt, int? idcha)
        {
            ThemMenu(idmenu, 2, loaitin, stt, idcha);
            return Redirect("/QLMenu/Edit/" + idmenu);
        }
        [HttpPost]
        public ActionResult ThemLink(int idmenu, string ten, string link, int stt, int? idcha)
        {
            LinkTuDo _link = new LinkTuDo();
            _link.TenLink = ten;
            _link.Link = link;
            db.LinkTuDoes.Add(_link);
            db.SaveChanges();

            ThemMenu(idmenu, 3, _link.ID, stt, idcha);
            return Redirect("/QLMenu/Edit/" + idmenu);
        }
        [HttpPost]
        public ActionResult ThemMenuLinks(int idmenu, int malink, int stt, int? idcha)
        {
            ThemMenu(idmenu, 3, malink, stt, idcha);
            return Redirect("/QLMenu/Edit/" + idmenu);
        }
        [HttpPost]
        public ActionResult ThemPages(int idmenu, string ten, int stt, int? idcha)
        {
            NguoiDung ng = Session["TaiKhoan"] as NguoiDung;
            Page _page = new Page();
            _page.MaND = ng.MaND;
            _page.Show = true;
            _page.Xem = 1;
            _page.NgayDang = DateTime.Now;
            _page.TenT = ten;
            _page.MT = ten;
            _page.Link = Cus.RewriteUrl(_page.TenT);
            var test = db.Pages.Where(b => b.Link.Contains(_page.Link));
            if (test != null)
            {
                int count = test.ToList().Count;
                string khongdau = "";
                Page bd = new Page();
                for (int i = 0; i <= count; i++)
                {
                    khongdau = _page.Link;
                    if (i > 0)
                    {
                        khongdau = khongdau + "-" + i.ToString();
                    }
                    bd = db.Pages.Where(b => b.Link.Equals(khongdau)).FirstOrDefault();
                    if (bd == null)
                    {
                        _page.Link = khongdau;
                        break;
                    }
                }
            }
            db.Pages.Add(_page);
            db.SaveChanges();
            ThemMenu(idmenu, 1, _page.MaT, stt, idcha);
            return Redirect("/QLMenu/Edit/" + idmenu);
        }
        [HttpPost]
        public ActionResult ThemMenuPages(int idmenu, int matin, int stt, int? idcha)
        {
            ThemMenu(idmenu, 1, matin, stt, idcha);
            return Redirect("/QLMenu/Edit/" + idmenu);
        }
        public void ThemMenu(int idmenu, int chon, int loai, int stt, int? idcha)
        {
            ChitietMenu chitiet = new ChitietMenu();
            chitiet.IdMenu = idmenu;
            if (chon == 1)
            {
                chitiet.IdPage = loai;
            }
            if (chon == 2)
            {
                chitiet.IdLoaiTin = loai;
            }
            if (chon == 3)
            {
                chitiet.IdLink = loai;
            }
            chitiet.STT = stt;
            chitiet.IdCha = idcha;
            db.ChitietMenus.Add(chitiet);
            db.SaveChanges();
        }
        public PartialViewResult LoadMenu(int id)
        {
            var Menu = db.ChitietMenus.Where(w => w.IdMenu == id && w.IdCha == null).OrderBy(o => o.STT).ToList();
            return PartialView(Menu);
        }
        public PartialViewResult LoadDropDown(int? id)
        {
            ViewBag.IdLink = db.LinkTuDoes.ToList();
            ViewBag.IdLoaiTin = new SelectList(db.LoaiTins, "MaLT", "TenLT");
            ViewBag.IdPage = new SelectList(db.Pages, "MaT", "TenT");

            var IDCHA = db.ChitietMenus.Where(w => w.IdMenu == id).Select(s => new { ID = s.ID, TenMenu = ((s.LoaiTin.TenLT ?? s.Page.TenT) ?? (s.LinkTuDo.TenLink ?? "")) }).ToList();
            ViewBag.IDcha = new SelectList(IDCHA, "ID", "TenMenu");
            Menu menu = db.Menus.Find(id);
            return PartialView(menu);
        }
        // GET: QLMenu/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: QLMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            db.ChitietMenus.RemoveRange(db.ChitietMenus.Where(w => w.IdMenu == id));
            db.SaveChanges();
            Menu menu = await db.Menus.FindAsync(id);
            db.Menus.Remove(menu);
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
