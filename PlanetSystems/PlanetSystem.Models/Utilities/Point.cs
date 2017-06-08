using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetSystem.Models.Utilities
{
    [ComplexType]
    [Table("Positions")]
    public partial class Point
    {
        // Constructors
        public Point(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        private Point() { }

        // Properties
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
