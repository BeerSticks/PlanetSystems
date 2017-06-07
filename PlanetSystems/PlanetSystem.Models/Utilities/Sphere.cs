using System;

namespace PlanetSystem.Models.Utilities
{
    public abstract partial class Sphere
    {
        // Fields
        private double _radius;

        // Constructors
        protected Sphere(double centerX, double centerY, double centerZ, double radius)
            : this(new Point(centerX, centerY, centerZ), radius)
        {
        }

        protected Sphere(Point center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }
        
        // Properties
        public  int PointId { get; set; }
        public virtual Point Center { get; set; }
        public double Radius
        {
            get { return _radius; }
            set
            {
                if (value > 0)
                {
                    _radius = value;
                }
                else
                {
                    throw new ArgumentException("Radius must be greater 0");
                }
            }
        }

        public double Volume
        {
            get
            {
                var volume = 4 * Math.PI * Math.Pow(Radius, 3) / 3;
                return volume;
            }
        }
    }
}
