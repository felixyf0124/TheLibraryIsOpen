using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TheLibraryIsOpen.Constants
{
    /// <summary>
    /// Allows for complex object storage
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Stores the object to the session
        /// </summary>
        /// <param name="key">The name of the object in the session</param>
        /// <param name="obj">The object to store to the session</param>
        /// <seealso cref="TheLibraryIsOpen.Models.SessionModel" />
        /// <code>
        /// var books = HttpContext.Session.GetObject&lt;List&lt;SessionModel&gt;&gt;("Books") ?? new List&lt;SessionModel&gt;();
        /// </code>
        public static void SetObject(this ISession session, string key, object obj)
        {
            session.SetString(key, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// Retrieves the object from the session
        /// </summary>
        /// <param name="key">The name of the object in the session</param>
        /// <seealso cref="TheLibraryIsOpen.Models.SessionModel" />
        /// <code>
        /// SessionModel sm = new SessionModel {
        ///     Id = book.Id,
        ///     ModelType = TypeEnum.Book
        /// }
        /// var books = HttpContext.Session.GetObject&lt;List&lt;SessionModel&gt;&gt;("Books") ?? new List&lt;SessionModel&gt;();
        /// books.Add(sm);
        /// HttpContext.Session.SetObject("Books", books);
        /// </code>
        public static T GetObject<T>(this ISession session, string key)
        {
            string jsonObject = session.GetString(key);
            return jsonObject is null ? default(T) : JsonConvert.DeserializeObject<T>(jsonObject);
        }
    }
}
