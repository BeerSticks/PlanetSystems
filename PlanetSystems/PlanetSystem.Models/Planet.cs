using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetSystem.Models
{
    public partial class Planet
    {
        public Planet()
        {
            this.Moons = new HashSet<Moon>();
            this.Coordinates = new Models.Point();
            this.Velocity = new Models.Vector();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public double Mass { get; set; }

        public double Density { get; set; }

        //[ForeignKey("fk_StarCoordinates_Point")]
        public int PointID { get; set; }

        public virtual Point Coordinates { get; set; }

        //[ForeignKey("fk_PlanetVelocity_Vector")]
        public int VectorID { get; set; }

        public Vector Velocity { get; set; }

        //[ForeignKey("fk_Planet_PlanetSystem")]
        public int PlanetSystemID { get; set; }

        public virtual ICollection<Moon> Moons { get; set; }
    }
}