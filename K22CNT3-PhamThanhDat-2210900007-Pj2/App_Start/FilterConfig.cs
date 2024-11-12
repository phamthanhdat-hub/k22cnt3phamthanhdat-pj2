using System.Web;
using System.Web.Mvc;

namespace K22CNT3_PhamThanhDat_2210900007_Pj2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
