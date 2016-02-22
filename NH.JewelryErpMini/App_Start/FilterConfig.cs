using System.Web;
using System.Web.Mvc;

namespace NH.JewelryErpMini
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
