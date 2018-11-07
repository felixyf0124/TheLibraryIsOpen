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

        //EditAsync method

        public Task<bool> EditAsync(params object[] objectsToEdit)
        {
            return Task.Factory.StartNew(() =>
            {
                List<Book> books = new List<Book>();
                List<Magazine> mags = new List<Magazine>();
                List<Movie> movies = new List<Movie>();
                List<Music> music = new List<Music>();
                List<Person> people = new List<Person>();
                foreach (var item in objectsToEdit)
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
                    {
                        books.ForEach(temp =>
                        {
                            while (!_bookLock.TryEnterReadLock(10)) ;
                            bool hasBook = _books.ContainsKey(temp.BookId);
                            _bookLock.ExitReadLock();

                            while (!_bookLock.TryEnterWriteLock(10)) ;
                            if (!hasBook)
                                _books.Add(temp.BookId, temp);
                            else
                                _books[temp.BookId] = temp;
                            _bookLock.ExitWriteLock();
                        });

                        _db.UpdateBooks(books.ToArray());
                    }
                    if (mags.Count > 0)
                    {
                        mags.ForEach(temp =>
                        {
                            while (!_magLock.TryEnterReadLock(10)) ;
                            bool hasMag = _mags.ContainsKey(temp.MagazineId);
                            _bookLock.ExitReadLock();

                            while (!_magLock.TryEnterWriteLock(10)) ;
                            if (!hasMag)
                                _mags.Add(temp.MagazineId, temp);
                            else
                                _mags[temp.MagazineId] = temp;
                            _magLock.ExitWriteLock();
                        });

                        _db.UpdateMagazines(mags.ToArray());
                    }

                    if (movies.Count > 0)
                    {
                        movies.ForEach(movie =>
                        {
                            var prevMovie = FindMovie(movie.MovieId);
                            if (prevMovie.Actors == null)
                                prevMovie.Actors = _db.GetAllMovieActors(movie.MovieId);
                            if (prevMovie.Producers == null)
                                prevMovie.Producers = _db.GetAllMovieProducers(movie.MovieId);

                            var actorsToAdd = movie.Actors?
                                                    .Select(a => a.PersonId)?
                                                    .Except(prevMovie.Actors?
                                                        .Select(a => a.PersonId) ?? new List<int>())
                                                   .ToArray()
                                                    ?? new int[0];

                            _db.CreateMovieActors(movie.MovieId, actorsToAdd);


                            var actorsToRemove = prevMovie.Actors?
                                                    .Select(a => a.PersonId)?
                                                    .Except(movie.Actors?
                                                        .Select(a => a.PersonId) ?? new List<int>())
                                                    .ToArray()
                                                    ?? new int[0];

                            _db.DeleteMovieActors(movie.MovieId, actorsToRemove);

                            var producersToAdd = movie.Producers?
                                                        .Select(a => a.PersonId)?
                                                        .Except(prevMovie.Producers?
                                                            .Select(p => p.PersonId) ?? new List<int>())
                                                      .ToArray()
                                                        ?? new int[0];

                            _db.CreateMovieProducers(movie.MovieId, producersToAdd);


                            var producersToRemove = prevMovie.Producers?
                                                        .Select(a => a.PersonId)?
                                                        .Except(movie.Producers?
                                                            .Select(p => p.PersonId) ?? new List<int>())
                                                       .ToArray()
                                                        ?? new int[0];

                            _db.DeleteMovieProducers(movie.MovieId, producersToRemove);


                            while (!_movieLock.TryEnterReadLock(10)) ;
                            bool hasMovie = _movies.ContainsKey(movie.MovieId);
                            _movieLock.ExitReadLock();

                            while (!_movieLock.TryEnterWriteLock(10)) ;
                            if (!hasMovie)
                                _movies.Add(movie.MovieId, movie);
                            else
                                _movies[movie.MovieId] = movie;
                            _movieLock.ExitWriteLock();
                        });

                        _db.UpdateMovies(movies.ToArray());
                    }
                    if (music.Count > 0)
                    {
                        music.ForEach(temp =>
                        {
                            while (!_musicLock.TryEnterReadLock(10)) ;
                            bool hasMusic = _music.ContainsKey(temp.MusicId);
                            _musicLock.ExitReadLock();

                            while (!_musicLock.TryEnterWriteLock(10)) ;
                            if (!hasMusic)
                                _music.Add(temp.MusicId, temp);
                            else
                                _music[temp.MusicId] = temp;
                            _musicLock.ExitWriteLock();
                        });

                        _db.UpdateMusic(music.ToArray());
                    }
                    if (people.Count > 0)
                    {
                        people.ForEach(temp =>
                        {
                            while (!_peopleLock.TryEnterReadLock(10)) ;
                            bool hasPerson = _people.ContainsKey(temp.PersonId);
                            _peopleLock.ExitReadLock();

                            while (!_peopleLock.TryEnterWriteLock(10)) ;
                            if (!hasPerson)
                                _people.Add(temp.PersonId, temp);
                            else
                                _people[temp.PersonId] = temp;
                            _peopleLock.ExitWriteLock();
                        });

                        _db.UpdatePeople(people.ToArray());
                    }
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

        public Movie FindMovie(int movieId)
        {
            Movie movieToFind;

            while (!_movieLock.TryEnterReadLock(10)) ;
            _movies.TryGetValue(movieId, out movieToFind);
            _movieLock.ExitReadLock();

            if (movieToFind == null)
            {
                movieToFind = _db.GetMovieById(movieId);

                if (movieToFind != null)
                {
                    while (!_movieLock.TryEnterWriteLock(10)) ;
                    _movies.TryAdd(movieId, movieToFind);
                    _movieLock.ExitWriteLock();

                }
            }

            return movieToFind;
        }

        public Music FindMusic(int musicId)
        {
            Music musicToFind;

            while (!_musicLock.TryEnterReadLock(10)) ;
            _music.TryGetValue(musicId, out musicToFind);
            _musicLock.ExitReadLock();

            if (musicToFind == null)
            {
                musicToFind = _db.GetMusicById(musicId);

                if (musicToFind != null)
                {
                    while (!_musicLock.TryEnterWriteLock(10)) ;
                    _music.TryAdd(musicId, musicToFind);
                    _musicLock.ExitWriteLock();

                }
            }

            return musicToFind;
        }

        public Person FindPerson(int personId)
        {
            Person personToFind;

            while (!_peopleLock.TryEnterReadLock(10)) ;
            _people.TryGetValue(personId, out personToFind);
            _peopleLock.ExitReadLock();

            if (personToFind == null)
            {
                personToFind = _db.GetPersonById(personId);

                if (personToFind != null)
                {
                    while (!_peopleLock.TryEnterWriteLock(10)) ;
                    _people.TryAdd(personId, personToFind);
                    _peopleLock.ExitWriteLock();

                }
            }

            return personToFind;
        }

        public Magazine FindMagazine(int magazineID)
        {
            Magazine magazineToFind;
            while (!_magLock.TryEnterReadLock(10)) ;
            _mags.TryGetValue(magazineID, out magazineToFind);
            _magLock.ExitReadLock();
            if (magazineToFind == null)
            {
                magazineToFind = _db.GetMagazineById(magazineID);
                if (magazineToFind != null)
                {
                    while (!_magLock.TryEnterWriteLock(10)) ;
                    _mags.TryAdd(magazineID, magazineToFind);
                    _magLock.ExitWriteLock();
                }

            }
            return magazineToFind;
        }

        public Book FindBook(int bookID)
        {
            Book bookToFind;
            while (!_bookLock.TryEnterReadLock(10)) ;
            _books.TryGetValue(bookID, out bookToFind);
            _bookLock.ExitReadLock();
            if (bookToFind == null)
            {
                bookToFind = _db.GetBookById(bookID);
                if (bookToFind != null)
                {
                    while (!_bookLock.TryEnterWriteLock(10)) ;
                    _books.TryAdd(bookID, bookToFind);
                    _bookLock.ExitWriteLock();
                }

            }
            return bookToFind;
        }

        // TODO: BOOK find by isbn 13, isbn 10 and GetAllBooks

        // TODO: MAGAZINE find by isbn 13, isbn 10 and GetAllMagazines

        // TODO: MOVIE GetAllMovies, getAllPerson, GetAllMovieProducers, GetAllMovieActors

        // TODO: MUSIC GetAllMusic
    }
}
