using System.Web;
using System.Web.Mvc;

namespace blog_mvc_yunus
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
