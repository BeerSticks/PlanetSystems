using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSystem.Models.Utilities.Contracts
{
    public interface IAstronomicalBody
    {
        string Name { get; set; }
        double Mass { get; set; }
        double Density { get; }
        Vector Velocity { get; set; }
        void AdvanceMovement(double timeInSeconds);
        void ApplyForce(Vector force, double secondsUnderForce);
    }
}
