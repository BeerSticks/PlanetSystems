using PlanetSystem.Models;
using System.Data.Entity;

namespace PlanetSystem.Data
{
    public class SqlServerContext :DbContext
    {
        public SqlServerContext()
            : base("SqlServerConnection")
        {
        }

        public virtual DbSet<ArtificialObject> ArtificialObjects { get; set; }

        public virtual DbSet<Asteroid> Asteroids { get; set; }

        public virtual DbSet<AstronomicalBody> AstronomicalBodies { get; set; }

        public virtual DbSet<Moon> Moon { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        public virtual DbSet<PlanetSystem.Models.PlanetSystem> PlanetSystems { get; set; }

        public virtual DbSet<Point> Points { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Vector> Vectors { get; set; }

    }
}
