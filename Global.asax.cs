namespace Tweets
{
    using App_Start;
    using Properties;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                StaticResourcesConfig.RegisterStaticResources(Settings.Default.UserFilePath,
                    Settings.Default.TweetFilePath);
            }
            catch (Exception /*ex*/)
            {
                //TODO: Log exception.
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!StaticResourcesConfig.IsInitialized)
            {
                try
                {
                    StaticResourcesConfig.RegisterStaticResources(Settings.Default.UserFilePath,
                        Settings.Default.TweetFilePath);
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Items["Error"] = ex;
                    HttpContext.Current.RewritePath("~/Error");
                }
            }
        }
    }
}
