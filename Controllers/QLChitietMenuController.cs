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

    public class QLChitietMenuController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        //// GET: QLChitietMenu
        ////public async Task<ActionResult> Index()
        ////{
        ////    var chitietMenus = db.ChitietMenus.Include(c => c.LinkTuDo).Include(c => c.LoaiTin).Include(c => c.Page).Include(c => c.Menu);
        ////    return View(await chitietMenus.ToListAsync());
        ////}

        //// GET: QLChitietMenu/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ChitietMenu chitietMenu = await db.ChitietMenus.FindAsync(id);
        //    if (chitietMenu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chitietMenu);
        //}

        //// GET: QLChitietMenu/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IdLink = new SelectList(db.LinkTuDoes, "ID", "TenLink");
        //    ViewBag.IdLoaiTin = new SelectList(db.LoaiTins, "MaLT", "TenLT");
        //    ViewBag.IdPage = new SelectList(db.Pages, "MaT", "TenT");
        //    ViewBag.IdMenu = new SelectList(db.Menus, "ID", "TenMenu");

        //    var IDCHA = db.ChitietMenus.Select(s => new { ID = s.ID, TenMenu = s.Menu.TenMenu + " ,  " + ((s.LoaiTin.TenLT ?? s.Page.TenT) ?? (s.LinkTuDo.TenLink ?? "")) }).ToList();
        //    ViewBag.IDcha = new SelectList(IDCHA, "ID", "TenMenu");
        //    return View();
        //}

        //// POST: QLChitietMenu/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "ID,IdPage,IdLoaiTin,IdLink,STT,IdCha,CapMenu,IdMenu")] ChitietMenu chitietMenu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ChitietMenus.Add(chitietMenu);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IdLink = new SelectList(db.LinkTuDoes, "ID", "TenLink", chitietMenu.IdLink);
        //    ViewBag.IdLoaiTin = new SelectList(db.LoaiTins, "MaLT", "TenLT", chitietMenu.IdLoaiTin);
        //    ViewBag.IdPage = new SelectList(db.Pages, "MaT", "TenT", chitietMenu.IdPage);
        //    ViewBag.IdMenu = new SelectList(db.Menus, "ID", "TenMenu", chitietMenu.IdMenu);
        //    var IDCHA = db.ChitietMenus.Select(s => new { ID = s.ID, TenMenu = s.Menu.TenMenu + ",  " + ((s.LoaiTin.TenLT ?? s.Page.TenT) ?? (s.LinkTuDo.TenLink ?? "")) }).ToList();
        //    ViewBag.IDcha = new SelectList(IDCHA, "ID", "TenMenu");
        //    return View(chitietMenu);
        //}

        //// GET: QLChitietMenu/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ChitietMenu chitietMenu = await db.ChitietMenus.FindAsync(id);
        //    if (chitietMenu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IdLink = new SelectList(db.LinkTuDoes, "ID", "TenLink", chitietMenu.IdLink);
        //    ViewBag.IdLoaiTin = new SelectList(db.LoaiTins, "MaLT", "TenLT", chitietMenu.IdLoaiTin);
        //    ViewBag.IdPage = new SelectList(db.Pages, "MaT", "TenT", chitietMenu.IdPage);
        //    ViewBag.IdMenu = new SelectList(db.Menus, "ID", "TenMenu", chitietMenu.IdMenu);
        //    var IDCHA = db.ChitietMenus.Select(s => new { ID = s.ID, TenMenu = s.Menu.TenMenu + ",  " + ((s.LoaiTin.TenLT ?? s.Page.TenT) ?? (s.LinkTuDo.TenLink ?? "")) }).ToList();
        //    ViewBag.IDcha = new SelectList(IDCHA, "ID", "TenMenu");
        //    return View(chitietMenu);
        //}

        //// POST: QLChitietMenu/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ID,IdPage,IdLoaiTin,IdLink,STT,IdCha,CapMenu,IdMenu")] ChitietMenu chitietMenu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(chitietMenu).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdLink = new SelectList(db.LinkTuDoes, "ID", "TenLink", chitietMenu.IdLink);
        //    ViewBag.IdLoaiTin = new SelectList(db.LoaiTins, "MaLT", "TenLT", chitietMenu.IdLoaiTin);
        //    ViewBag.IdPage = new SelectList(db.Pages, "MaT", "TenT", chitietMenu.IdPage);
        //    ViewBag.IdMenu = new SelectList(db.Menus, "ID", "TenMenu", chitietMenu.IdMenu);
        //    var IDCHA = db.ChitietMenus.Select(s => new { ID = s.ID, TenMenu = s.Menu.TenMenu + "  ,  " + ((s.LoaiTin.TenLT ?? s.Page.TenT) ?? (s.LinkTuDo.TenLink ?? "")) }).ToList();
        //    ViewBag.IDcha = new SelectList(IDCHA, "ID", "TenMenu");
        //    return View(chitietMenu);
        //}

        //// GET: QLChitietMenu/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ChitietMenu chitietMenu = await db.ChitietMenus.FindAsync(id);
        //    if (chitietMenu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chitietMenu);
        //}

        //// POST: QLChitietMenu/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    ChitietMenu chitietMenu = await db.ChitietMenus.FindAsync(id);
        //    db.ChitietMenus.Remove(chitietMenu);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
