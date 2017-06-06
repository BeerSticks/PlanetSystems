using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetSystem.Models
{
    public partial class Star
    {
        public Star()
        {
            this.PlanetSystem = new PlanetSystem();
            this.Coordinates = new Models.Point();
            
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public double Radius { get; set; }

        public double Mass { get; set; }

        public double Density { get; set; }

        ///[ForeignKey("fk_StarCoordinates_Point")]
        public int PointID { get; set; }

        public virtual Point Coordinates { get; set; }

        //[ForeignKey("fk_Star_PlanetSystem")]
        public int PlanetSystemID { get; set; }

        public virtual PlanetSystem PlanetSystem { get; set; }


    }
}