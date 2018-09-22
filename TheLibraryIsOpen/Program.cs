using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TheLibraryIsOpen
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}

    public class Client
    {
        private static int clientId = 0;
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string EmailAddress { get; set; }
        private string HomeAddress { get; set; }
        private int PhoneNo { get; set; }
        private string Password { get; set; }
        private bool isAdmin { get; set; }

        public Client (string firstName, string lastName, string emailAddress, string homeAddress, int phoneNo, string password)
        {
            clientId++;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.HomeAddress = homeAddress;
            this.PhoneNo = phoneNo;
            this.Password = password;
            isAdmin = false;
        }

     
        public override string ToString()
        {
            return "Client:\nFirst Name:" + FirstName + "Last Name:" + LastName +"\nID: " + clientId + "\nEmail Address:" + EmailAddress + "\nHome Address:" + HomeAddress + "\nPhone No:" + PhoneNo;
        }
    }
}
