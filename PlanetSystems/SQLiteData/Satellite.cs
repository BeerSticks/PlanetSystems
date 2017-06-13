using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteData
{
    public partial class Satellite
    {
        public Satellite()
        {
            this.SatelliteType = new SatelliteType();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int SattelliteTypeId { get; set; }

        public virtual SatelliteType SatelliteType {get;set;}
    }
}
