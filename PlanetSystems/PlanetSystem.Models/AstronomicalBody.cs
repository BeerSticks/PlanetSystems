using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSystem.Models
{
    public partial class AstronomicalBody
    {
        public AstronomicalBody()
        {
            this.Center = new Models.Point();
            this.Velocity = new Models.Vector();
        }

        [Key]
        public int id { get; set; }

        public double Mass { get; set; }

        public double Radius { get; set; }

        //[ForeignKey("fk_BodyCenter_Point")]
        public int PointID { get; set; }

        public virtual Point Center { get; set; }

        //[ForeignKey("fk_BodyVelocity_Vector")]
        public int VectorID { get; set; }

        public virtual Vector Velocity { get; set; }
    }
}
