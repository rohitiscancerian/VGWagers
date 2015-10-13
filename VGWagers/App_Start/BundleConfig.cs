using System.Web;
using System.Web.Optimization;

namespace VGWagers
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) 
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.11.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                      "~/Scripts/moment*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                    "~/Scripts/jquery.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/zocial.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/footer.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/loginmodal").Include(
                   "~/Scripts/App/login.modal.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                    "~/Scripts/App/login.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/carousel").Include(
                  "~/Scripts/jquery.event.move.js",
                   "~/Scripts/jquery.event.swipe.js",
                    "~/Scripts/jquery.movingboxes.js",
                     "~/Scripts/App/carousel.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/register").Include(
                   "~/Scripts/App/register.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/externalregister").Include(
                  "~/Scripts/App/externalregister.js"
              ));
        }
    }
}
