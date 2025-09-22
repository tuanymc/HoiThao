using System.Web;
using System.Web.Optimization;

namespace KhoaXayDung
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jsQL").Include(
                     "~/Scripts/jquery-1.10.2.min.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/metisMenu.min.js",
                     "~/Scripts/sb-admin-2.js",
                     "~/Scripts/jquery.dataTables.min.js",
                     "~/Scripts/dataTables.bootstrap.min.js",
                     "~/Scripts/cropper.min.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/jsWeb").Include(
                     "~/Scripts/jquery.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/jquery.marquee.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/cssWeb").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css",
                      "~/Content/font-awesome.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/cssWeb2018").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/swipebox.css",
                      "~/Content/website.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/website_2017/cssWeb2018").Include(
                      "~/Content/website_2017/style.css",
                      "~/Content/website_2017/bootstrap.min.css",
                      "~/Content/website_2017/jcarousel.css",
                      "~/Content/website_2017/main.css",
                      "~/Content/website_2017/menu.css",
                      "~/Content/website_2017/owl.carousel.min.css",
                      "~/Content/website_2017/owl.theme.min.css",
                      "~/Content/website_2017/slider.css",
                      "~/Content/website_2017/slider2.css",
                      "~/Content/website_2017/style-responsive.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsWeb2018").Include(
                     "~/Scripts/jquery.js",
                     "~/Scripts/website_2017/bootstrap.min.js",
                     "~/Scripts/jquery.swipebox.js",
                     "~/Scripts/website_2017/jQuery.succinct.min.js",
                     "~/Scripts/website_2017/jcarousel.js",
                     "~/Scripts/website_2017/jcarousellite_1.0.1.js",
                     "~/Scripts/website_2017/responsiveCarousel.min.js",
                     "~/Scripts/website_2017/jquery.carouFredSel.js",
                     "~/Scripts/website_2017/js_head.js",
                     "~/Scripts/website_2017/photoslide.js",
                     "~/Scripts/website_2017/script.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/cssQL").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/metisMenu.min.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/dataTables.bootstrap.css",
                      "~/Content/dataTables.responsive.css",
                      "~/Content/cropper.min.css"
                      ));


            //Home2019
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/home2019/jquery-3.4.1.slim.min.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/home2019/umd/popper.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/home2019/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/home2019/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/home2019/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/home2019/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/owl.theme.default.min.css",
                      "~/Content/home2019/site.min.css"));
        }
    }
}
