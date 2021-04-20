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
    }
}
