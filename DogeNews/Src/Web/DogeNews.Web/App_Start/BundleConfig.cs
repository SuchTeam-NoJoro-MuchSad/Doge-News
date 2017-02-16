using System.Web.Optimization;
using System.Web.UI;

namespace DogeNews.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include(
                "~/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/jquery.signalR-2.2.1.min.js",
                "~/Scripts/Common/hubs.js",
                "~/Scripts/Common/notificator.js"));
            
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "signalR",
                new ScriptResourceDefinition
                {
                    Path = "~/bundles/SignalR"
                });
        }
    }
}