using PlanetSystem.Models.Utilities;

namespace PlanetSystem.Models.Bodies
{
    public partial class ArtificialObject : AstronomicalBody
    {
        // Constructors
        public ArtificialObject(Point center, double mass, double radius, Vector velocity)
            : base(center, mass, radius, velocity)
        {
        }

        public ArtificialObject(Point center, double mass, double radius)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)))
        {
        }

        public ArtificialObject(ArtificialObject artificialObject)
            : this(artificialObject.Center, artificialObject.Mass, artificialObject.Radius, artificialObject.Velocity)
        {
        }

        // Properties
        public int? PlanetarySystemId { get; set; }
        public virtual PlanetarySystem PlanetarySystem { get; set; }
    }
}
