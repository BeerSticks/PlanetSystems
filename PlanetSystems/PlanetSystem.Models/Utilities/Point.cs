using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlanetSystem.Models.Utilities
{
    public partial class Point
    {
        // Constructors
        public Point(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // Properties
        [Key]
        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // Mehtods
        public static Point GetAverage(ICollection<Point> points)
        {
            double xSum = 0;
            foreach (var point in points)
            {
                xSum += point.X;
            }

            double ySum = 0;
            foreach (var point in points)
            {
                ySum += point.Y;
            }

            double zSum = 0;
            foreach (var point in points)
            {
                zSum += point.Z;
            }

            double xAvg = xSum / points.Count;
            double yAvg = ySum / points.Count;
            double zAvg = zSum / points.Count;

            Point result = new Point(xAvg, yAvg, zAvg);
            return result;
        }
    }
}
