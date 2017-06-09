using PlanetSystem.Models.Utilities.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlanetSystem.Models.Utilities
{
    public partial class AstronomicalBody : Sphere, IAstronomicalBody
    {
        // Fields
        private double _mass;
        private string _name;

        // Construcors
        public AstronomicalBody(Point center, double mass, double radius, Vector velocity, string name)
            : base(center, radius)
        {
            Velocity = velocity;
            Mass = mass;
            Name = name;
        }

        public AstronomicalBody(Point center, double mass, double radius, string name)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)), name)
        {
        }

        public AstronomicalBody(AstronomicalBody body)
            : this(body.Center, body.Mass, body.Radius, body.Velocity, body.Name)
        {
        }

        protected AstronomicalBody() { }

        // Properties

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

        [Required]
        public double Mass
        {
            get { return _mass; }
            set
            {
                if (value > 0)
                {
                    _mass = value;
                }
                else
                {
                    throw new ArgumentException("Mass must be greater 0");
                }
            }
        }

        public double Density
        {
            get
            {
                var density = Mass / Radius;
                return density;
            }
        }

        public virtual Vector Velocity { get; set; }

        // Methods
        public void AdvanceMovement(double timeInSeconds)
        {
            AstronomicalBody dummyBody = new AstronomicalBody(this.Center, this.Mass, this.Radius, this.Velocity, this.Name);
            Physics.AdvanceMovementOfBody(ref dummyBody, timeInSeconds);
            this.Center = dummyBody.Center;
        }

        public void ApplyForce(Vector force, double secondsUnderForce)
        {
            AstronomicalBody dummyBody = new AstronomicalBody(this.Center, this.Mass, this.Radius, this.Velocity, this.Name);
            Physics.ApplyForceToBody(ref dummyBody, force, secondsUnderForce);
            this.Center = dummyBody.Center;
            this.Velocity = dummyBody.Velocity;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Body----------");
            sb.AppendLine($"         X = {this.Center.X}");
            sb.AppendLine($"         Y = {this.Center.Y}");
            sb.AppendLine($"         Z = {this.Center.Z}");
            //sb.AppendLine($"      Mass = {this.Mass}");
            //sb.AppendLine($"    Radius = {this.Radius}");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
