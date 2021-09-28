using MySql.Data.EntityFramework;
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
        public DbSet<invoice_items> invoice_items { get; set; }
        public DbSet<quotation> quotation { get; set; }
        public DbSet<invoice> invoice { get; set; }
        public DbSet<token> token { get; set; }
        public DbSet<site> site { get; set; }
        public DbSet<plot> plot { get; set; }
        public DbSet<customer> customer { get; set; }
        public DbSet<payment> payment { get; set; }
        public DbSet<userStaff> userStaff { get; set; }
    }
}
