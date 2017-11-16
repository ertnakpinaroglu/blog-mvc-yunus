using System.Web;
using System.Web.Optimization;

namespace blog_mvc_yunus
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // anasayfa için css dosyaları 
            bundles.Add(new StyleBundle("~/homepage/css").Include(

                "~/Content/bootstrap.min.css",
                "~/Content/Site.min.css"
                ));

            // anasayfa için script dosyaları 
            bundles.Add(new ScriptBundle("~/homepage/js").Include(
                "~/Scripts/jquery-3.2.1.min.js",
                "~/Scripts/bootstrap.min.js"


                ));

            // admin paneli için gerekli css dosyaları
            bundles.Add(new StyleBundle("~/admin/css").Include(
               "~/Content/Admin/CSS/bootstrap.min.css",
                "~/Content/Admin/CSS/metisMenu.min.css",
              "~/Content/Admin/CSS/morris.css",
                "~/Content/Admin/CSS/sb-admin-2.css",
                "~/Content/Admin/font-awesome/css/font-awesome.min.css"
                ));

            // admin paneli için gerekli js dosyaları
            bundles.Add(new ScriptBundle("~/admin/js").Include(
            "~/Content/Admin/Scripts/bootstrap.min.js",  
           "~/Content/Admin/Scripts/jquery.min.js",
            "~/Content/Admin/Scripts/metisMenu.min.js", 
             "~/Content/Admin/Scripts/morris-data.js",
             "~/Content/Admin/Scripts/morris.min.js",
            "~/Content/Admin/Scripts/raphael.min.js",
            "~/Content/Admin/Scripts/sb-admin-2.js"
             ));

            // validation
            bundles.Add(new ScriptBundle("~/admin/validate_js").Include(
                "~/Scripts/jquery.validate-vsdoc.js",
               "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.validate.unobtrusive.min.js"
                ));

        }
    }
}
