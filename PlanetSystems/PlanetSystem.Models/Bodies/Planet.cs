using System;
using System.Collections.Generic;
using PlanetSystem.Models.Utilities;

namespace PlanetSystem.Models.Bodies
{
    public partial class Planet : AstronomicalBody
    {
        // Fields 
        private List<Moon> _moons;
        // Constructors
        public Planet(Point center, double mass, double radius, Vector velocity)
            : base(center, mass, radius, velocity)
        {
            this._moons = new List<Moon>();
        }

        public Planet(Point center, double mass, double radius)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)))
        {
        }

        public Planet(Planet planet)
            : this(planet.Center, planet.Mass, planet.Radius, planet.Velocity)
        {
        }

        // Properties
        public int? PlanetarySystemId { get; set; }
        public PlanetarySystem PlanetarySystem { get; set; }
        public Star Star { get { return PlanetarySystem.Star; } }
        public ICollection<Moon> Moons
        {
            get { return this._moons.AsReadOnly(); }
        }

        // Methods
        public void AttachMoon(Moon moon)
        {
            if (this.PlanetarySystem == moon.PlanetarySystem)
            {
                if (!moon.IsAttached)
                {
                    if (_moons.IndexOf(moon) < 0)
                    {
                        _moons.Add(moon);
                        moon.AttachToPlanet(this);
                    }
                    else
                    {
                        throw new ArgumentException("Moon already attached to this planet.");
                    }
                }
                else
                {
                    throw new ArgumentException("Moon is already attached to this or other planet.");
                }
            }
            else
            {
                throw new ArgumentException("Planetary system mismatch");
            }
        }

        public void DetachMoon(Moon moon)
        {
            int index = _moons.IndexOf(moon);
            if (index >= 0)
            {
                moon.DetachFromPlanet();
                _moons.Remove(moon);
            }
        }
    }
}
