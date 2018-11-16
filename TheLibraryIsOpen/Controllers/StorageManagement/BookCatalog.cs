using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database; // TODO: delete this when db code is removed
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class BookCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;
        private readonly Db _db; // TODO: delete this when db code is removed


        public BookCatalog(UnitOfWork unitOfWork, IdentityMap im, Db db)
        {
            _unitOfWork = unitOfWork;
            _im = im;
            _db = db; // TODO: delete this when db code is removed
        }

        //Create Book
        public Task<IdentityResult> CreateAsync(Book book)
        {
            if (book != null)
            {

                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false

                    _unitOfWork.RegisterNew(book);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "book was null" });
            });
        }


        //Delete Book
        public Task<IdentityResult> DeleteAsync(Book book)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDeleted(book);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "book was null" });
            });
        }

        //Find methods (by id, isbn10, isbn13)

        public async Task<Book> FindByIdAsync(string bookId)
        {
            Book book = await _im.FindBook(int.Parse(bookId));

            return book;
        }

        public Task<Book> FindByIsbn10Async(string isbn10)
        {
            if (!string.IsNullOrEmpty(isbn10))
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: replace with _im
                    return _db.GetBookByIsbn10(isbn10);
                });
            }
            throw new ArgumentNullException("isbn10");
        }

        public Task<Book> FindByIsbn13Async(string isbn13)
        {
            if (!string.IsNullOrEmpty(isbn13))
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: replace with _im
                    return _db.GetBookByIsbn13(isbn13);
                });
            }
            throw new ArgumentNullException("isbn13");
        }


        //Update Methods (per attribute, general)
        public Task<IdentityResult> UpdateAsync(Book book)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDirty(book);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "book was null" });
            });
        }

        public Task<IdentityResult> addModelCopy(Book book)
        {
            if (book != null)
            {

                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false

                    _unitOfWork.RegisterNew(new ModelCopy
                    {
                        modelID = book.BookId,
                        modelType = TypeEnum.Book
                    });
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "book was null" });
            });
        }

        //Get all Books
        public Task<List<Book>> GetAllBookDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                // TODO: replace with _im
                return _db.GetAllBooks();
            });
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public Task<int> getNoOfAvailableModelCopies(Book book)
        {
            return Task.Factory.StartNew(() =>
            {

                int AvailableCopies = _db.CountModelCopiesOfModel(book.BookId, (int)TypeEnum.Book, BorrowType.NotBorrowed);

                return AvailableCopies;

            });

        }

        public async Task<List<ModelCopy>> getModelCopies(Book book)
        {
            List<ModelCopy> copies = await _im.FindModelCopies(book.BookId, TypeEnum.Book);
            return copies;
        }
    }
}
