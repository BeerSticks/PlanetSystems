using System.Collections.Generic;
using PlanetSystem.Models.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;

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
        private List<ArtificialObject> _artificialObjects;
        //private List<AstronomicalBody> _bodies;

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

        public virtual Star Star
        {
            get { return this._star; }
            set { this._star = value; }
        }
        public virtual ICollection<Planet> Planets
        {
            get { return this._planets; }
            set { this._planets = value.ToList(); }
        }
        public virtual ICollection<Moon> Moons
        {
            get { return this._moons; }
            set { this._moons = (List<Moon>)value; }
        }

        public virtual ICollection<Asteroid> Asteroids
        {
            get { return this._asteroids; }
            set { this._asteroids = (List<Asteroid>)value; }
        }
        public virtual ICollection<ArtificialObject> ArtificialObjects
        {
            get { return this._artificialObjects; }
            set { this._artificialObjects = (List<ArtificialObject>)value; }
        }

        // Dont put Bodies List in Database.
        //[NotMapped]
        //public ICollection<AstronomicalBody> Bodies { get { return _bodies.AsReadOnly(); } }

        // Methods
        public void SetStar(Star star)
        {
            this._star = star;
            //this._bodies.Add(star);
            star.PlanetarySystem = this;
            //TODO: Detach the old star and maybe reposition the new one to 0,0,0
        }

        public void AddPlanetByOrbitalRadius(Planet planet, double radius)
        {
            this._planets.Add(planet);
            //this._bodies.Add(planet);
            this._star.Planets.Add(planet);
            planet.PlanetarySystem = this;
            Physics.EnterOrbitByGivenRadius(ref planet, this.Star, radius);
        }

        public void AddPlanetByOrbitalSpeed(Planet planet, double speed)
        {
            this._planets.Add(planet);
            //this._bodies.Add(planet);
            this._star.Planets.Add(planet);
            planet.PlanetarySystem = this;
            Physics.EnterOrbitByGivenSpeed(ref planet, this.Star, speed);
        }

        public void RemovePlanet(Planet planet)
        {
            this._planets.Remove(planet);
            //this._bodies.Remove(planet);
            this._star.Planets.Remove(planet);
            planet.PlanetarySystem = null;

            // TODO: clear planet's references to the system;
        }

        public void AttachMoonToPlanetByOrbitalRadius(Moon moon, Planet planet, double radius)
        {
            // TODO: Validations
            moon.DetachFromPlanet();
            moon.PlanetarySystem = this;
            this._planets[this._planets.IndexOf(planet)].AttachMoonByOrbitalRadius(moon, radius);
        }

        public void AttachMoonToPlanetByOrbitalSpeed(Moon moon, Planet planet, double speed)
        {
            // TODO: Validations
            moon.DetachFromPlanet();
            moon.PlanetarySystem = this;
            this._planets[this._planets.IndexOf(planet)].AttachMoonByOrbitalSpeed(moon, speed);
        }

        public void DetachMoonFromPlanet(Moon moon, Planet planet)
        {
            // TODO: Implement
        }

        public void DetachMoonsFromPlanet(Planet planet)
        {
            // TODO: Implement
        }


        public void AddAsteroid(Asteroid asteroid)
        {
            this._asteroids.Add(asteroid);
            //this._bodies.Add(asteroid);
            asteroid.PlanetarySystem = this;
        }

        public void RemoveAsteroid(Asteroid asteroid)
        {
            this._asteroids.Remove(asteroid);
            //this._bodies.Remove(asteroid);
            asteroid.PlanetarySystem = null;
        }

        public void AddArtificialObject(ArtificialObject artificialObject)
        {
            this._artificialObjects.Add(artificialObject);
            //this._bodies.Add(artificialObject);
            artificialObject.PlanetarySystem = this;
        }

        public void RemoveArtificialObject(ArtificialObject artificialObject)
        {
            this._artificialObjects.Remove(artificialObject);
            //this._bodies.Remove(artificialObject);
            artificialObject.PlanetarySystem = null;
        }

        public void AdvanceTime(List<AstronomicalBody> bodiesInAccount, double seconds)
        {
            Physics.SimulateGravitationalInteraction(ref bodiesInAccount, seconds);
        }

        private void InitCollections()
        {
            this._planets = new List<Planet>();
            this._moons = new List<Moon>();
            this._asteroids = new List<Asteroid>();
            this._artificialObjects = new List<ArtificialObject>();
            //this._bodies = new List<AstronomicalBody>();
        }

        public List<AstronomicalBody> GetAllBodies()
        {
            List<AstronomicalBody> bodies = new List<AstronomicalBody>();
            bodies.Add(this.Star);
            foreach (var planet in this.Planets)
            {
                bodies.Add(planet);
            }

            foreach (var moon in this.Moons)
            {
                bodies.Add(moon);
            }

            foreach (var asteroid in this.Asteroids)
            {
                bodies.Add(asteroid);
            }

            foreach (var artObj in this.ArtificialObjects)
            {
                bodies.Add(artObj);
            }
            return bodies;
        }
    }
}
