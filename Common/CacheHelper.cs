using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class CacheHelper
    {
        public static object Get(string key)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            return cache.Get(key);
        }

        public static void Set(string key, object value)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            cache[key] = value;
        }


        public static void Set(string key, object value, DateTime time)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            cache.Insert(key, value,null,time,TimeSpan.Zero);
        }

        public static void Remove(string key)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            cache.Remove(key); 

        }
    }
}