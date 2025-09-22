using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KhoaXayDung.Models;

namespace KhoaXayDung
{
    public class RouteConfig
    {
        static private KhoaXayDungEntities db = new KhoaXayDungEntities();
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Trang chu
            routes.MapRoute(
                name: "HomeIndex",
                url: "",
                defaults: new { controller = "Home2022", action = "Index" }
            );
            //Lich Cong Tac
            routes.MapRoute(
              name: "CalendarIndex",
              url: "lich-cong-tac",
              defaults: new { controller = "LichCongTac", action = "Index" }
          );

            routes.MapRoute(
                name: "CalendarDetail",
                url: "lich-cong-tac/tuan-{link}",
                defaults: new { controller = "LichCongTac", action = "Detail", link = UrlParameter.Optional }
            );



            //var page = db.Pages.ToList();
            //foreach (var item in page)
            //{
            //    routes.MapRoute(
            //                    name: item.MaT.ToString() + item.Link,
            //                    url: item.Link,
            //                    defaults: new { controller = "Category", action = "Pages" }
            //          );
            //}


            //var loaitin = db.LoaiTins.Where(w => w.khongdau != "lich-cong-tac").ToList();
            //foreach (var item in loaitin)
            //{
            //    routes.MapRoute(
            //                           name: item.MaLT.ToString() + item.khongdau,
            //                           url: item.khongdau,
            //                           defaults: new { controller = "Category", action = "List" }
            //        );
            //}


            //var tin = db.Tins.Where(w => w.LoaiTin.khongdau != "lich-cong-tac").ToList();
            //foreach (var item in tin)
            //{
            //    var link = item.LoaiTin.khongdau + "/" + item.Link;
            //    routes.MapRoute(
            //       name: item.MaT.ToString() + link,
            //       url: link,
            //       defaults: new { controller = "Category", action = "Index" }
            //   );
            //}



            //  Tin tuc

            //routes.MapRoute(
            //    name: "CategoryList",
            //    url: "danh-muc/{khong_dau}",
            //    defaults: new { controller = "Category", action = "List", khong_dau = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "CategoryList",
                url: "danh-muc-tin-tuc/{khong_dau}",
                defaults: new { controller = "Website", action = "ListNews", khong_dau = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CategoryIndex",
                url: "danh-muc/{khong_dau}/{link}",
                defaults: new { controller = "Category", action = "Index", khong_dau = UrlParameter.Optional, link = UrlParameter.Optional }
            );

            //  Thông báo
            routes.MapRoute(
                name: "NotificationLists",
                url: "danh-muc-thong-bao/{khong_dau}",
                defaults: new { controller = "Website", action = "ListNotification", khong_dau = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NotificationIndex",
                url: "danh-muc/{khong_dau}/{link}",
                defaults: new { controller = "Category", action = "Index", khong_dau = UrlParameter.Optional, link = UrlParameter.Optional }
            );

            // pages
            routes.MapRoute(
               name: "BaiViet",
               url: "bai-viet/{link}",
               defaults: new { controller = "Website", action = "Content", link = UrlParameter.Optional }
           );
            // routes.MapRoute(
            //    name: "BaiViet",
            //    url: "bai-viet/{link}",
            //    defaults: new { controller = "Category", action = "Pages", link = UrlParameter.Optional }
            //);

            // Video và hình ảnh hoạt động
            routes.MapRoute(
               name: "Video",
               url: "video-hoat-dong/{ids}",
               defaults: new { controller = "Website", action = "AlbumHoatDong", ids = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Pictures",
               url: "hinh-anh-hoat-dong/{ids}",
               defaults: new { controller = "Website", action = "AlbumHoatDong", ids = UrlParameter.Optional}
           );

            routes.MapRoute(
               name: "VideoHinhAnh",
               url: "video-hinh-anh-hoat-dong",
               defaults: new { controller = "Website", action = "AlbumHoatDong"}
           );


            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home2022", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
