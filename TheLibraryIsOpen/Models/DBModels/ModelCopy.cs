using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLibraryIsOpen.Models.DBModels
{
    public class ModelCopy
    {
        int id;
        enum ModelType {Book = 0, Magazine = 1, Movie = 2, Music = 3};
        ModelType modelType;
        int modelID;
        int borrowerID;
        DateTime borrowedDate;
        DateTime returnDate;

    }
}
 