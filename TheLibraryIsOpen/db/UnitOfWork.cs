using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        private readonly Dictionary<string, object> _registeredNew;
        private readonly Dictionary<string, object> _registeredDeleted;
        private readonly Dictionary<string, object> _registeredDirty;

        public UnitOfWork(IdentityMap im)
        {
            _im = im;
            _newLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _toDeleteLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _dirtyLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);


            _registeredNew = new Dictionary<string, object>();
            _registeredDeleted = new Dictionary<string, object>();
            _registeredDirty = new Dictionary<string, object>();
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
                        succeeded = _registeredNew.TryAdd($"{TypeEnum.Book}-{(temp.BookId == 0 ? _registeredNew.Count.ToString() : $"custom{temp.BookId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredNew.TryAdd($"{TypeEnum.Magazine}-{(temp.MagazineId == 0 ? _registeredNew.Count.ToString() : $"custom{temp.MagazineId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredNew.TryAdd($"{TypeEnum.Movie}-{(temp.MovieId == 0 ? _registeredNew.Count.ToString() : $"custom{temp.MovieId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredNew.TryAdd($"{TypeEnum.Music}-{(temp.MusicId == 0 ? _registeredNew.Count.ToString() : $"custom{temp.MusicId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredNew.TryAdd($"{TypeEnum.Person}-{(temp.PersonId == 0 ? _registeredNew.Count.ToString() : $"custom{temp.PersonId}")}", o);
                        _newLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.ModelCopy:
                    {
                        ModelCopy temp = (ModelCopy)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredNew.TryAdd($"{TypeEnum.ModelCopy}-{(temp.id == 0 ? _registeredNew.Count.ToString() : $"custom{temp.id}")}", o);
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
                        while (!_dirtyLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDirty.TryAdd($"{TypeEnum.Book}-{temp.BookId}", o);
                        _dirtyLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        while (!_dirtyLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDirty.TryAdd($"{TypeEnum.Magazine}-{temp.MagazineId}", o); ;
                        _dirtyLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        while (!_dirtyLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDirty.TryAdd($"{TypeEnum.Movie}-{temp.MovieId}", o); ;
                        _dirtyLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        while (!_dirtyLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDirty.TryAdd($"{TypeEnum.Music}-{temp.MusicId}", o);
                        _dirtyLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        while (!_dirtyLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDirty.TryAdd($"{TypeEnum.Person}-{temp.PersonId}", o);
                        _dirtyLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.ModelCopy:
                    {
                        ModelCopy temp = (ModelCopy)o;
                        while (!_dirtyLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDirty.TryAdd($"{TypeEnum.ModelCopy}-{temp.id}", o);
                        _dirtyLock.ExitWriteLock();
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
                        while (!_toDeleteLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDeleted.TryAdd($"{TypeEnum.Book}-{temp.BookId}", o);
                        _toDeleteLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Magazine:
                    {
                        Magazine temp = (Magazine)o;
                        while (!_toDeleteLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDeleted.TryAdd($"{TypeEnum.Magazine}-{temp.MagazineId}", o);
                        _toDeleteLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Movie:
                    {
                        Movie temp = (Movie)o;
                        while (!_toDeleteLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDeleted.TryAdd($"{TypeEnum.Movie}-{temp.MovieId}", o);
                        _toDeleteLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Music:
                    {
                        Music temp = (Music)o;
                        while (!_toDeleteLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDeleted.TryAdd($"{TypeEnum.Music}-{temp.MusicId}", o);
                        _toDeleteLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.Person:
                    {
                        Person temp = (Person)o;
                        while (!_toDeleteLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDeleted.TryAdd($"{TypeEnum.Person}-{temp.PersonId}", o);
                        _toDeleteLock.ExitWriteLock();
                        break;
                    }
                case TypeEnum.ModelCopy:
                    {
                        ModelCopy temp = (ModelCopy)o;
                        while (!_newLock.TryEnterWriteLock(10)) ;
                        succeeded = _registeredDeleted.TryAdd($"{TypeEnum.ModelCopy}-{temp.id}", o);
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
                await _im.AddAsync(_registeredNew.Values.ToArray())
                &&
                await _im.EditAsync(_registeredDirty.Values.ToArray())
                &&
                await _im.DeleteAsync(_registeredDeleted.Values.ToArray())
                );
        }
    }
}
