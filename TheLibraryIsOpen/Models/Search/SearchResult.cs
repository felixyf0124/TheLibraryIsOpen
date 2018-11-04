using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Models.Search
{
    public class SearchResult
    {
        public TypeEnum Type { get; set; }
        public int ModelId { get; set; }
        public string Name { get; set; }
        public string[] Description { get; set; }

        //Default constructor
        public SearchResult() { }

        //constructor
        public SearchResult(TypeEnum _type, int _modelId, string _name, string[] _description)
        {
            Type = _type;
            ModelId = _modelId;
            Name = _name;
            Description = _description;
        }

    }

}
