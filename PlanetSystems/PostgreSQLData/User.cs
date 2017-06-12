using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLData
{
    public partial class User
    {
        public User()
        {
            this.OwnPlanetarySystems = new HashSet<OwnPlanetarySystem>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<OwnPlanetarySystem> OwnPlanetarySystems { get; set; }

    }
}
