using System.Collections.Generic;
using PlanetSystem.Models.Utilities;

namespace PlanetSystem.Models.Bodies
{
    public class Star : AstronomicalBody
    {
        // Constructors
        public Star(Point center, double mass, double radius, Vector velocity)
            : base(center, mass, radius, velocity)
        {
        }

        public Star(Point center, double mass, double radius)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)))
        {
        }

        public Star(Star star)
            : this(star.Center, star.Mass, star.Radius, star.Velocity)
        {
        }

        // Properties
        public int? PlanetarySystemId { get; set; }
        public PlanetarySystem PlanetarySystem { get; set; }
        public ICollection<Planet> Planets { get; set; }


    }
}
