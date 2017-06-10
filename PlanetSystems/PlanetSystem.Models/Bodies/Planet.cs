using System;
using System.Collections.Generic;
using PlanetSystem.Models.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PlanetSystem.Models.Bodies
{
    public partial class Planet : Utilities.AstronomicalBody
    {
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
            InitCollections();
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

        public void AddMoonByOrbitalRadius(Moon moon, double radius)
        {
            RemoveMoon(moon.Name);
            this.Moons.Add(moon);
            moon.AttachToPlanet(this);
            Physics.EnterOrbitByGivenRadius(ref moon, this, radius);
        }
        public void AddMoonByOrbitalSpeed(Moon moon, double speed)
        {
            RemoveMoon(moon.Name);
            this.Moons.Add(moon);
            moon.AttachToPlanet(this);
            Physics.EnterOrbitByGivenRadius(ref moon, this, speed);
        }

        public void RemoveMoon(string name)
        {
            var moonQuery = from m in this.Moons
                            where m.Name == name
                            select m;
            var moon = moonQuery.FirstOrDefault();
            if (moon != null)
            {
                this.Moons.Remove(moon);
                moon.DetachFromPlanet();
            }
        }

        public void RemoveAllMoons()
        {
            this.Moons.ToList().ForEach(m => RemoveMoon(m.Name));
        }

        public void Attach(PlanetarySystem planetarySystem)
        {
            RemoveAllMoons();            
            this.PlanetarySystem = planetarySystem;
            this.Moons = new List<Moon>();
        }
        
        private void InitCollections()
        {
            this.Moons = new List<Moon>();
        }
    }
}
