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
        public Planet()
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

        public virtual ICollection<Moon> Moons { get; set; }

        // Methods
        //public void AttachMoonByOrbitalRadius(Moon moon, double radius)
        //{
        //    if (!this.Moons.Contains(moon))
        //    {
        //        bool nameFound = false;
        //        foreach (var m in this.Moons)
        //        {
        //            if (moon.Name == m.Name)
        //            {
        //                nameFound = true;
        //                break;
        //            }
        //        }

        //        if (!nameFound)
        //        {
        //            this.Moons.Add(moon);
        //            moon.AttachToPlanet(this);
        //            Physics.EnterOrbitByGivenRadius(ref moon, this, radius);
        //        }
        //    }
        //}

        //public void AttachMoonByOrbitalSpeed(Moon moon, double speed)
        //{
        //    if (!this.Moons.Contains(moon))
        //    {
        //        bool nameFound = false;
        //        foreach (var m in this.Moons)
        //        {
        //            if (moon.Name == m.Name)
        //            {
        //                nameFound = true;
        //                break;
        //            }
        //        }

        //        if (!nameFound)
        //        {
        //            this.Moons.Add(moon);
        //            moon.AttachToPlanet(this);
        //            Physics.EnterOrbitByGivenSpeed(ref moon, this, speed);
        //        }
        //    }
        //}

        //public void DetachMoon(Moon moon)
        //{
        //    int index = _moons.IndexOf(moon);
        //    if (index >= 0)
        //    {
        //        moon.DetachFromPlanet();
        //        _moons.Remove(moon);
        //    }
        //}

        //public void DetachMoons()
        //{
        //    foreach (var moon in Moons)
        //    {
        //        DetachMoon(moon);
        //    }
        //}

        private void InitCollections()
        {
            this.Moons = new List<Moon>();
            this._moons = new List<Moon>();
        }
    }
}
