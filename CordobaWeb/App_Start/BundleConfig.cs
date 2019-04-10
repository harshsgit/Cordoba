using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace CordobaWeb
{
    class NonOrderingBundleOrderer : IBundleOrderer
    {        

        IEnumerable<BundleFile> IBundleOrderer.OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
    public class BundleConfig
    {
   
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            ////////////////// Common /////////////////////////////       
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/CommonJs").Include(
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/bootstrap-modalmanager.js",
                    "~/Scripts/bootstrap-modal.js",
                    "~/Scripts/bootstrap-datepicker.js",
                    "~/Scripts/jquery.bootstrap-duallistbox.js",
                     "~/Scripts/bootbox.js",
                     "~/Scripts/ckeditor/ckeditor.js"
                     ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/textAngular.css",
                      "~/Content/css/bootstrap-duallistbox.css",
                      "~/Content/css/jquery.dataTables.min.css",
                      "~/Content/css/bootstrap-datepicker.min.css",
                      "~/Content/css/toastr.css",
                      "~/Content/css/site.css",
                      "~/Content/css/rzslider.css",
                      "~/Content/css/nav.css",
                      "~/Content/css/jquery-ui.min.css"));

            var angularBundle = new Bundle("~/bundles/angular");
              angularBundle.Include("~/Scripts/angular.js");
              angularBundle.Include("~/Scripts/angular-animate.js");
              angularBundle.Include("~/Scripts/angular-dragdrop.min.js");
              angularBundle.Include("~/Scripts/angular-ui-router.min.js");
              angularBundle.Include("~/Scripts/angular-local-storage.js");
              angularBundle.Include( "~/Scripts/angular-sanitize.js");
              angularBundle.Include("~/Scripts/angular-datatables.min.js");
              angularBundle.Include("~/Scripts/angular-ui-switch.min.js");
              angularBundle.Include("~/Scripts/angular-star-rating/main.js");
              angularBundle.Orderer = new NonOrderingBundleOrderer();
              bundles.Add(angularBundle);

            var appConfigBundle = new Bundle("~/bundles/appConfig");
            appConfigBundle.Include("~/Scripts/jquery.dataTables.min.js");
            appConfigBundle.Include("~/Scripts/dataTables.tableTools.js");
            appConfigBundle.Include("~/Scripts/jquery.dataTables.rowReordering.js");
            appConfigBundle.Include("~/Scripts/ng-file-upload.min.js");
            appConfigBundle.Include("~/Scripts/ng-file-upload-shim.min.js");
            appConfigBundle.Include("~/Scripts/textAngular-rangy.min.js");
            appConfigBundle.Include("~/Scripts/textAngular-sanitize.js");
            appConfigBundle.Include("~/Scripts/textAngular.min.js");
            appConfigBundle.Include("~/Scripts/jquery.responsiveTabs.js");
            appConfigBundle.Include("~/Scripts/menuscript.js");
            appConfigBundle.Include("~/Scripts/toastr.js");
            appConfigBundle.Include("~/Scripts/Chart.min.js");
            appConfigBundle.Include("~/JS/appConfiguration.js");
            appConfigBundle.Include("~/JS/Common.js");
            appConfigBundle.Include("~/Scripts/bsDuallistbox.js");
            appConfigBundle.IncludeDirectory("~/JS/Directives", "*.js", false);
            appConfigBundle.IncludeDirectory("~/JS/Factory", "*.js", false);
            appConfigBundle.IncludeDirectory("~/JS/Filters", "*.js", false);
            appConfigBundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(appConfigBundle);



            ////////////////////Admin//////////////////////

            //---------------------CSS--------------------//
            bundles.Add(new StyleBundle("~/bundles/AdminCss").Include(
                        "~/Content/admin/css/bootstrap.css",
                      "~/Content/admin/css/font-awesome.min.css",
                     "~/Content/admin/css/icons/icomoon/styles.css",
                     "~/Content/admin/css/core.css",
                     "~/Content/admin/css/components.css",
                     "~/Content/admin/css/colors.css",
                     "~/Content/css/toastr.css",
                      "~/Scripts/angular-star-rating/main.css",
                      "~/Content/css/angular-ui-switch.min.css"
                     ));

            //---------------------CSS--------------------//

            //--------------------js---------------------------//
            bundles.Add(new ScriptBundle("~/bundles/AdminJs").Include(
                      "~/Scripts/admin/js/plugins/loaders/pace.min.js",
                      "~/Scripts/admin/js/core/libraries/bootstrap.min.js",
                        "~/Scripts/admin/js/plugins/loaders/blockui.min.js",
                        "~/Scripts/admin/js/plugins/visualization/d3/d3.min.js",
                       "~/Scripts/admin/js/plugins/visualization/d3/d3_tooltip.js",
                       "~/Scripts/admin/js/plugins/forms/styling/switchery.min.js",
                       "~/Scripts/admin/js/plugins/forms/styling/uniform.min.js",
                       "~/Scripts/admin/js/plugins/forms/selects/bootstrap_multiselect.js",
                       "~/Scripts/admin/js/plugins/ui/moment/moment.min.js",
                       "~/Scripts/admin/js/plugins/pickers/daterangepicker.js",
                       "~/Scripts/admin/js/core/app.js",
                       "~/Scripts/admin/js/pages/dashboard.js",
                       "~/Scripts/ckeditor/ckeditor.js"
                      ));

            var AdminControllerBundle = new Bundle("~/bundles/AdminControllers");
            AdminControllerBundle.IncludeDirectory("~/JS/Home", "*.js", false);
            AdminControllerBundle.IncludeDirectory("~/JS/Test", "*.js", false);
            AdminControllerBundle.IncludeDirectory("~/JS/Admin", "*.js", true);
            bundles.Add(AdminControllerBundle);
            //--------------------js---------------------------//
            ////////////////////Admin //////////////////////


            //--This is Temporary--//
            var controllerBundle = new Bundle("~/bundles/Controllers");
            controllerBundle.IncludeDirectory("~/JS/Home", "*.js", false);
            controllerBundle.IncludeDirectory("~/JS/Test", "*.js", false);
            bundles.Add(controllerBundle);
            //--This is Temporary--//

            ///////////////////// Layout1//////////////////
            //------------------------CSS-------------------------//
            bundles.Add(new StyleBundle("~/bundles/Layout1CSS").Include(
                     "~/Content/layout1/css/bootstrap.min.css",
                     "~/Content/layout1/css/font-awesome.min.css",
                     "~/Content/layout1/css/jquery.bxslider.min.css",
                     "~/Content/layout1/css/jquery.mCustomScrollbar.min.css",
                     "~/Content/layout1/css/style.css",
                     "~/Content/layout1/css/dpNumberPicker.min.css",
                     "~/Content/layout1/css/responsive.css",
                     "~/Content/layout1/css/classes.css",
                     "~/Content/layout1/css/zoom.css",
                     "~/Content/css/toastr.min.css",
                     "~/Scripts/angular-star-rating/main.css",
                      "~/Content/css/angular-ui-switch.min.css"
                  ));
            //------------------------CSS-------------------------//

            //------------------------js-------------------------//

            var Layout1Js = new Bundle("~/bundles/Layout1Js");
            Layout1Js.Include("~/Scripts/layout1/js/jquery.min.js");
            Layout1Js.Include("~/Scripts/layout1/js/bootstrap.min.js");
            Layout1Js.Include("~/Scripts/layout1/js/jquery.bxslider.min.js");
            Layout1Js.Include("~/Scripts/layout1/js/jquery.mCustomScrollbar.concat.min.js");
            Layout1Js.Include("~/Scripts/layout1/js/dpNumberPicker.min.js");
            Layout1Js.Include("~/Scripts/layout1/js/enhance.js");
            Layout1Js.Include("~/Scripts/layout1/js/velocity.min.js");
            Layout1Js.Include("~/Scripts/bootstrap-modalmanager.js");
            Layout1Js.Include("~/Scripts/bootstrap-modal.js");
            Layout1Js.Include("~/Scripts/bootstrap-datepicker.js");
            Layout1Js.Include("~/Scripts/jquery.bootstrap-duallistbox.js");
            Layout1Js.Include("~/Scripts/bootbox.js");
            Layout1Js.Include("~/Scripts/ckeditor/ckeditor.js");
            bundles.Add(Layout1Js);
            //------------------------js-------------------------//

            ///////////////////// Layout1//////////////////

            ///////////////////// Layout2//////////////////
            //------------------------CSS-------------------------//
            bundles.Add(new StyleBundle("~/bundles/Layout2CSS").Include(
                  "~/Content/layout1/css/bootstrap.min.css",
                     "~/Content/layout1/css/font-awesome.min.css",
                     "~/Content/layout1/css/jquery.bxslider.min.css",
                     "~/Content/layout1/css/jquery.mCustomScrollbar.min.css",
                     "~/Content/layout2/css/custom.min.css",
                      "~/Content/layout1/css/dpNumberPicker.min.css",
                     "~/Content/layout2/css/responsive.css",
                     "~/Content/layout1/css/classes.css",
                     "~/Content/layout1/css/zoom.css",
                     "~/Content/css/toastr.min.css",
                     "~/Scripts/angular-star-rating/main.css",
                      "~/Content/css/angular-ui-switch.min.css"
                //"~/Content/layout2/css/bootstrap.min.css",
                //"~/Content/layout1/css/font-awesome.min.css",
                //"~/Content/layout1/css/jquery.bxslider.min.css",
                //"~/Content/layout2/css/jquery.mCustomScrollbar.min.css",
                //"~/Content/layout2/css/custom.min.css",
                //"~/Content/layout2/css/style.css",
                //"~/Content/layout1/css/dpNumberPicker.min.css",
                //"~/Content/layout2/css/responsive.css",
                //"~/Content/layout1/css/classes.css",
                //"~/Content/layout2/css/flexslider.css",
                //"~/Content/layout1/css/zoom.css",
                //"~/Content/css/toastr.min.css",
                //"~/Scripts/angular-star-rating/main.css",
                //"~/Content/css/angular-ui-switch.min.css"
               ));

            //------------------------CSS-------------------------//

            //------------------------js-------------------------//
            //js/jquery.min.js
            bundles.Add(new ScriptBundle("~/bundles/Layout2Js").Include(
                     "~/Scripts/layout2/js/jquery.min.js",
                     "~/Scripts/layout2/js/bootstrap.min.js",
                     //"~/Scripts/layout2/js/jquery.flexslider.js",
                     "~/Scripts/bootstrap-modalmanager.js",
                    "~/Scripts/bootstrap-modal.js",
                    "~/Scripts/bootstrap-datepicker.js",
                    "~/Scripts/jquery.bootstrap-duallistbox.js",
                     "~/Scripts/bootbox.js",
                     "~/Scripts/ckeditor/ckeditor.js",
                  "~/Scripts/layout1/js/jquery.bxslider.min.js",
                  "~/Scripts/layout1/js/dpNumberPicker.min.js",
                  "~/Scripts/layout1/js/enhance.js",
                  "~/Scripts/layout1/js/velocity.min.js",
                  "~/Scripts/layout2/js/jquery.mCustomScrollbar.concat.min.js"
                     ));

            //------------------------js-------------------------//


            ///////////////////// Layout2//////////////////


            ///////Layout Controllers//////
            var LayoutControllerBundle = new Bundle("~/bundles/LayoutControllers");
            LayoutControllerBundle.IncludeDirectory("~/JS/Layout", "*.js", true);
            bundles.Add(LayoutControllerBundle);
            ///////////////////////////////



            ////////////////////////Layout2 Complete Js Bundling////////////////
            var Layout2AllJs = new Bundle("~/bundles/Layout2AllJs");
                Layout2AllJs.Include("~/Scripts/layout2/js/jquery.min.js");
                Layout2AllJs.Include("~/Scripts/layout2/js/bootstrap.min.js");
                //Layout2AllJs.Include("~/Scripts/layout2/js/jquery.flexslider.js");
                Layout2AllJs.Include("~/Scripts/layout2/js/jquery.mCustomScrollbar.concat.min.js");
                Layout2AllJs.Include("~/Scripts/bootstrap-modalmanager.js");
                Layout2AllJs.Include("~/Scripts/bootstrap-modal.js");
                Layout2AllJs.Include("~/Scripts/bootstrap-datepicker.js");
                Layout2AllJs.Include("~/Scripts/jquery.bootstrap-duallistbox.js");
                Layout2AllJs.Include("~/Scripts/bootbox.js");
                Layout2AllJs.Include("~/Scripts/ckeditor/ckeditor.js");
                Layout2AllJs.Include("~/Scripts/layout1/js/jquery.bxslider.min.js");
                Layout2AllJs.Include("~/Scripts/layout1/js/dpNumberPicker.min.js");
                Layout2AllJs.Include("~/Scripts/layout1/js/enhance.js");
                Layout2AllJs.Include("~/Scripts/layout1/js/velocity.min.js");
                Layout2AllJs.Include("~/Scripts/jquery-ui.min.js"); 
                Layout2AllJs.Include("~/Scripts/angular.js");
                Layout2AllJs.Include("~/Scripts/angular-animate.js");
                Layout2AllJs.Include("~/Scripts/angular-dragdrop.min.js");
                Layout2AllJs.Include("~/Scripts/angular-ui-router.min.js");
                Layout2AllJs.Include("~/Scripts/angular-local-storage.js");
                Layout2AllJs.Include("~/Scripts/angular-sanitize.js");
                Layout2AllJs.Include("~/Scripts/angular-datatables.min.js");
                Layout2AllJs.Include("~/Scripts/angular-ui-switch.min.js");
                Layout2AllJs.Include("~/Scripts/angular-star-rating/main.js");
                Layout2AllJs.Include("~/Scripts/ui-bootstrap-tpls-0.12.1.min.js");
                Layout2AllJs.Include("~/Scripts/ng-ckeditor.js");
                Layout2AllJs.Include("~/Scripts/jquery.dataTables.min.js");
                Layout2AllJs.Include("~/Scripts/dataTables.tableTools.js");
                Layout2AllJs.Include("~/Scripts/jquery.dataTables.rowReordering.js");
                Layout2AllJs.Include("~/Scripts/ng-file-upload.min.js");
                Layout2AllJs.Include("~/Scripts/ng-file-upload-shim.min.js");
                Layout2AllJs.Include("~/Scripts/textAngular-rangy.min.js");
                Layout2AllJs.Include("~/Scripts/textAngular-sanitize.js");
                Layout2AllJs.Include("~/Scripts/textAngular.min.js");
                Layout2AllJs.Include("~/Scripts/jquery.responsiveTabs.js");
                Layout2AllJs.Include("~/Scripts/menuscript.js");
                Layout2AllJs.Include("~/Scripts/toastr.js");
                Layout2AllJs.Include("~/Scripts/Chart.min.js");
                Layout2AllJs.Include("~/JS/appConfiguration.js");
                Layout2AllJs.Include("~/JS/Common.js");
                Layout2AllJs.Include("~/Scripts/bsDuallistbox.js");
                Layout2AllJs.IncludeDirectory("~/JS/Directives", "*.js", false);
                Layout2AllJs.IncludeDirectory("~/JS/Factory", "*.js", false);
                Layout2AllJs.IncludeDirectory("~/JS/Filters", "*.js", false); 
                Layout2AllJs.IncludeDirectory("~/JS/Layout", "*.js", true);
                Layout2AllJs.Include("~/Scripts/layout2/js/jquery.mCustomScrollbar.concat.min.js");
                Layout2AllJs.Include("~/Scripts/jquery.cookiesdirective.js");            
                Layout2AllJs.Orderer = new NonOrderingBundleOrderer();
                bundles.Add(Layout2AllJs);
            //////////////////////////////////////////////////////////////////

        }
    }
}
