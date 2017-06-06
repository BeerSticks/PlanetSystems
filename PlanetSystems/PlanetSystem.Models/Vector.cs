using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSystem.Models
{
    public partial class Vector
    {
        public Vector()
        {
            this.Moons = new HashSet<Moon>();
            this.Planets = new HashSet<Planet>();
            this.Asteroids = new HashSet<Asteroid>();
            this.AstronomicalBodies = new HashSet<AstronomicalBody>();
            this.ArtificialObjects = new HashSet<ArtificialObject>();
        }

        public Vector(double x, double y, double z, double length, double theta, double phi)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Length = length;
            this.Theta = theta;
            this.Phi = phi;
        }

        [Key]
        public int Id { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double Length { get; set; }

        public double Theta { get; set; }

        public double Phi { get; set; }

        public ICollection<Planet> Planets { get; set; }

        public ICollection<Moon> Moons { get; set; }

        public ICollection<Asteroid> Asteroids { get; set; }

        public ICollection<ArtificialObject> ArtificialObjects { get; set; }

        public ICollection<AstronomicalBody> AstronomicalBodies { get; set; }

    }
}
