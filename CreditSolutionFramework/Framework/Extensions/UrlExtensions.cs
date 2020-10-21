using System.Linq;
using System.Web;

namespace Framework.Extensions
{
    public static class UrlExtensions
    {
        public static string GetUrlWithQueryObject(string url, object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)?.ToString());

            var queryString = string.Join("&", properties.ToArray());

            return string.IsNullOrEmpty(queryString) ? url : $"{url}?{queryString}";
        }
    }
}