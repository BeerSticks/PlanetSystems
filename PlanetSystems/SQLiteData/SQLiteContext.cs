using SQLiteData.Migrations;
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
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLiteContext,SQLiteConfig>(true));
        }

        public virtual DbSet<Satellite> Satellites { get; set; }

        public virtual DbSet<SatelliteType> SatelliteTypes { get; set; }

    }
}
