using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class BookCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;


        public BookCatalog(UnitOfWork unitOfWork, IdentityMap im)
        {
            _unitOfWork = unitOfWork;
            _im = im;
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

        public Task<IdentityResult> AddModelCopy(string id)
        {
            return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register returns false

                    _unitOfWork.RegisterNew(new ModelCopy
                    {
                        modelID = Int32.Parse(id),
                        modelType = TypeEnum.Book
                    });
                    return IdentityResult.Success;
                });
        }

        //Get all Books
        public Task<List<Book>> GetAllBookDataAsync()
        {
            return _im.GetAllBooks();
        }

        public Task<bool> CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public Task<int> GetNoOfAvailableModelCopies(Book book)
        {

            return _im.CountModelCopiesOfModel(book.BookId, (int)TypeEnum.Book, BorrowType.NotBorrowed);


        }

        public async Task<IdentityResult> DeleteFreeModelCopy(string id)
        {
            ModelCopy temp = new ModelCopy
            {
                modelID = Int32.Parse(id),
                modelType = TypeEnum.Book
            };
            await _im.DeleteFreeModelCopy(temp, Int32.Parse(id));
            return IdentityResult.Success;

        }
    }
}
