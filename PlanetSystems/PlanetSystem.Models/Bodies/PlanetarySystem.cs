using System.Collections.Generic;
using PlanetSystem.Models.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace PlanetSystem.Models.Bodies
{
    public partial class PlanetarySystem
    {
        // Fields
        private string _name;
        private Star _star;
        private List<Planet> _planets;
        private List<Moon> _moons;
        private List<Asteroid> _asteroids;
        private List<AstronomicalBody> _bodies;

        // Constructors
        public PlanetarySystem(string name)
        {
            this.Name = name;
            InitCollections();
        }

        // Required from Entity Framework
        private PlanetarySystem()
        {
            InitCollections();
        }

        // Properties
        [Key]
        public int PlanetarySystemId { get; set; }
        [Required]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value.Length > 0)
                {
                    this._name = value;

                }
                else
                {
                    throw new ArgumentException("Name can not be an empty string");
                }
            }
        }

        public virtual Star Star { get { return this._star; } }
        public virtual ICollection<Planet> Planets { get { return _planets.AsReadOnly(); } }
        public virtual ICollection<Moon> Moons { get { return _moons.AsReadOnly(); } }
        public virtual ICollection<Asteroid> Asteroids { get { return _asteroids.AsReadOnly(); } }

        // Dont put Bodies List in Database.
        [NotMapped]
        public ICollection<AstronomicalBody> Bodies { get { return _bodies.AsReadOnly(); } }

        // Methods
        public void SetStar(Star star)
        {
            this._star = star;
            this._bodies.Add(star);
            star.PlanetarySystem = this;
            //TODO: Detach the old star and maybe reposition the new one to 0,0,0
        }

        public void AddPlanet(Planet planet)
        {
            this._planets.Add(planet);
            this._bodies.Add(planet);
            planet.PlanetarySystem = this;
        }

        public void RemovePlanet(Planet planet)
        {
            this._planets.Remove(planet);
            this._bodies.Remove(planet);
            planet.PlanetarySystem = null;
            // TODO: clear planet's references to the system;
        }

        public void DetachMoonsFromPlanet(Planet planet)
        {
            // TODO: Implement
        }

        public void AdvanceTime(double seconds)
        {
            Physics.SimulateGravitationalInteraction(ref _bodies, seconds);
        }

        private void InitCollections()
        {
            this._planets = new List<Planet>();
            this._moons = new List<Moon>();
            this._asteroids = new List<Asteroid>();
            this._bodies = new List<AstronomicalBody>();
        }
    }
}
