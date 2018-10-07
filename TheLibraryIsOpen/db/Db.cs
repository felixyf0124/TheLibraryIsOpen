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
            year varchar(255) 
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
         *  Magazine Table methods
         */

        public void CreateMagazine(Magazine magazine)
        {
            string query = $"INSERT INTO magazines (title, publisher, language, date, isbn10, isbn13) VALUES(\"{magazine.Title}\", \"{magazine.Publisher}\", \"{magazine.Language}\", \"{magazine.Date}\", " +
                "\"{magazine.Isbn10}\", \"{magazine.Isbn13}\");";

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

        public void UpdateMagazine(Magazine magazine)
        {
            string query = $"UPDATE magazines SET title = \"{magazine.Title}\", publisher = \"{magazine.Publisher}\", language = \"{magazine.Language}\", date = \"{magazine.Date}\", " +
                "isbn10 = \"{magazine.Isbn10}\", isbn13 = \"{magazine.Isbn13}\" WHERE magazineId = \"{magazine.MagazineId}\";";

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

        public void DeleteMagazine(Magazine magazine)
        {
            string query = $"DELETE FROM magazines WHERE (MusicId = \"{magazine.MagazineId}\");";

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

        public List<Magazine> GetAllMagazines() 
        {
            string query = $"SELECT * FROM magazines;";

            List<Magazine> magazines = new List<Magazine>();
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
                        //Read the data, create magazine object and store in list
                        while (dr.Read())
                        {
                            int magazineId = (int)dr["magazineID"];
                            string title = dr["title"] + "";
                            string publisher = dr["publisher"] + "";
                            string language = dr["language"] + "";
                            string date = dr["date"] + "";
                            string isbn10 = dr["isbn10"] + "";
                            string isbn13 = dr["isbn13"] + "";

                            magazines.Add(new Magazine(magazineId, title, publisher, language, date, isbn10, isbn13));
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return magazines;

        }

        public Magazine GetMagazineById(int id)
        {
            string query = $"SELECT * FROM magazines WHERE magazineId = \" { id } \";";

            Magazine magazine = null;
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
                        //Read the data, create magazine object and store in list
                        if (dr.Read())
                        {
                            int magazineId = (int)dr["magazineID"];
                            string title = dr["title"] + "";
                            string publisher = dr["publisher"] + "";
                            string language = dr["language"] + "";
                            string date = dr["date"] + "";
                            string isbn10 = dr["isbn10"] + "";
                            string isbn13 = dr["isbn13"] + "";

                            magazine = new Magazine(magazineId, title, publisher, language, date, isbn10, isbn13);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return magazine;
        }

        public Magazine GetMagazineByIsbn10(string Isbn10)
        {
            string query = $"SELECT * FROM magazines WHERE isbn10 = \" { Isbn10 } \";";

            Magazine magazine = null;
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
                        //Read the data, create magazine object and store in list
                        if (dr.Read())
                        {
                            int magazineId = (int)dr["magazineID"];
                            string title = dr["title"] + "";
                            string publisher = dr["publisher"] + "";
                            string language = dr["language"] + "";
                            string date = dr["date"] + "";
                            string isbn10 = dr["isbn10"] + "";
                            string isbn13 = dr["isbn13"] + "";

                            magazine = new Magazine(magazineId, title, publisher, language, date, isbn10, isbn13);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return magazine;
        }

        public Magazine GetMagazineByIsbn13(string Isbn13)
        {
            string query = $"SELECT * FROM magazines WHERE isbn13 = \" { Isbn13 } \";";

            Magazine magazine = null;
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
                        //Read the data, create magazine object and store in list
                        if (dr.Read())
                        {
                            int magazineId = (int)dr["magazineID"];
                            string title = dr["title"] + "";
                            string publisher = dr["publisher"] + "";
                            string language = dr["language"] + "";
                            string date = dr["date"] + "";
                            string isbn10 = dr["isbn10"] + "";
                            string isbn13 = dr["isbn13"] + "";

                            magazine = new Magazine(magazineId, title, publisher, language, date, isbn10, isbn13);
                        }
                    }
                    catch (Exception e) { throw e; }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();
                }
            }
            return magazine;
        }



        /*
         * The following methods are made for the music table
         */

        // Inserts a new music into the database
        public void CreateMusic(Music music)
        {
            string query = $"INSERT INTO musics (type, title, artist, label, releaseDate, asin) VALUES(\"{music.Type}\", \"{music.Title}\", \"{music.Artist}\", \"{music.Label}\", \"{music.ReleaseDate}\", \"{music.Asin}\", \"{music.MusicId});";

            // Try QuerySend when table for music will be created
            //QuerySend(query);

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

        // Update a music's information in the database by MusicId
        public void UpdateMusic(Music music)
        {
            string query = $"UPDATE music SET type = \"{music.Type}\", title = \"{music.Title}\", artist = \"{music.Artist}\", label = \"{music.Label}\", releaseDate = \"{music.ReleaseDate}\", asin = \"{music.Asin}\" WHERE musicID = \"{music.MusicId}\";";

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

        // Delete music by MusicId from the database
        public void DeleteMusic(Music music)
        {
            string query = $"DELETE FROM musics WHERE (MusicId = \"{music.MusicId}\");";

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

        // Retrieve a music information by id
        public Music GetMusicById(int id)
        {
            string query = $"SELECT * FROM musics WHERE musicId = \" { id } \";";

            // Try QueryRetrieve when music table will be created
            //return QueryRetrieve(query);

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
                        //Read the data, create client object and store in list
                        if (dr.Read())
                        {
                            int musicId = (int)dr["musicId"];
                            string type = dr["type"] + "";
                            string title = dr["title"] + "";
                            string artist = dr["artist"] + "";
                            string label = dr["label"] + "";
                            string releaseDate = dr["releaseDate"] + "";
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

        // Retrieve a music information by ISBN
        public Music GetMusicByIsbn(int ISBN)
        {
            string query = $"SELECT * FROM musics WHERE isbn = \" { ISBN } \";";
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
                        //Read the data, create client object and store in list
                        if (dr.Read())
                        {
                            int musicId = (int)dr["musicId"];
                            string type = dr["type"] + "";
                            string title = dr["title"] + "";
                            string artist = dr["artist"] + "";
                            string label = dr["label"] + "";
                            string releaseDate = dr["releaseDate"] + "";
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


        /*
         * TO BE TESTED
         * For all types of tables
         * Method to send query to database for creating, updating and deleting
         */
        public void QuerySend (string query) 
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
         * TO BE TESTED
         * For only music table
         * Method to retrieve music information by id or ISBN
         */
        public Music QueryRetrieve (string query)
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
                        //Read the data, create client object and store in list
                        if (dr.Read())
                        {
                            int musicId = (int)dr["musicId"];
                            string type = dr["type"] + "";
                            string title = dr["title"] + "";
                            string artist = dr["artist"] + "";
                            string label = dr["label"] + "";
                            string releaseDate = dr["releaseDate"] + "";
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


        // Returns a list of all clients in the db converted to client object.
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
                            string year = dr["year"] + "";
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

    }
}