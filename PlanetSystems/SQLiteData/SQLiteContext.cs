using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteData
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext()
            : base("SQLiteConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLiteContext, Migrations.SQLiteConfig>(true));
        }

        public virtual DbSet<TestClass> Tests { get; set; }
    }
}
