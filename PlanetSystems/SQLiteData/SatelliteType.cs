using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SQLiteData
{
    public partial class SatelliteType
    {
        public SatelliteType()
        {
            this.Satellites = new HashSet<Satellite>();
        }

        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public int SatteliteID { get; set; }

        public virtual ICollection<Satellite> Satellites { get; set; }
    }
}