using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetSystem.Models.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetSystem.Models.Bodies
{
    public partial class Asteroid : AstronomicalBody
    {
        // Constructors
        public Asteroid(Point center, double mass, double radius, Vector velocity, string name)
            : base(center, mass, radius, velocity, name)
        {
        }

        public Asteroid(Point center, double mass, double radius, string name)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)), name)
        {
        }

        public Asteroid(Asteroid asteroid)
            : this(asteroid.Center, asteroid.Mass, asteroid.Radius, asteroid.Velocity, asteroid.Name)
        {
        }

        // Required from Entity Framework
        public Asteroid() { }

        // Properties
        [Key]
        public int AsteroidId { get; set; }
        public int? PlanetarySystemId { get; set; }

        [ForeignKey("PlanetarySystemId")]
        public virtual PlanetarySystem PlanetarySystem { get; set; }
    }
}
