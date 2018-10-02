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
         * The following part will be methods for music
         */

        // Inserts a new music into the database
        public void CreateMusic(Music music)
        {

        }

  
    }
}