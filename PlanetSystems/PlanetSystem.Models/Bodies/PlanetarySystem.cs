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
        public void AddPlanetByOrbitalRadius(Planet planet, double radius, double coveredAngle)
        {
            this.Planets.Add(planet);
            planet.Attach(this);
            Physics.EnterOrbitByGivenRadius(ref planet, this.Star, radius, coveredAngle);
        }

        public void AddPlanetByOrbitalSpeed(Planet planet, double speed, double coveredAngle)
        {
            this.Planets.Add(planet);
            planet.Attach(this);
            Physics.EnterOrbitByGivenSpeed(ref planet, this.Star, speed, coveredAngle);
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
                this.Planets.Remove(planet);
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

        public void AddMoonByOrbitalRadius(Moon moon, Planet planet, double radius, double coveredAngle)
        {
            planet.AddMoonByOrbitalRadius(moon, radius, coveredAngle);
        }

        public void AddMoonByOrbitalSpeed(Moon moon, Planet planet, double speed, double coveredAngle)
        {
            var dummy = planet;
            var dummy2 = moon;
            planet.AddMoonByOrbitalSpeed(moon, speed, coveredAngle);
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

        public void RemoveAsteroid(string name)
        {
            var asteroidQuery = from a in this.Asteroids
                                where a.Name == name
                                select a;
            var asteroid = asteroidQuery.FirstOrDefault();
            if (asteroid != null)
            {
                RemoveAsteroid(asteroid);
            }
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

        public void RemoveArtificialObject(string name)
        {
            var artObjQuery = from a in this.ArtificialObjects
                              where a.Name == name
                              select a;
            var artObj = artObjQuery.FirstOrDefault();
            if (artObj != null)
            {
                RemoveArtificialObject(artObj);
            }
        }

        #endregion

        #region Other
        public void AdvanceTime(List<AstronomicalBody> bodiesInAccount, double seconds)
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

        public List<Point> GetAllPositions()
        {
            List<Point> positions = new List<Point>();
            GetAllBodies().ToList().ForEach(b => positions.Add(b.Center));
            return positions;
        }
        #endregion

        #region CLONING
        public PlanetarySystem Clone()
        {
            PlanetarySystem clone = new PlanetarySystem(this.Name);
            Star cloneStar = new Star(this.Star);
            clone.SetStar(cloneStar);

            this.Planets.ToList().ForEach(pl =>
            {
                Planet clonePlanet = new Planet(pl);
                clone.AddPlanetNOTORBITALSAFE(clonePlanet);
                pl.Moons.ToList().ForEach(m =>
                {
                    Moon cloneMoon = new Moon(m);
                    pl.AddMoonNOTORBITALSAFE(cloneMoon);
                });
            });

            this.Asteroids.ToList().ForEach(a =>
            {
                Asteroid cloneAsteroid = new Asteroid(a);
                clone.AddAsteroid(cloneAsteroid);
            });

            this.ArtificialObjects.ToList().ForEach(artObj =>
            {
                ArtificialObject cloneArtObj = new ArtificialObject(artObj);
                clone.AddArtificialObject(cloneArtObj);
            });

            return clone;
        }
        public void AddPlanetNOTORBITALSAFE(Planet planet)
        {
            this.Planets.Add(planet);
            planet.Attach(this);
        }

        public void AddMoonNOTORBITALSAFE(Moon moon, Planet planet)
        {
            planet.AddMoonNOTORBITALSAFE(moon);
        }
        #endregion
    }
}