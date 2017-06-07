using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetSystem.Models.Utilities;

namespace PlanetSystem.Models.Bodies
{
    public partial class Asteroid : AstronomicalBody
    {
        // Constructors
        public Asteroid(Point center, double mass, double radius, Vector velocity)
            : base(center, mass, radius, velocity)
        {
        }

        public Asteroid(Point center, double mass, double radius)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)))
        {
        }

        public Asteroid(Asteroid asteroid)
            : this(asteroid.Center, asteroid.Mass, asteroid.Radius, asteroid.Velocity)
        {
        }

        // Properties
        public int? PlanetarySystemId { get; set; }
        public virtual PlanetarySystem PlanetarySystem { get; set; }

    }
}
