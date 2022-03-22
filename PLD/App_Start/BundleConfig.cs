using System.Web;
using System.Web.Optimization;

namespace PLD
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //-- Bundle Login --//
            //--
            bundles.Add(new StyleBundle("~/Content/FontAwesome").Include(
                                "~/Content/font-awesome.min.css",
                                "~/Content/icon/icofont/css/icofont.css"));

            bundles.Add(new StyleBundle("~/Content/SimpleLineIcons").Include(
                                "~/Content/icon/simple-line-icons/css/simple-line-icons.css"));

            bundles.Add(new StyleBundle("~/Content/BootstrapV4").Include(
                                "~/Content/bootstrap/css/bootstrap.min.css",
                                "~/plugins/bootstrap-select-1.13.9/dist/css/bootstrap-select.min.css"));

            bundles.Add(new StyleBundle("~/Content/AbleProLite").Include(
                                "~/Content/mainAbleProLite.css",
                                "~/Content/responsive.css",
                                "~/Content/mdb.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                "~/Scripts/jquery/jquery-3.3.1.js",
                                "~/Scripts/jquery.unobtrusive-ajax.js",
                                "~/Scripts/jquery.validate.js",
                                "~/Scripts/jquery.validate.unobtrusive.js",
                                "~/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                                "~/Scripts/jquery-ui/jquery-ui.min.js",
                                "~/Scripts/Waves/waves.min.js",
                                "~/Scripts/elements.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapl").Include(
                                 "~/Scripts/umd/popper.min.js",
                                 "~/Scripts/popper.min.js",
                                 "~/Scripts/moment/moment.js",
                                 "~/Scripts/tether/dist/js/tether.js",
                                 "~/Scripts/bootstrap/js/bootstrap.js",
                                 "~/plugins/bootstrap-select-1.13.9/dist/js/bootstrap-select.js",
                                 "~/Scripts/mdb.min.js"));
            //-- Términa login --//


            //-- Bundle Main PLD --//
            bundles.Add(new ScriptBundle("~/bundles/bootstrapTest").Include(
                                "~/Content/css/bootstrap.min.css",
                                "~/Content/css/bootstrap-responsive.min.css",
                                "~/Content/css/colorpicker.css",
                                "~/Content/css/datepicker.css",
                                "~/Content/css/uniform.css",
                                "~/Content/css/select2.css",
                                "~/Content/css/jquery.gritter.css",
                                "~/Content/css/maruti-style.css",
                                "~/Content/css/maruti-media.css",
                                "~/pugins/gijgo/gijgo.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymaruti").Include(
                                //"~/Content/js/lib/jquery.js",
                                "~/Scripts/jquery/jquery-3.3.1.js",
                                "~/Scripts/jquery.unobtrusive-ajax.js",
                                "~/Content/js/jquery.min.js",
                                "~/Content/js/jquery.ui.custom.js",
                                "~/Content/js/bootstrap.min.js",
                                "~/Content/js/jquery.uniform.js",
                                "~/Content/js/select2.min.js",
                                "~/Content/js/jquery.dataTables.min.js",
                                "~/Content/js/maruti.js",
                                "~/Content/js/maruti.tables.js",
                                "~/Scripts/bootbox.min.js",
                                //"~/Content/js/jquery.validate.js",

                                "~/Content/js/bootstrap-colorpicker.js",
                                "~/Content/js/bootstrap-datepicker.js",

                                "~/Content/js/jquery.gritter.min.js",
                                "~/Content/js/jquery.peity.min.js",

                                "~/Content/js/maruti.interface.js",
                                //"~/Content/js/maruti.popover.js"
                                //"~/Content/js/maruti.form_validation.js",
                                /*"~/Content/js/maruti.form_common.js"*/
                                "~/plugins/gijgo/gijgo.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquerymarutitable").Include(
                                "~/Content/js/jquery.dataTables.min.js",
                                "~/Content/js/maruti.js",
                                "~/Content/js/maruti.tables.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapdate").Include(
                                 "~/Content/css/colorpicker.css",
                                "~/Content/css/datepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryfirst").Include(
                                "~/Scripts/jquery/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydate").Include(
                                "~/Content/js/bootstrap.min.js",
                                "~/Content/js/bootstrap-colorpicker.js",
                                "~/Content/js/bootstrap-datepicker.js",
                                "~/Content/js/maruti.form_common.js"));

            bundles.Add(new ScriptBundle("~/bundles/gijgo").Include(
                                "~/plugins/gijgo/gijgo.min.js"));
            //-- Términa Bundle Main PLD --//

            //-- Test Layout --//
            bundles.Add(new ScriptBundle("~/bundles/bootstrapLayout").Include(
                                 "~/Content/css/bootstrap.min.css",
                                 "~/Content/css/bootstrap-responsive.min.css",
                                 "~/Content/css/colorpicker.css",
                                 "~/Content/css/datepicker.css",
                                 "~/Content/css/uniform.css",
                                 "~/Content/css/select2.css",
                                 "~/Content/css/maruti-style.css",
                                  "~/Content/css/maruti.form_validation.css",
                                 "~/Content/css/maruti-media.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryLayout").Include(
                        "~/Scripts/jquery/jquery-3.3.1.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Content/js/jquery.min.js",
                        "~/Content/js/jquery.ui.custom.js",
                        "~/Content/js/bootstrap.min.js",
                        "~/Content/js/bootstrap-colorpicker.js",
                        "~/Content/js/bootstrap-datepicker.js",
                        "~/Content/js/jquery.gritter.min.js",
                        "~/Content/js/jquery.uniform.js",
                        "~/Content/js/jquery.peity.min.js",
                        "~/Content/js/select2.min.js",
                        //"~/Content/js/jquery.validate.js",
                        "~/Content/js/jquery.dataTables.min.js",
                        "~/Content/js/maruti.js",
                        "~/Content/js/maruti.form_common.js",
                        "~/Content/js/maruti.tables.js", 
                        "~/Content/js/maruti.interface.js",
                        "~/Content/js/maruti.popover.js"));

            //-- Termina Test Layout --//

        }
    }
}
