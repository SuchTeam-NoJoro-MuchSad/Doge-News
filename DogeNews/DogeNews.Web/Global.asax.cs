﻿using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

using DogeNews.Web.App_Start;

namespace DogeNews.Web
{
    public class Global : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BindingsConfig.BindPresenterFactory();
            MappingsConfig.Map();
        }
    }
}