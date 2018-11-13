using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models
{
    /// <summary>
    /// Stores necessary model information to keep in session
    /// </summary>
    /// <seealso cref="TheLibraryIsOpen.Constants.SessionExtensions"/>
    public class SessionModel
    {
        public int Id { get; set; }
        public TypeEnum ModelType { get; set; }
    }
}
