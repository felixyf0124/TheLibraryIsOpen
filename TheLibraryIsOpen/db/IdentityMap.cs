using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLibraryIsOpen.Constants;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.db
{
    public class IdentityMap
    {
        
        private readonly Db _db;
        private readonly ReaderWriterLockSlim _bookLock;
        private readonly ReaderWriterLockSlim _magLock;
        private readonly ReaderWriterLockSlim _movieLock;
        private readonly ReaderWriterLockSlim _musicLock;
        private readonly ReaderWriterLockSlim _peopleLock;

        private readonly Dictionary<int, Book> _books;
        private readonly Dictionary<int, Magazine> _mags;
        private readonly Dictionary<int, Movie> _movies;
        private readonly Dictionary<int, Music> _music;
        private readonly Dictionary<int, Person> _people;

        public IdentityMap(Db db)
        {
            _db = db;
            _bookLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _magLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _movieLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _musicLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _peopleLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

            _books = new Dictionary<int, Book>();
            _mags = new Dictionary<int, Magazine>();
            _movies = new Dictionary<int, Movie>();
            _music = new Dictionary<int, Music>();
            _people = new Dictionary<int, Person>();
        }

        public Task<bool> AddAsync(params object[] objectsToAdd)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Book> books = new List<Book>();
                List<Magazine> mags = new List<Magazine>();
                List<Movie> movies = new List<Movie>();
                List<Music> music = new List<Music>();
                List<Person> people = new List<Person>();
                foreach (var item in objectsToAdd)
                {
                    switch (GetTypeNum(item.GetType()))
                    {
                        case TypeEnum.Book:
                            {
                                books.Add((Book)item);
                                break;
                            }
                        case TypeEnum.Magazine:
                            {
                                mags.Add((Magazine)item);
                                break;
                            }
                        case TypeEnum.Movie:
                            {
                                movies.Add((Movie)item);
                                break;
                            }
                        case TypeEnum.Music:
                            {
                                music.Add((Music)item);
                                break;
                            }
                        case TypeEnum.Person:
                            {
                                people.Add((Person)item);
                                break;
                            }
                        default:
                            {
                                return false;
                            }
                    }
                }
                try
                {
                    if (books.Count > 0)
                        _db.CreateBooks(books.ToArray());
                    if (mags.Count > 0)
                        _db.CreateMagazines(mags.ToArray());
                    if (movies.Count > 0)
                        _db.CreateMovies(movies.ToArray());
                    if (music.Count > 0)
                        _db.CreateMusic(music.ToArray());
                    if (people.Count > 0)
                        _db.CreatePeople(people.ToArray());
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> DeleteAsync(params object[] objectsToDelete)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Book> books = new List<Book>();
                List<Magazine> mags = new List<Magazine>();
                List<Movie> movies = new List<Movie>();
                List<Music> music = new List<Music>();
                List<Person> people = new List<Person>();
                foreach (var item in objectsToDelete)
                {
                    switch(GetTypeNum(item.GetType()))
                    {
                        case TypeEnum.Book:
                            {
                                books.Add((Book)item);
                                break;
                            }
                        case TypeEnum.Magazine:
                            {
                                mags.Add((Magazine)item);
                                break;
                            }
                        case TypeEnum.Movie:
                            {
                                movies.Add((Movie)item);
                                break;
                            }
                        case TypeEnum.Music:
                            {
                                music.Add((Music)item);
                                break;
                            }
                        case TypeEnum.Person:
                            {
                                people.Add((Person)item);
                                break;
                            }
                        default:
                            {
                                return false;
                            }
                    }
                }
                try
                {
                    if (books.Count > 0)
                    {
                        while (!_bookLock.TryEnterWriteLock(10)) ;
                        books.ForEach(temp => _books.Remove(temp.BookId));
                        _bookLock.ExitWriteLock();

                        _db.DeleteBooks(books.ToArray());
                    }
                    if (mags.Count > 0)
                    {
                        while (!_magLock.TryEnterWriteLock(10)) ;
                        mags.ForEach(temp => _mags.Remove(temp.MagazineId));
                        _magLock.ExitWriteLock();

                        _db.DeleteMagazines(mags.ToArray());
                    }
                    if (movies.Count > 0)
                    {
                        while (!_movieLock.TryEnterWriteLock(10)) ;
                        movies.ForEach(temp => _movies.Remove(temp.MovieId));
                        _movieLock.ExitWriteLock();

                        _db.DeleteMovies(movies.ToArray());
                    }
                    if (music.Count > 0)
                    {
                        while (!_musicLock.TryEnterWriteLock(10)) ;
                        music.ForEach(temp => _books.Remove(temp.MusicId));
                        _musicLock.ExitWriteLock();

                        _db.DeleteMusic(music.ToArray());
                    }
                    if (people.Count > 0)
                    {
                        while (!_peopleLock.TryEnterWriteLock(10)) ;
                        people.ForEach(temp => _books.Remove(temp.PersonId));
                        _peopleLock.ExitWriteLock();

                        _db.DeletePeople(people.ToArray());
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}
