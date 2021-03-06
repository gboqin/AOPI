﻿using System.Web;
using System.Web.Optimization;

namespace NH.JewelryErpMini
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/easyuijs").Include(
                      "~/Scripts/jquery.easyui-{version}.js",
                      "~/Scripts/jquery.easyui-1.4.3.min.js",
                      "~/Scripts/jquery.easyui-{version}.min.js",
                      "~/Scripts/locale/easyui-lang-zh_CN.js"));

            bundles.Add(new ScriptBundle("~/bundles/easyuicss").Include(
                      "~/Content/themes/icon.css",
                      "~/Content/themes/default.css",
                      "~/Content/themes/default/icon.css",
                      "~/Content/themes/default/easyui.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));
                      //"~/Content/site.css"
            bundles.Add(new ScriptBundle("~/Scripts/businessGrid").Include(
                      "~/Scripts/projectJs/businessGrid.js",
                      "~/Scripts/datagrid-detailview.js"
                ));
            bundles.Add(new ScriptBundle("~/Scripts/businessmsgGrid").Include(
                      "~/Scripts/projectJs/businessDlgMsg.js",
                      "~/Scripts/datagrid-detailview.js"
                ));
        }
    }
}
