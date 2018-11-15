using System;
using System.Collections.Generic;
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
            Person = 4,
            ModelCopy = 5,
            Log = 6
        };

        public enum BorrowType
        {
            Borrowed = 0,
            NotBorrowed = 1,
            Any = 3

        };

        private static Dictionary<Type, TypeEnum> typeDict = new Dictionary<Type, TypeEnum>
        {
            {typeof(Book),          TypeEnum.Book},
            {typeof(Magazine),      TypeEnum.Magazine},
            {typeof(Movie),         TypeEnum.Movie},
            {typeof(Music),         TypeEnum.Music},
            {typeof(Person),        TypeEnum.Person},
            {typeof(ModelCopy),        TypeEnum.ModelCopy},
            {typeof(Log),        TypeEnum.Log}
        };

        public static TypeEnum GetTypeNum(Type t)
        {
            return typeDict[t];
        }
    }
}
