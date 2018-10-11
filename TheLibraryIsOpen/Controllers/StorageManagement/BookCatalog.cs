using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class BookCatalog 
    {
        private readonly Db _db;

        public BookCatalog(Db db)
        {
            _db = db;
        }

        //Create Book
        public Task<IdentityResult> CreateAsync(Book book)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    if (_db.GetBookByIsbn10(book.Isbn10) != null)
                        return IdentityResult.Failed(new IdentityError { Description = "book with this isbn10 already exists" });
                    _db.CreateBook(book);
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
                    _db.DeleteBook(book);
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
                return _db.GetBookById((int.Parse(bookId)));
            });
            throw new ArgumentNullException("bookId");
        }

        public Task<Book> FindByIsbn10Async(string isbn10)
        {
            if (!string.IsNullOrEmpty(isbn10))
            {
                return Task.Factory.StartNew(() =>
                {
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
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
                });
            }
            throw new ArgumentNullException("book");
        }

        public Task SetYearAsync(Book book, string year)
        {
            if (book != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    book.Year = year;
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
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
                    _db.UpdateBook(book);
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
                return _db.GetAllBooks();
            });
        }

    }
}
