using Newtonsoft.Json;
using System;
using System.Web;

namespace HealthyMeal.Utils
{
    public static class NavigationParameterConverter
    {
        public static string ObjectToPairKeyValue(object obj, string objectName)
        {
            if (obj != null)
                return $"{objectName}={JsonConvert.SerializeObject(obj)}";
            return null;
        }

        public static T ObjectFromPairKeyValue<T>(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException("Пустой параметр");
            return JsonConvert.DeserializeObject<T>(parameter);
        }

        public static T ObjectFromUrl<T>(string url)
        {
            return ObjectFromPairKeyValue<T>(HttpUtility.UrlDecode(url));
        }
    }
}
