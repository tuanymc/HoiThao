using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KhoaXayDung.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Images()
        {
            return View();
        }

        public PartialViewResult _Images()
        {
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/img/ckeditor/Images"))
                              .Select(fn => "~/img/ckeditor/Images/" + Path.GetFileName(fn));
            return PartialView();
        }
    }
}