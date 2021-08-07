﻿using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Data.Common;
using models.DatabaseTable;

namespace services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDb : DbContext
    {

        public AppDb() : base("name=DefaultConnection")
        {
        }

        public AppDb(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {

        }

        public static AppDb Create()
        {
            return new AppDb();
        }

        public DbSet<user> users { get; set; }

        public DbSet<template> template { get; set; }
        public DbSet<product> product { get; set; }
        public DbSet<contact> contact { get; set; }
        public DbSet<company> company { get; set; }
        public DbSet<quotation_items> quotation_items { get; set; }
        public DbSet<quotation> quotation { get; set; }
        public DbSet<invoice> invoice { get; set; }
        public DbSet<token> token { get; set; }
    }
}
