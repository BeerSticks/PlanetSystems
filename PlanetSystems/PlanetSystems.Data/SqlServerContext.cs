using System.Data.Entity;

namespace PlanetSystems.Data
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext()
            :base("SqlServerConnection")
        {
        }


    }
}
