using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSystem.Models
{
    public partial class PlanetSystem
    {
        public PlanetSystem()
        {
            this.Planets = new HashSet<Planet>();
            this.Asteroids = new HashSet<Asteroid>();
            this.Stars = new HashSet<Star>();
            this.ArtificialObjects = new HashSet<ArtificialObject>();
        }
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public virtual ICollection<Planet> Planets { get; set; }

        public virtual ICollection<Asteroid> Asteroids { get; set; }

        public virtual ICollection<Star> Stars { get; set; }

        public virtual ICollection<ArtificialObject> ArtificialObjects { get; set; }
    }
}
