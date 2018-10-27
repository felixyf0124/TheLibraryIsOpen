using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLibraryIsOpen.Constants;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.db
{
    /*
     * TODO:
     * - Implement RegisterDeleted - done please double check
     * - Implement RegisterDirty - done please double check
     * - ? Implement RegisterClean? should we?
     * - Complete CommitAsync
     */
    public class UnitOfWork
    {
        private readonly IdentityMap _im;
        private readonly ReaderWriterLockSlim _newLock;
        private readonly ReaderWriterLockSlim _toDeleteLock;
        private readonly ReaderWriterLockSlim _dirtyLock;

        private readonly Dictionary<string, object> RegisteredNew;
        private readonly Dictionary<string, object> RegisteredDeleted;
        private readonly Dictionary<string, object> RegisteredDirty;

        public UnitOfWork(IdentityMap im)
        {
            _im = im;
            _newLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _toDeleteLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _dirtyLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);


            RegisteredNew = new Dictionary<string, object>();
            RegisteredDeleted = new Dictionary<string, object>();
            RegisteredDirty = new Dictionary<string, object>();
        }

        public bool RegisterNew(object o)
        {
            bool succeeded = false;
            switch (GetTypeNum(o.GetType()))
            {
                case TypeEnum.Book:
                    {
                        Book temp = (Book)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Book}-{(temp.BookId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.BookId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Magazine}-{(temp.MagazineId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.MagazineId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Movie}-{(temp.MovieId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.MovieId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Music}-{(temp.MusicId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.MusicId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredNew.TryAdd($"{TypeEnum.Person}-{(temp.PersonId == 0 ? RegisteredNew.Count.ToString() : $"custom{temp.PersonId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            return succeeded;
        }

        public bool RegisterDirty(object o)
        {
            bool succeeded = false;
            switch (GetTypeNum(o.GetType()))
            {
                case TypeEnum.Book:
                    {
                        Book temp = (Book)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDirty.TryAdd($"{TypeEnum.Book}-{(temp.BookId == 0 ? RegisteredDirty.Count.ToString() : $"custom{temp.BookId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDirty.TryAdd($"{TypeEnum.Magazine}-{(temp.MagazineId == 0 ? RegisteredDirty.Count.ToString() : $"custom{temp.MagazineId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDirty.TryAdd($"{TypeEnum.Movie}-{(temp.MovieId == 0 ? RegisteredDirty.Count.ToString() : $"custom{temp.MovieId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDirty.TryAdd($"{TypeEnum.Music}-{(temp.MusicId == 0 ? RegisteredDirty.Count.ToString() : $"custom{temp.MusicId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDirty.TryAdd($"{TypeEnum.Person}-{(temp.PersonId == 0 ? RegisteredDirty.Count.ToString() : $"custom{temp.PersonId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            return succeeded;
        }

        public bool RegisterDeleted(object o)
        {
            bool succeeded = false;
            switch (GetTypeNum(o.GetType()))
            {
                case TypeEnum.Book:
                    {
                        Book temp = (Book)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDeleted.TryAdd($"{TypeEnum.Book}-{temp.BookId}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDeleted.TryAdd($"{TypeEnum.Magazine}-{temp.MagazineId}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDeleted.TryAdd($"{TypeEnum.Movie}-{temp.MovieId}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDeleted.TryAdd($"{TypeEnum.Music}-{temp.MusicId}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = RegisteredDeleted.TryAdd($"{TypeEnum.Person}-{temp.PersonId}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            return succeeded;
        }


        public async Task<bool> CommitAsync()
        {
            return (
                await _im.AddAsync(RegisteredNew.ToArray())
                &&
                await _im.AddAsync(RegisteredDirty.ToArray())
                &&
                await _im.DeleteAsync(RegisteredDeleted.ToArray())                
                );
        }
    }
}
