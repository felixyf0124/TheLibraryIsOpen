/*
    The Db class contains methods for performing sql queries on the db.
    Inspired from https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    The database table looks like so:
    Table:      users
    Columns:    clientID int(12) 
                firstName varchar(255) 
                lastName varchar(255) 
                emailAddress varchar(255) 
                homeAddress varchar(255) 
                phoneNumber varchar(255) 
                password varchar(255) 
                isAdmin tinyint(1)

    Table: books
    Columns:
            bookID int(11) AI PK 
            title varchar(255) 
            author varchar(255) 
            format varchar(255) 
            pages int(11) 
            publisher varchar(255) 
            date varchar(255) 
            language varchar(255) 
            isbn10 varchar(255) 
            isbn13 varchar(255)

    Table: magazines
    Columns:
            magazineID int(11) AI PK 
            title varchar(255) 
            publisher varchar(255) 
            language varchar(255) 
            date varchar(255) 
            isbn10 varchar(255) 
            isbn13 varchar(255)

    Table: movies
    Columns:
            movieID int(11) AI PK 
            title varchar(255) 
            director varchar(255) 
            producers varchar(255) 
            actors varchar(255) 
            language varchar(255) 
            subtitles varchar(255) 
            dubbed varchar(255) 
            releasedate varchar(255) 
            runtime varchar(255)

    Table: cds
    Columns:
            cdID int(11) AI PK 
            type varchar(255) 
            title varchar(255) 
            artist varchar(255) 
            label varchar(255) 
            releasedate varchar(255) 
            asin varchar(255)   

    Table: person
    Columns:
            personID int(11) AI PK 
            firstname varchar(255) 
            lastname varchar(255) 
            role varchar(255)

    Table: movieperson
    Columns:
            movieid int(11) 
            personid int(11)
 */
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Database
{
    public class Db
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Db()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "35.236.241.114";
            database = "library";
            uid = "root";
            password = "library343";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            catch (MySqlException e) { throw e; }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e) { throw e; }
        }

        // Returns a list of all clients in the db converted to client object.
        public List<Client> GetAllClients()
        {
            //Create a list of unknown size to store the result
            List<Client> list = new List<Client>();
            string query = "SELECT * FROM users;";

            lock (this)
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create client object and store in list
                        while (dr.Read())
                        {
                            int clientID = (int)dr["clientID"];
                            string firstName = dr["firstName"] + "";
                            string lastName = dr["lastName"] + "";
                            string emailAddress = dr["emailAddress"] + "";
                            string homeAddress = dr["homeAddress"] + "";
                            string phoneNumber = dr["phoneNumber"] + "";
                            string password = dr["password"] + "";
                            bool isAdmin = (bool)dr["isAdmin"];

                            Client client = new Client(clientID, firstName, lastName, emailAddress, homeAddress, phoneNumber, password, isAdmin);

                            list.Add(client);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return list;
        }

        // Selects a client by id and returns a client object.
        public Client GetClientById(int id)
        {
            string query = $"SELECT * FROM users WHERE clientID = \"{id}\";";
            Client client = null;

            lock (this)
            {
                //Open connection
                if (OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    try
                    {
                        //Read the data, create client object and store in list
                        if (dr.Read())
                        {
                            int clientID = (int)dr["clientID"];
                            string firstName = dr["firstName"] + "";
                            string lastName = dr["lastName"] + "";
                            string emailAddress = dr["emailAddress"] + "";
                            string homeAddress = dr["homeAddress"] + "";
                            string phoneNumber = dr["phoneNumber"] + "";
                            string password = dr["password"] + "";
                            bool isAdmin = (bool)dr["isAdmin"];

                            client = new Client(clientID, firstName, lastName, emailAddress, homeAddress, phoneNumber, password, isAdmin);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return client;
        }

        // Selects a client by email and returns a client object
        public Client GetClientByEmail(string emailAddres)
        {
            string query = $"SELECT * FROM users WHERE emailAddress = \"{emailAddres}\";";
            Client client = null;

            lock (this)
            {
                //Open connection
                if (OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create client object and store in list
                        if (dr.Read())
                        {
                            int clientID = (int)dr["clientID"];
                            string firstName = dr["firstName"] + "";
                            string lastName = dr["lastName"] + "";
                            string emailAddress = dr["emailAddress"] + "";
                            string homeAddress = dr["homeAddress"] + "";
                            string phoneNumber = dr["phoneNumber"] + "";
                            string password = dr["password"] + "";
                            bool isAdmin = (bool)dr["isAdmin"];

                            client = new Client(clientID, firstName, lastName, emailAddress, homeAddress, phoneNumber, password, isAdmin);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return client;
        }


        // Inserts a new client into the db
        public void CreateClient(Client client)
        {

            string query = $"INSERT INTO users (firstName, lastName, emailAddress, homeAddress, phoneNumber, password, isAdmin) VALUES(\"{client.FirstName}\", \"{client.LastName}\", \"{client.EmailAddress}\", \"{client.HomeAddress}\", \"{client.PhoneNo}\", \"{client.Password}\", {client.IsAdmin});";

            lock (this)
            {
                //open connection
                if (this.OpenConnection() == true)
                {
                    try
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e) { throw e; }

                    //close connection
                    this.CloseConnection();
                }
            }
        }

        // Deletes a client by id from the db
        public void DeleteClient(Client client)
        {
            string query = $"DELETE FROM users WHERE (clientID = \"{client.clientId}\");";

            lock (this)
            {
                //open connection
                if (this.OpenConnection() == true)
                {
                    try
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e) { throw e; }

                    //close connection
                    this.CloseConnection();
                }
            }
        }

        // Updates a client's information in the db by id
        public void UpdateClient(Client client)
        {
            string query = $"UPDATE users SET firstName = \"{client.FirstName}\", lastName = \"{client.LastName}\", emailAddress = \"{client.EmailAddress}\", homeAddress = \"{client.HomeAddress}\", phoneNumber = \"{client.PhoneNo}\", password = \"{client.Password}\", isAdmin = {client.IsAdmin} WHERE clientID = \"{client.clientId}\";";

            lock (this)
            {
                //open connection
                if (this.OpenConnection() == true)
                {
                    try
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e) { throw e; }

                    //close connection
                    this.CloseConnection();
                }
            }
        }


        /*
         * For all types of tables
         * Method to send query to database for creating, updating and deleting
         */
        public void QuerySend(string query)
        {
            lock (this)
            {
                //open connection
                if (this.OpenConnection() == true)
                {
                    try
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e) { throw e; }

                    //close connection
                    this.CloseConnection();
                }
            }
        }

        /*
         * The following methods are made for the music table
         */

        // Inserts a new music into the database
        public void CreateMusic(Music music)
        {
            string query = $"INSERT INTO cds (type, title, artist, label, releasedate, asin) VALUES(\"{music.Type}\", \"{music.Title}\", \"{music.Artist}\", \"{music.Label}\", \"{music.ReleaseDate}\", \"{music.Asin}\");";
            QuerySend(query);
        }

        // Update a music's information in the database by MusicId
        public void UpdateMusic(Music music)
        {
            string query = $"UPDATE cds SET type = \"{music.Type}\", title = \"{music.Title}\", artist = \"{music.Artist}\", label = \"{music.Label}\", releasedate = \"{music.ReleaseDate}\", asin = \"{music.Asin}\" WHERE (cdID = \"{music.MusicId}\");";
            QuerySend(query);
        }

        // Delete music by MusicId from the database
        public void DeleteMusic(Music music)
        {
            string query = $"DELETE FROM cds WHERE (cdID = \"{music.MusicId}\");";
            QuerySend(query);
        }

        // Retrieve a music information by id
        public Music GetMusicById(int id)
        {
            string query = $"SELECT * FROM cds WHERE cdID = \" { id } \";";
            return QueryRetrieveMusic(query);
        }

        // Retrieve a music information by ISBN
        public Music GetMusicByAsin(string ASIN)
        {
            string query = $"SELECT * FROM cds WHERE (asin = \"{ ASIN }\");";
            return QueryRetrieveMusic(query);
        }


        /*
         * For retrieving ONE object ONLY
         * Method to retrieve music information by id or asin 
         */
        public Music QueryRetrieveMusic(string query)
        {
            Music music = null;

            lock (this)
            {
                //Open connection
                if (OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create music object and store in list
                        if (dr.Read())
                        {
                            int musicId = (int)dr["cdID"];
                            string type = dr["type"] + "";
                            string title = dr["title"] + "";
                            string artist = dr["artist"] + "";
                            string label = dr["label"] + "";
                            string releaseDate = dr["releasedate"] + "";
                            string asin = dr["asin"] + "";

                            music = new Music(musicId, type, title, artist, label, releaseDate, asin);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return music;
        }

        // Returns a list of all musics in the db converted to music object.
        public List<Music> GetAllMusics()
        {
            //Create a list of unknown size to store the result
            List<Music> list = new List<Music>();
            Music music = null;
            string query = "SELECT * FROM cds;";

            lock (this)
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create music object and store in list
                        while (dr.Read())
                        {
                            int musicId = (int)dr["cdID"];
                            string type = dr["type"] + "";
                            string title = dr["title"] + "";
                            string artist = dr["artist"] + "";
                            string label = dr["label"] + "";
                            string releaseDate = dr["releasedate"] + "";
                            string asin = dr["asin"] + "";

                            music = new Music(musicId, type, title, artist, label, releaseDate, asin);
                            list.Add(music);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return list;
        }

        /*
         * The following methods are made for the movie table
         */

        // Inserts a new movie into the database
        public void CreateMovie(Movie movie)
        {
            string query = $"INSERT INTO movies (title, language, subtitles, dubbed, releasedate, runtime) VALUES(\"{movie.Title}\", \"{ movie.Language}\", \"{movie.Subtitles}\", \"{movie.Dubbed}\", \"{movie.ReleaseDate}\", \"{movie.RunTime}\");";
            QuerySend(query);
        }

        // Update a movie's information in the database by MusicId
        public void UpdateMovie(Movie movie)
        {
            string query = $"UPDATE movies SET title = \"{movie.Title}\", language = \"{movie.Language}\", subtitles = \"{movie.Subtitles}\", dubbed = \"{movie.Dubbed}\", releasedate = \"{movie.ReleaseDate}\", runtime = \"{movie.RunTime}\" WHERE (movieID = \"{movie.MovieId}\");";
            QuerySend(query);
        }

        // Delete movie by movieId from the database
        public void DeleteMovie(Movie movie)
        {
            string query = $"DELETE FROM movies WHERE (movieID = \"{movie.MovieId}\");";
            QuerySend(query);
        }

        // Retrieve a movie information by id
        public Movie GetMovieById(int id)
        {
            string query = $"SELECT * FROM movie WHERE movieID = \" { id } \";";

            Movie movie = null;

            lock (this)
            {
                //Open connection
                if (OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create music object and store in list
                        if (dr.Read())
                        {
                            int movieId = (int)dr["movieID"];
                            string title = dr["title"] + "";
                            string language = dr["language"] + "";
                            string subtitles = dr["subtitles"] + "";
                            string dubbed = dr["dubbed"] + "";
                            string releaseDate = dr["releasedate"] + "";
                            string runtime = dr["runtime"] + "";

                            movie = new Movie(movieId, title, language, subtitles, dubbed, releaseDate, runtime);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return movie;
        }

        // Returns a list of all movies in the db converted to music object.
        public List<Movie> GetAllMovies()
        {
            //Create a list of unknown size to store the result
            List<Movie> list = new List<Movie>();
            Movie movie = null;
            string query = "SELECT * FROM movies;";

            lock (this)
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create music object and store in list
                        while (dr.Read())
                        {
                            int movieId = (int)dr["movieID"];
                            string title = dr["title"] + "";
                            string language = dr["language"] + "";
                            string subtitles = dr["subtitles"] + "";
                            string dubbed = dr["dubbed"] + "";
                            string releaseDate = dr["releasedate"] + "";
                            string runtime = dr["runtime"] + "";

                            movie = new Movie(movieId, title, language, subtitles, dubbed, releaseDate, runtime);
                            list.Add(movie);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return list;
        }

        /*
         * The following methods are made for the person table
         */

        // Inserts a new movie into the database
        public void CreatePerson(Person person)
        {
            string query = $"INSERT INTO persons (firstName, lastName, role) VALUES(\"{person.FirstName}\", \"{person.LastName}\", \"{person.Role}\");";
            QuerySend(query);
        }

        // Update a movie's information in the database by MusicId
        public void UpdateMovie(Movie movie)
        {
            string query = $"UPDATE movies SET title = \"{movie.Title}\", language = \"{movie.Language}\", subtitles = \"{movie.Subtitles}\", dubbed = \"{movie.Dubbed}\", releasedate = \"{movie.ReleaseDate}\", runtime = \"{movie.RunTime}\" WHERE (movieID = \"{movie.MovieId}\");";
            QuerySend(query);
        }

        // Delete movie by movieId from the database
        public void DeleteMovie(Movie movie)
        {
            string query = $"DELETE FROM movies WHERE (movieID = \"{movie.MovieId}\");";
            QuerySend(query);
        }

        // Retrieve a movie information by id
        public Movie GetMovieById(int id)
        {
            string query = $"SELECT * FROM movie WHERE movieID = \" { id } \";";

            Movie movie = null;

            lock (this)
            {
                //Open connection
                if (OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create music object and store in list
                        if (dr.Read())
                        {
                            int movieId = (int)dr["movieID"];
                            string title = dr["title"] + "";
                            string language = dr["language"] + "";
                            string subtitles = dr["subtitles"] + "";
                            string dubbed = dr["dubbed"] + "";
                            string releaseDate = dr["releasedate"] + "";
                            string runtime = dr["runtime"] + "";

                            movie = new Movie(movieId, title, language, subtitles, dubbed, releaseDate, runtime);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return movie;
        }

        // Returns a list of all movies in the db converted to music object.
        public List<Movie> GetAllMovies()
        {
            //Create a list of unknown size to store the result
            List<Movie> list = new List<Movie>();
            Movie movie = null;
            string query = "SELECT * FROM movies;";

            lock (this)
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create music object and store in list
                        while (dr.Read())
                        {
                            int movieId = (int)dr["movieID"];
                            string title = dr["title"] + "";
                            string language = dr["language"] + "";
                            string subtitles = dr["subtitles"] + "";
                            string dubbed = dr["dubbed"] + "";
                            string releaseDate = dr["releasedate"] + "";
                            string runtime = dr["runtime"] + "";

                            movie = new Movie(movieId, title, language, subtitles, dubbed, releaseDate, runtime);
                            list.Add(movie);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return list;
        }

        // Returns a list of all books in the db converted to book object.
        public List<Book> GetAllBooks()
        {
            //Create a list of unknown size to store the result
            List<Book> list = new List<Book>();
            string query = "SELECT * FROM books;";

            lock (this)
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();
                    try
                    {
                        //Read the data, create book object and store in list
                        while (dr.Read())
                        {
                            int bookId = (int)dr["bookID"];
                            string title = dr["title"] + "";
                            string author = dr["author"] + "";
                            string format = dr["format"] + "";
                            int pages = (int)dr["pages"];
                            string publisher = dr["publisher"] + "";
                            string year = dr["date"] + "";
                            string language = dr["language"] + "";                             
                            string isbn10 = dr["isbn10"]+"";
                            string isbn13 = dr["isbn13"] + "";

                            Book book = new Book(bookId,title, author, format, pages,publisher, year, language,isbn10, isbn13);
                            //Console.Write(book);

                            list.Add(book);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return list;
        }


        // Return book get from isbn 10
        public Book GetBooksByIsbn(string isbn)
        {
            string query = $"SELECT * FROM books WHERE isbn10 = \"{isbn}\";";
            Book book = null;

            lock (this)
            {
                //Open connection
                if (OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    try
                    {
                        //Read the data, create client object and store in list
                        if (dr.Read())
                        {
                            int bookId = (int)dr["bookID"];
                            string title = dr["title"] + "";
                            string author = dr["author"] + "";
                            string format = dr["format"] + "";
                            int pages = (int)dr["pages"];
                            string publisher = dr["publisher"] + "";
                            string year = dr["date"] + "";
                            string language = dr["language"] + "";
                            string isbn10 = dr["isbn10"] + "";
                            string isbn13 = dr["isbn13"] + "";

                            book = new Book(bookId, title, author, format, pages, publisher, year, language, isbn10, isbn13);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return book;
        }


        // Inserts a new book into the db
        public void CreateBook(Book book)
        {

            string query = $"INSERT INTO books (title, author, format, pages, publisher, date, language, isbn10, isbn13) VALUES(\"{book.Title}\", \"{book.Author}\", \"{book.Format}\", \"{book.Pages}\", \"{book.Publisher}\", \"{book.Date}\", \"{book.Language}\",\"{book.Isbn10}\",\"{book.Isbn13}\")";
            
            QuerySend(query);
        }

        // Update a book information in the database by book ID
        // We can add other function to update book
        public void UpdateBookByBookId(Book book, int bookId)
        {
            string query = $"UPDATE books SET title = \"{book.Title}\", author = \"{book.Author}\", format = \"{book.Format}\", pages = \"{book.Pages}\", publisher = \"{book.Publisher}\", date = \"{book.Date}\", language = \"{book.Language}\", isbn10 = \"{book.Isbn10}\", isbn13 = \"{book.Isbn13}\" WHERE (bookID = \"{bookId}\");";

            QuerySend(query);
        }

        // Update a book information in the database by isbn10
        public void UpdateBookByBookIsbn(Book book, string isbn10)
        {
            string query = $"UPDATE books SET title = \"{book.Title}\", author = \"{book.Author}\", format = \"{book.Format}\", pages = \"{book.Pages}\", publisher = \"{book.Publisher}\", date = \"{book.Date}\", language = \"{book.Language}\", isbn13 = \"{book.Isbn13}\" WHERE (isbn10 = \"{isbn10}\");";

            QuerySend(query);
        }

        // Delete a book information in db by isbn10
        public void DeleteBookByIsbn10(string isbn10)
        {
            string query = $"DELETE FROM books WHERE (isbn10 = \"{isbn10}\");";

            QuerySend(query);
        }
    }
}