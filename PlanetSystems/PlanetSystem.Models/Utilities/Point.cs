using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        public Point(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.Z = point.Z;
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

        public static List<Point> OffsetBy(ICollection<Point> points, Point offset)
        {
            List<Point> pointsList = points.ToList();
            pointsList.ForEach(p => p += offset);
            return pointsList;
        }

        public Point GetOpposite()
        {
            Point result = new Point(
                this.X * (-1),
                this.Y * (-1),
                this.Z * (-1));
            return result;
        }

        public static Point operator + (Point point1, Point point2)
        {
            var result = new Point(
                point1.X + point2.X,
                point1.Y + point2.Y,
                point1.Z + point2.Z);
            return result;
        }

        public static Point operator - (Point point1, Point point2)
        {
            var result = new Point(
                point1.X - point2.X,
                point1.Y - point2.Y,
                point1.Z - point2.Z);
            return result;
        }

        public static Point operator / (Point point, double divisor)
        {
            var result = new Point(
                point.X / divisor,
                point.Y / divisor,
                point.Z / divisor);
            return result;
        }

        //public static Point operator *(Point point, double multiplicator)
        //{
        //    var result = new Point(
        //        point.X * multiplicator,
        //        point.Y * multiplicator,
        //        point.Z * multiplicator);
        //    return result;
        //}
    }
}
