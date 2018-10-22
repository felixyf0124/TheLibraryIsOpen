using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Constants
{
    public static class TypeConstants
    {
        public enum TypeEnum
        {
            Book = 0,
            Magazine = 1,
            Movie = 2,
            Music = 3,
            Person = 4
        };

        private static Dictionary<Type, TypeEnum> typeDict = new Dictionary<Type, TypeEnum>
        {
            {typeof(Book),          TypeEnum.Book},
            {typeof(Magazine),      TypeEnum.Magazine},
            {typeof(Movie),         TypeEnum.Movie},
            {typeof(Music),         TypeEnum.Music},
            {typeof(Person),        TypeEnum.Person}
        };

        public static TypeEnum GetTypeNum(Type t)
        {
            return typeDict[t];
        }
    }
}
