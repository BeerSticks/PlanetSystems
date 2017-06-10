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
        #region Properties
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
                if (value.Length > 0 && value.Length <= 40)
                {
                    this._name = value;
                }
                else
                {
                    throw new ArgumentException("Name length must be in the range of 1 to 40 character");
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
            get { return GetMoons(); }
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
        #endregion
            
        // Methods
        #region Stars
        public void SetStar(Star star)
        {
            if (this.Star != null)
            {
                this.Star.Detach();
            }
            star.Attach(this);
            this.Star = star;
        }
        #endregion

        #region Planets
        public void AddPlanetByOrbitalRadius(Planet planet, double radius)
        {
            this.Planets.Add(planet);
            planet.Attach(this);
            Physics.EnterOrbitByGivenRadius(ref planet, this.Star, radius);
        }

        public void AddPlanetByOrbitalSpeed(Planet planet, double speed)
        {
            this.Planets.Add(planet);
            planet.Attach(this);
            Physics.EnterOrbitByGivenSpeed(ref planet, this.Star, speed);
        }

        public void RemovePlanet(string name)
        {
            var planetQuery = from p in this.Planets
                              where p.Name == name
                              select p;
            var planet = planetQuery.FirstOrDefault();
            if (planet != null)
            {
                planet.RemoveAllMoons();
            }
        }
        #endregion

        #region Moons
        private List<Moon> GetMoons()
        {
            List<Moon> moons = new List<Moon>();
            foreach (var p in this.Planets)
            {
                moons.AddRange(p.Moons);
            }

            return moons;
        }

        public void AddMoonByOrbitalRadius(Moon moon, Planet planet, double radius)
        {
            planet.AddMoonByOrbitalRadius(moon, radius);
        }

        public void AddMoonByOrbitalSpeed(Moon moon, Planet planet, double speed)
        {
            planet.AddMoonByOrbitalSpeed(moon, speed);
        }
        #endregion

        #region Asteroids and ArtificialObjects
        public void AddAsteroid(Asteroid asteroid)
        {
            this._asteroids.Add(asteroid);
            asteroid.PlanetarySystem = this;
        }

        public void RemoveAsteroid(Asteroid asteroid)
        {
            this._asteroids.Remove(asteroid);
            asteroid.PlanetarySystem = null;
        }

        public void AddArtificialObject(ArtificialObject artificialObject)
        {
            this._artificialObjects.Add(artificialObject);
            artificialObject.PlanetarySystem = this;
        }

        public void RemoveArtificialObject(ArtificialObject artificialObject)
        {
            this._artificialObjects.Remove(artificialObject);
            artificialObject.PlanetarySystem = null;
        }
        #endregion

        #region Other
        public void AdvanceTime(List<Utilities.AstronomicalBody> bodiesInAccount, double seconds)
        {
            Physics.SimulateGravitationalInteraction(ref bodiesInAccount, seconds);
        }

        private void InitCollections()
        {
            this._planets = new List<Planet>();
            //this._moons = new List<Moon>();
            this._asteroids = new List<Asteroid>();
            this._artificialObjects = new List<ArtificialObject>();
        }

        public List<Utilities.AstronomicalBody> GetAllBodies()
        {
            List<Utilities.AstronomicalBody> bodies = new List<Utilities.AstronomicalBody>();
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
        #endregion

    }
}