using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetSystem.Models
{
    public partial class Asteroid
    {
        public Asteroid()
        {
            this.PlanetSystem = new PlanetSystem();
            this.Coordinates = new Models.Point();
            this.Velocity = new Models.Vector();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public double Radius { get; set; }

        public double Mass { get; set; }

        public double Density { get; set; }

        //[ForeignKey("fk_AsteroidCoordinates_Point")]
        public int PointID { get; set; }

        public virtual Point Coordinates { get; set; }

        //[ForeignKey("fk_AsteroidVelocity_Vector")]
        public int VectorID { get; set; }

        public Vector Velocity { get; set; }

        //[ForeignKey("fk_Asteroid_PlanetSystem")]
        public int PlanetSystemID { get; set; }

        public virtual PlanetSystem PlanetSystem { get; set; }
    }
}