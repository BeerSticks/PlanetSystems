using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLData
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext()
            : base("PostgresDotNet")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<OwnPlanetarySystem> OwnPlanetarySystems { get; set; }
    }
}
