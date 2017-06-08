using System;
using System.Collections.Generic;
using PlanetSystem.Models.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PlanetSystem.Models.Bodies
{
    public partial class Planet : AstronomicalBody
    {
        // Fields 
        private List<Moon> _moons;
        // Constructors
        public Planet(Point center, double mass, double radius, Vector velocity, string name)
            : base(center, mass, radius, velocity, name)
        {
            InitCollections();
        }

        public Planet(Point center, double mass, double radius, string name)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)), name)
        {
        }

        public Planet(Planet planet)
            : this(planet.Center, planet.Mass, planet.Radius, planet.Velocity, planet.Name)
        {
        }

        // Required from Entity Framework
        private Planet()
        {
            this._moons = new List<Moon>();
        }

        // Properties
        [Key]
        public int PlanetId { get; set; }

        public int? PlanetarySystemId { get; set; }
        [ForeignKey("PlanetarySystemId")]
        public virtual PlanetarySystem PlanetarySystem { get; set; }

        public int? StarId;
        [ForeignKey("StarId")]
        public virtual Star Star { get { return PlanetarySystem.Star; } }

        public virtual ICollection<Moon> Moons
        {
            get { return this._moons; }
            set { this._moons = (List<Moon>)value; }
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

        public void AttachMoons(ICollection<Moon> moons)
        {
            moons.ToList().ForEach(m => this.AttachMoon(m));
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

        private void InitCollections()
        {
            this._moons = new List<Moon>();
        }
    }
}
