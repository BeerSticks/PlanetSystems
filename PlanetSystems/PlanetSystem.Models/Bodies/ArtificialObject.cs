using PlanetSystem.Models.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetSystem.Models.Bodies
{
    public partial class ArtificialObject : AstronomicalBody
    {
        // Constructors
        public ArtificialObject(Point center, double mass, double radius, Vector velocity, string name)
            : base(center, mass, radius, velocity, name)
        {
        }

        public ArtificialObject(Point center, double mass, double radius, string name)
            : this(center, mass, radius, new Vector(new Point(0, 0, 0)), name)
        {

        }

        public ArtificialObject(ArtificialObject artificialObject)
            : this(artificialObject.Center, artificialObject.Mass, artificialObject.Radius, artificialObject.Velocity, artificialObject.Name)
        {
        }

        // Required from Entity Framework
        private ArtificialObject() { }

        // Properties
        [Key]
        public int ArtificialObjectId { get; set; }
        public int? PlanetarySystemId { get; set; }

        [ForeignKey("PlanetarySystemId")]
        public virtual PlanetarySystem PlanetarySystem { get; set; }        
    }
}
