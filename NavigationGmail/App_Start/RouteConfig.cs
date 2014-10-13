using System.Web.Mvc;
using System.Web.Routing;

namespace NavigationGmail
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Content",
				url: "Content",
				defaults: new { controller = "Mail", action = "_Content" }
			);
		}
    }
}
