﻿using Microsoft.AspNet.Identity;
//using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TheLibraryIsOpen.Models.DBModels
{
    public class Client : IUser<string>
    {
        public int clientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int BorrowMax { get; set; } = 10;

        //required for implementing IUser
        public string Id { get => clientId.ToString(); set => clientId = Int32.Parse(value); }
        public string UserName { get => EmailAddress; set => EmailAddress = value; }

        // Default constructor
        public Client()
        {

        }

        /* Here the constructor assign values to attributes besides clientId.
         * The clientId is generated by database, when insert an entry to the "client" table (assumed it's already & primary key autoincrement).
         * The last id just entered from table would be assigned to clientId for the client object. So that to avoid same id appears when server gets restarted.
        */

        public Client(string firstName, string lastName, string emailAddress, string homeAddress, string phoneNo, string password, bool isAdmin = false, int borrowMax = 10)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            HomeAddress = homeAddress;
            PhoneNo = phoneNo;
            Password = password;
            IsAdmin = isAdmin;
            BorrowMax = borrowMax;
        }
        // another construcor who  assigns client id is added as requested.
        public Client(int cId, string firstName, string lastName, string emailAddress, string homeAddress, string phoneNo, string password, bool isAdmin = false, int borrowMax = 10) :
            this(firstName, lastName, emailAddress, homeAddress, phoneNo, password, isAdmin, borrowMax)
        {
            clientId = cId;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Client> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //verify if the password entered matches
        public bool PasswordVerify(string pswd)
        {
            return pswd.Equals(this.Password);
        }

        //method to allow someone to register as an admin.Since we will have admin class extends client, is this necessary? 
        public void RegisterAsAdmin()
        {
            IsAdmin = true;
        }

        public override string ToString()
        {
            return "Client:\nFirst Name:" + FirstName + "Last Name:" + LastName + "\nID: " + clientId + "\nEmail Address:" + EmailAddress + "\nHome Address:" + HomeAddress + "\nPhone No:" + PhoneNo;
        }
    }
}
