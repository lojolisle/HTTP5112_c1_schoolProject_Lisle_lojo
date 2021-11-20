using System.Web;
using System.Web.Mvc;

namespace WebApplication_HTTP5112_SchoolProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
