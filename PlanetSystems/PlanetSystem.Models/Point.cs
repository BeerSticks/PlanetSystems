using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSystem.Models
{
    public partial class Point
    {
        public Point()
        {
            this.Moons = new HashSet<Moon>();
            this.Planets = new HashSet<Planet>();
            this.Asteroids = new HashSet<Asteroid>();
            this.AstronomicalBodies = new HashSet<AstronomicalBody>();
            this.Stars = new HashSet<Star>();
            this.ArtificialObjects = new HashSet<ArtificialObject>();

        }

        public Point(double x, double y, double z)
        {
            this.Moons = new HashSet<Moon>();
            this.Planets = new HashSet<Planet>();
            this.Asteroids = new HashSet<Asteroid>();
            this.AstronomicalBodies = new HashSet<AstronomicalBody>();
            this.Stars = new HashSet<Star>();
            this.ArtificialObjects = new HashSet<ArtificialObject>();

            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        [Key]
        public int Id { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    
        public double Z { get; set; }

        public ICollection<Star> Stars { get; set; }

        public ICollection<Planet> Planets { get; set; }

        public ICollection<Moon> Moons { get; set; }

        public ICollection<Asteroid> Asteroids { get; set; }
    
        public ICollection<ArtificialObject> ArtificialObjects { get; set; }

        public ICollection<AstronomicalBody> AstronomicalBodies { get; set; }


    }
}
