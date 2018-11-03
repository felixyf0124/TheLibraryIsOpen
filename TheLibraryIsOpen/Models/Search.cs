using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Database;
using static TheLibraryIsOpen.Constants.TypeConstants;


namespace TheLibraryIsOpen.Models
{
    public class Search
    {
        private readonly Db _db;
        SearchResult[] newResults;


        public Search(Db db)
        {
            _db = db;
        }

        //Search all
        //public async Task<List<SearchResult>> searchAll(string searchStr)
        //{
        //    return Task.Factory.StartNew(() =>
        //    {
        //        List<SearchResult> _allForOneStr = new List<SearchResult>();

        //        List<SearchResult> foundFromBK = _db.searchBook(searchStr);
        //        List<SearchResult> foundFromMG = _db.searchMagazine(searchStr);
        //        List<SearchResult> foundFromMV = _db.searchMovie(searchStr);
        //        List<SearchResult> foundFromMS = _db.searchMusic(searchStr);

                
                
        //        for (int i = 0; i < foundFromBK.Count; i++)
        //        {
        //            _allForOneStr.Add(foundFromBK[i]);
                    
        //        }
        //        for (int i = 0; i < foundFromMG.Count; i++)
        //        {
        //            _allForOneStr.Add(foundFromMG[i]);

        //        }
        //        for (int i = 0; i < foundFromMV.Count; i++)
        //        {
        //            _allForOneStr.Add(foundFromMV[i]);

        //        }
        //        for (int i = 0; i < foundFromMS.Count; i++)
        //        {
        //            _allForOneStr.Add(foundFromMS[i]);

        //        }

        //        return _allForOneStr;
        //    });
        //}

        


    }


    public class SearchResult
    {
        public TypeEnum Type { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string[] Description { get; set; }

        //Default constructor
        public SearchResult() { }

        //constructor
        public SearchResult(TypeEnum _type, int _Id, string _name, string[] _description)
        {
            Type = _type;
            ID = _Id;
            Name = _name;
            Description = new string[_description.Length];
            for (int i = 0; i< _description.Length; i++ )
            {
                Description[i] = _description[i];
            }
        }

    }

}
