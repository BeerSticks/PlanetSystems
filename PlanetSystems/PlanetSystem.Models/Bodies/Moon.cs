using System;
using PlanetSystem.Models.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetSystem.Models.Bodies
{
    public partial class Moon : AstronomicalBody
    {
        // Fields
        private Planet _planet;

        // Constructors
        public Moon(Point center, double mass, double radius, Vector velocity, string name)
            : base(center, mass, radius, velocity, name)
        {
        }

        public Moon(Point center, double mass, double radius, string name)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)), name)
        {
        }

        public Moon(Moon moon)
            : this(moon.Center, moon.Mass, moon.Radius, moon.Velocity, moon.Name)
        {
        }

        // Required from Entity Framework
        private Moon() { }

        // Properties
        [Key]
        public int MoonId { get; set; }

        public int? PlanetarySystemId { get; set; }
        [ForeignKey("PlanetarySystemId")]
        public virtual PlanetarySystem PlanetarySystem { get; set; }

        public int? PlanetId { get; set; }
        [ForeignKey("PlanetId")]
        public virtual Planet Planet {
            get { return this._planet; }
            set { this._planet = value; }
        }

        public bool IsAttached { get { return this.Planet == null ? false : true; } }

        // Methods
        public void DetachFromPlanet()
        {
            this._planet = null;
        }

        public void AttachToPlanet(Planet planet)
        {
            if (!IsAttached)
            {
                if (this.PlanetarySystem == planet.PlanetarySystem)
                {
                    this._planet = planet;
                    this.PlanetarySystem = planet.PlanetarySystem;
                    this.PlanetarySystemId = planet.PlanetarySystemId;
                }
                else
                {
                    throw new ArgumentException("Planetary system mismatch.");
                }
            }
            else
            {
                throw new ArgumentException($"The moon {this.Name} is already attached to a planet.");
            }
        }
    }
}
