/*
    The DdQuery class contains methods for performing sql queries on the db.
    Inspired from https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
 */
using MySql.Data.MySqlClient;
using TheLibraryIsOpen;

namespace Database
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
            uid = "library343";
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
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        // TODO show message cannot connect to server
                        break;

                    case 1045:
                        // TODO show message invalid user/pass
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // TODO MessageBox.Show(ex);
                return false;
            }
        }

        // This method opens connection, runs query and closes connection
        private void query(string query)
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            } else {
                System.Console.WriteLine("db does not connect");
            }
        }

        // Methods related to different queries
        // change signature to include client
        public void CreateClient(Client client)
        {
            // TODO generate string to call query function
        }

        public void DeleteClient(Client client)
        {
            // TODO generate string to call query function
        }

        public void ModifiyClient(Client client)
        {
            // TODO generate string to call query function
        }
    }
}