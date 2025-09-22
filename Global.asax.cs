using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using KhoaXayDung.Models;

namespace KhoaXayDung
{
    public class MvcApplication : System.Web.HttpApplication
    {
        KhoaXayDungEntities db = new KhoaXayDungEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ThongKe tk = db.ThongKes.Find(1);
            if (tk == null)
            {
                tk = new ThongKe();
                tk.Visited = 1;
                db.ThongKes.Add(tk);
                db.SaveChanges();
            }
            Application.Lock();
            Application["visited"] = tk.Visited;
            Application.UnLock();
        }

        protected void Session_Start()
        {
            if (Application["online"] == null)
            {
                Application.Lock();
                Application["online"] = 1;
                Application.UnLock();
            }
            else
            {
                Application.Lock();
                Application["online"] = (int)Application["online"] + 1;
                Application.UnLock();
            }
            Application.Lock();
            Application["visited"] = (int)Application["visited"] + 1;
            Application.UnLock();
            ThongKe tk = db.ThongKes.Find(1);
            tk.Visited = (int)Application["visited"];
            db.SaveChanges();
        }
        protected void Session_End()
        {
            Application.Lock();
            Application["online"] = (int)Application["online"] - 1;
            Application.UnLock();
        }
    }
}
