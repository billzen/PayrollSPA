using System.Web;
using System.Web.Optimization;

namespace Payroll.WebApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/Vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/Vendors/jquery.js",               
                "~/Scripts/Vendors/toastr.js",
                "~/Scripts/Vendors/jquery.raty.js",
                "~/Scripts/Vendors/respond.src.js",
                "~/Scripts/Vendors/angular.js",
                "~/Scripts/Vendors/angular-route.js",
                "~/Scripts/Vendors/angular-cookies.js",
                "~/Scripts/Vendors/angular-validator.js",
                "~/Scripts/Vendors/angular-base64.js",
                "~/Scripts/Vendors/angular-file-upload.js",
                "~/Scripts/Vendors/angucomplete-alt.min.js",
                "~/Scripts/Vendors/bootstrap.js",
                "~/Scripts/Vendors/ui-bootstrap-tpls-0.13.1.js",
                "~/Scripts/Vendors/underscore.js",
                "~/Scripts/Vendors/raphael.js",
                "~/Scripts/Vendors/morris.js",
                "~/Scripts/Vendors/jquery.fancybox.js",
                "~/Scripts/Vendors/jquery.fancybox-media.js",
                "~/Scripts/Vendors/loading-bar.js",
                //"~/Scripts/Vendors/bootstrap-timepicker.min.js",
                 "~/Scripts/Vendors/bootstrap-timepicker.js"//,
                //"~/Scripts/Vendors/bootstrap.min.js"
                //"~/Scripts/Vendors/jquery-2.1.4.min.js"
                ));


            //bundles.Add(new ScriptBundle("~/bundles/vendors/AddOns").Include(
            //    //"~/Scripts/Vendors/AddOns/bootstrap.min.js",
            //    //"~/Scripts/Vendors/AddOns/bootstrap-timepicker.js",
            //    "~/Scripts/Vendors/AddOns/bootstrap-timepicker.min.js"//,
            //    // "~/Scripts/Vendors/AddOns/jquery-2.1.4.min.js"

            //    ));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/membershipService.js",
                "~/Scripts/spa/services/fileUploadService.js",
                "~/Scripts/spa/services/fileUploadBinaryService.js",
                "~/Scripts/spa/layout/topBar.directive.js",
                "~/Scripts/spa/layout/sideBar.directive.js",
                "~/Scripts/spa/layout/customPager.directive.js",
                //"~/Scripts/spa/directives/rating.directive.js",
                "~/Scripts/spa/directives/EmployeeType.directive.js",
                //"~/Scripts/spa/directives/EmployeeDetails.directive.js",  // *********** Not used in FullTime. performance errors. Keep for future use eith only one reponse
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js",
                "~/Scripts/spa/employees/employeesCtrl.js",
                "~/Scripts/spa/employees/employeesEditCtrl.js",
                "~/Scripts/spa/employees/employeesRegCtrl.js",
                "~/Scripts/spa/fulltimeemployees/fulltimeemployeesCtrl.js",
                "~/Scripts/spa/fulltimeemployees/fulltimeemployeesEditCtrl.js",                
                "~/Scripts/spa/commissionedemployees/commissionedemployeesCtrl.js",
                "~/Scripts/spa/commissionedemployees/commissionedemployeesEditCtrl.js",
                "~/Scripts/spa/parttimeemployees/parttimeemployeesCtrl.js",
                "~/Scripts/spa/parttimeemployees/parttimeemployeesEditCtrl.js",

                "~/Scripts/spa/orders/ordersCtrl.js",
                "~/Scripts/spa/orders/ordersEditCtrl.js",
                "~/Scripts/spa/orders/ordersAddCtrl.js",
                "~/Scripts/spa/orders/AddEmployeeToOrdersCtrl.js",
                "~/Scripts/spa/products/productsCtrl.js",
                "~/Scripts/spa/products/productsEditCtrl.js",
                "~/Scripts/spa/products/productsAddCtrl.js",
                "~/Scripts/spa/logentries/logentriesCtrl.js",
                "~/Scripts/spa/logentries/logentriesEditCtrl.js"                
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/content/css/site.css",
                "~/content/css/bootstrap.css",
                "~/content/css/bootstrap-theme.css",
                 "~/content/css/font-awesome.css",
                "~/content/css/morris.css",
                "~/content/css/toastr.css",
                "~/content/css/jquery.fancybox.css",
                "~/content/css/loading-bar.css",
              //  "~/content/css/bootstrap.min.css",
                "~/content/css/bootstrap-timepicker.min.css"));

            //bundles.Add(new StyleBundle("~/Content/css/AddOns").Include(
            //    //"~/content/css/AddOns/bootstrap.min.css",
            //    //"~/content/css/AddOns/bootstrap-timepicker.css",
            //    "~/content/css/AddOns/bootstrap-timepicker.min.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
