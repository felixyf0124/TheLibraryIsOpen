using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TheLibraryIsOpen.Constants
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object obj)
        {
            session.SetString(key, JsonConvert.SerializeObject(obj));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string jsonObject = session.GetString(key);
            return jsonObject is null ? default(T) : JsonConvert.DeserializeObject<T>(jsonObject);
        }
    }
}
