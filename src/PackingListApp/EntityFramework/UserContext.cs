﻿//using Microsoft.EntityFrameworkCore;
//using PackingListApp.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PackingListApp.EntityFramework
//{
//    public class UserContext : DbContext
//    {
//        private bool _initialized;

//        public UserContext(DbContextOptions<UserContext> options) : base(options)
//        {
//            if (!_initialized)
//            {
//                if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
//                {
//                    // Use Entity Framework migrations
//                    Database.Migrate();
//                }
//                _initialized = true;
//            }
//        }
//        public DbSet<UserModel> UserModels { get; set; }
//    }
//}
