using System.Web;
using System.Web.Optimization;

namespace idn.Skycic.Inventory.Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region["Script"]
            // Sử dụng thư viện jQuery v3.4.1
            bundles.Add(new ScriptBundle("~/bundles/jquery.min").Include(
                "~/Content/lte/plugins/jquery/jquery.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/Content/lte/plugins/jquery-ui/jquery-ui.min.js"
                ));
            // sử dụng thư viện Ckfinder
            bundles.Add(new ScriptBundle("~/bundles/Ckfinder").Include(
                "~/Content/plugin/ckeditor/ckeditor.js",
                "~/Content/plugin/ckfinder/ckfinder.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-library-lte-ie8").Include(
                "~/Scripts/support-ie/jquery-1.12.4.min.js"));
            // Show Error using jquery-ui.min.css, ace.min.css, jquery-ui.min.js, jquery.validate.min.js, jquery.form-2.68.js, ajaxform.js
            bundles.Add(new ScriptBundle("~/bundles/jquery-validate").Include(
                "~/Content/lte/plugins/jquery-validation/jquery.validate.min.js", // 1
                "~/Content/lte/plugins/jquery-validation/additional-methods.min.js",
                "~/Scripts/jquery-validation/jquery.validate.unobtrusive.min.js",
                "~/Scripts/js/jquery.form-2.68.js", // 2
                "~/Scripts/js/jquery.table.hpaging.js", // 3
                "~/Scripts/js/ajaxform.js" // 4
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/support-ie").Include(
                "~/Scripts/support-ie/html5shiv.min.js",
                "~/Scripts/support-ie/modernizr.js",
                "~/Scripts/support-ie/respond.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/popper.min.js").Include(
                "~/Content/lte/plugins/bootstrap/js/popper.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/lte/plugins/bootstrap/js/bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap.bundle.min").Include(
                "~/Content/lte/plugins/bootstrap/js/bootstrap.bundle.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/adminlte-js").Include(
                "~/Content/lte/dist/js/adminlte.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-fix-ie9").Include(
                "~/Scripts/support-ie/bootstrap-ie9.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-fix-ie8").Include(
                "~/Scripts/support-ie/bootstrap-ie8.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-lte-ie8").Include(
                "~/Scripts/bootstrap/bootstrap.js"
            ));
            bundles.Add(new StyleBundle("~/bundles/bootstrap-tagsinput-js").Include(
                "~/Content/lte/plugins/bootstrap/input-tags/bootstrap-tagsinput.js",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new ScriptBundle("~/bundles/toastr-js").Include(
                "~/Content/lte/plugins/toastr/toastr.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-number").Include(
                "~/Scripts/js/jquery.number.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/library-js").Include(
                "~/Scripts/js/CommonUtils.js",
                "~/Scripts/js/CommonExcel.js",
                //"~/Scripts/mine/KeepAlive.js",
                "~/Scripts/mine/Mst_ColumnConfig.js",
                "~/Scripts/js/idntable.js",
                "~/Scripts/js/confirm.js"
                //"~/Scripts/js/table-sortable.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/select2.min.js").Include(
                "~/Content/lte/plugins/select2/js/select2.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/hrscroll").Include(
                "~/Scripts/js/hrscroll.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                "~/Scripts/bootbox.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bxslider").Include(
                "~/Scripts/jquery.bxslider.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker-js").Include(
                "~/Content/datepicker/js/bootstrap-datepicker.min.js"
            ));
            #endregion

            #region["StyleSheet"]
            bundles.Add(new StyleBundle("~/bundles/bootstrap-v4.3.1").Include(
                "~/Content/lte/dist/css/bootstrap/bootstrap.min.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/bootstrap-tagsinput-css").Include(
                "~/Content/lte/plugins/bootstrap/input-tags/bootstrap-tagsinput.css",
                new CssRewriteUrlTransform()
            ));
            // Font Awesome
            bundles.Add(new StyleBundle("~/bundles/font-awesome").Include(
                "~/Content/lte/plugins/fontawesome-free/css/all.min.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui-css").Include(
                "~/Scripts/jquery-ui/jquery-ui.min.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/adminlte-css").Include(
                "~/Content/lte/dist/css/adminlte.min.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/bootstrap-fix-ie9").Include(
                "~/Content/support-ie/bootstrap-ie9.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/bootstrap-fix-ie8").Include(
                "~/Content/support-ie/bootstrap-ie8.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/toastr-css").Include(
                "~/Content/lte/plugins/toastr/toastr.min.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/select2.min.css").Include(
                "~/Content/lte/plugins/select2/css/select2.min.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/StyleSheet").Include(
                "~/Content/ProductCenter.css",
                new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/bootstrap-datepicker-css").Include(
                "~/Content/datepicker/css/bootstrap-datepicker.min.css",
                new CssRewriteUrlTransform()
            ));
            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}
