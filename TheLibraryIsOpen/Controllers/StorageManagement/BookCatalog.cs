using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Database; // TODO: delete this when db code is removed
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

        public void Dispose()
        { }


        //Find methods (by id, isbn10, isbn13)

        public Task<Book> FindByIdAsync(string bookId)
        {
            return Task.Factory.StartNew(() =>
            {
                Book book = _im.FindBook(int.Parse(bookId));

                return book;
            });
            throw new ArgumentNullException("bookId");
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

        public Task SetTitleAsync(Book book, string title)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Title = title;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetAuthorAsync(Book book, string author)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Author = author;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetFormatAsync(Book book, string format)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Format = format;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetPagesAsync(Book book, int pages)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Pages = pages;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetPublisherAsync(Book book, string publisher)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Publisher = publisher;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetYearAsync(Book book, string date)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Date = date;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetLanguageAsync(Book book, string language)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Language = language;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }



        public Task SetIsbn10Async(Book book, string isbn10)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Isbn10 = isbn10;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetIsbn13Async(Book book, string isbn13)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Isbn13 = isbn13;
                    _unitOfWork.RegisterDirty(book);
                });
            }
            throw new ArgumentNullException("book");
        }

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


    }
}
