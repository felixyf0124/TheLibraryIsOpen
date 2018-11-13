using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public TypeEnum ModelType { get; set; }
    }
}
