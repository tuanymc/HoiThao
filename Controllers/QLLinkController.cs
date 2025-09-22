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
    public class QLLinkController : Controller
    {
        private KhoaXayDungEntities db = new KhoaXayDungEntities();

        // GET: QLLink
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.LinkTuDoes.ToListAsync());
        //}

        //// GET: QLLink/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LinkTuDo linkTuDo = await db.LinkTuDoes.FindAsync(id);
        //    if (linkTuDo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(linkTuDo);
        //}

        //// GET: QLLink/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: QLLink/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "ID,TenLink,Link")] LinkTuDo linkTuDo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.LinkTuDoes.Add(linkTuDo);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(linkTuDo);
        //}

        //// GET: QLLink/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LinkTuDo linkTuDo = await db.LinkTuDoes.FindAsync(id);
        //    if (linkTuDo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(linkTuDo);
        //}

        //// POST: QLLink/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ID,TenLink,Link")] LinkTuDo linkTuDo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(linkTuDo).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(linkTuDo);
        //}

        //// GET: QLLink/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LinkTuDo linkTuDo = await db.LinkTuDoes.FindAsync(id);
        //    if (linkTuDo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(linkTuDo);
        //}

        //// POST: QLLink/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    LinkTuDo linkTuDo = await db.LinkTuDoes.FindAsync(id);
        //    db.LinkTuDoes.Remove(linkTuDo);
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
