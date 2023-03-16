﻿using LibraryAPI.Entities;
using System.Reflection.Metadata.Ecma335;

namespace LibraryAPI
{
    public class LibrarySeeder
    {
        private readonly LibraryContext _dbContext;

        public LibrarySeeder(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Email = "cresixu@gmail.com",
                    Password = "password",
                    Address = new Address()
                    {
                        City = "Example", Street = "Example", Number = "Example", PostalCode = "Example"
                    },
                    RoleId = 1
                }
            };
            return users;
        }
        
    }
}