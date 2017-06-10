using PlanetSystem.Models;
using System.Data.Entity;
using PlanetSystem.Models.Bodies;
using PlanetSystem.Models.Utilities;

namespace PlanetSystem.Data
{
    public class SqlServerContext :DbContext
    {
        public SqlServerContext()
            : base("PlanetSystems")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Fluent API must be implemented
        }

        //public virtual DbSet<Vector> Vectors { get; set; }

        //public virtual DbSet<Point> Points { get; set; }

        public virtual DbSet<PlanetarySystem> PlanetarySystems { get; set; }
        public virtual DbSet<Models.Bodies.AstronomicalBody> Stars { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Moon> Moons { get; set; }
        public virtual DbSet<Asteroid> Asteroids { get; set; }
        public virtual DbSet<ArtificialObject> ArtificialObjects { get; set; }
    }
}
