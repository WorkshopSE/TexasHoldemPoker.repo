﻿using Poker.BE.Service.Modules.Caches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Poker.BE.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected ICollection<ICache> Chaches = new List<ICache>();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Caches
            Chaches.Add(CommonCache.Instance);

            // TODO: idan - this is MVC adding, clear this
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        
    }
}
