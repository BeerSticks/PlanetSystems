using System;
using PlanetSystem.Models.Utilities;

namespace PlanetSystem.Models.Bodies
{
    public partial class Moon : AstronomicalBody
    {
        // Fields
        private Planet _planet;
        private bool _isAttached;

        // Constructors
        public Moon(Point center, double mass, double radius, Vector velocity)
            : base(center, mass, radius, velocity)
        {
            _isAttached = false;
        }

        public Moon(Point center, double mass, double radius)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)))
        {
        }

        public Moon(Moon moon)
            : this(moon.Center, moon.Mass, moon.Radius, moon.Velocity)
        {
        }

        // Properties
        public int? PlanetarySystemId { get; set; }
        public virtual PlanetarySystem PlanetarySystem { get; set; }
        public int? PlanetId { get; set; }
        public virtual Planet Planet { get { return this._planet; } }
        public bool IsAttached { get { return this._isAttached; } }

        // Methods
        public void DetachFromPlanet()
        {
            this._planet = null;
            this._isAttached = false;
        }

        public void AttachToPlanet(Planet planet)
        {
            if (IsAttached)
            {
                DetachFromPlanet();
            }

            if (this.PlanetarySystem == planet.PlanetarySystem)
            {
                this._planet = planet;
                this._isAttached = true;
            }
            else
            {
                throw new ArgumentException("Planetary system mismatch.");
            }
        }
    }
}
