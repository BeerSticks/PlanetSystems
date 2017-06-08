using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlanetSystem.Models.Utilities
{
    [ComplexType]
    [Table("Velocities")]
    public partial class Vector
    {
        // Fields
        private double _x;
        private double _y;
        private double _z;
        private double _length;
        private double _theta;
        private double _phi;
        private double _thetaSin;
        private double _thetaCos;
        private double _phiSin;
        private double _phiCos;

        // Constructors
        public Vector(Point endPoint)
        {
            BuildFromCartesianInput(endPoint);
        }

        public Vector(double length, double theta, double phi)
        {
            BuildFromSphericalInput(length, theta, phi);
        }

        public Vector(Vector vector)
        {
            BuildFromSphericalInput(vector.Length, vector.Theta, vector.Phi);
        }

        public Vector()
        {
            BuildFromCartesianInput(new Point(0, 0, 0));
        }

        //Properties

        //-------------------------------------------------------
        #region CartesianProperties
        public double X
        {
            get { return this._x; }
            set { BuildFromCartesianInput(new Point(value, this._y, this._z)); }
        }

        public double Y
        {
            get { return this._y; }
            set { BuildFromCartesianInput(new Point(this._x, value, this._z)); }
        }

        public double Z
        {
            get { return this._z; }
            set { BuildFromCartesianInput(new Point(this._x, this._y, value)); }
        }
        #endregion

        //-------------------------------------------------------
        #region PolarProperties
        public double Length
        {
            get { return this._length; }
            set { BuildFromSphericalInput(value, this._theta, this._phi); }
        }

        // <ZR
        public double Theta
        {
            get { return this._theta; }
            set { BuildFromSphericalInput(this._length, value, this._phi); }
        }

        // <X(R in XY)
        public double Phi
        {
            get { return this._phi; }
            set { BuildFromSphericalInput(this._length, this._theta, value); }
        }
        #endregion

        // Methods
        #region BuildRegion
        private void BuildFromCartesianInput(Point endPoint)
        {
            this._x = endPoint.X;
            this._y = endPoint.Y;
            this._z = endPoint.Z;
            this._length = Physics.GetDistanceBetweenPoints(new Point(0, 0, 0), endPoint);

            // Prevent throwing errors and angles from changing when input is 0,0,0
            if (this._length != 0)
            {
                this._thetaCos = Z / this.Length;
                _theta = Math.Acos(this._thetaCos);
                if (X != 0 || Y != 0)
                {
                    this._phiCos = X / Math.Sqrt((X * X) + (Y * Y));
                }
                else
                {
                    this._phiCos = 1;
                }

                _phi = Math.Acos(this._phiCos);
                if (Y < 0)
                {
                    _phi *= -1;
                }
            }
        }

        private void BuildFromSphericalInput(double length, double theta, double phi)
        {
            this._theta = theta;
            this._phi = phi;
            this._length = length;
            this._thetaCos = Math.Cos(theta);
            this._thetaSin = Math.Sin(theta);
            this._phiCos = Math.Cos(phi);
            this._phiSin = Math.Sin(phi);

            _z = this._thetaCos * length;
            _x = this._phiCos * this._thetaSin * length;
            _y = this._phiSin * this._thetaSin * length;
        }
        #endregion

        #region OperatorOverrides
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            double x = vector1.X + vector2.X;
            double y = vector1.Y + vector2.Y;
            double z = vector1.Z + vector2.Z;
            Vector resultingVector = new Vector(new Point(x, y, z));

            return resultingVector;
        }

        public static Vector operator /(Vector vector, double divisor)
        {
            if (divisor == 0)
            {
                throw new ArgumentException("Division by zero");
            }

            double resultingLength = vector.Length / divisor;
            Vector resultingVector = new Vector(resultingLength, vector.Theta, vector.Phi);
            return resultingVector;
        }

        public static Vector operator *(Vector vector, double multiplicator)
        {
            double resultingLength = vector.Length * multiplicator;
            Vector resultingVector = new Vector(resultingLength, vector.Theta, vector.Phi);
            return resultingVector;
        }

        public static Vector operator *(double multiplicator, Vector vector)
        {
            double resultingLength = multiplicator * vector.Length;
            Vector resultingVector = new Vector(resultingLength, vector.Theta, vector.Phi);
            return resultingVector;
        }
        #endregion

        public static Vector GetAverage(ICollection<Vector> vectors)
        {
            List<Point> endPoints = new List<Point>();
            foreach (var vector in vectors)
            {
                Point point = new Point(vector.X, vector.Y, vector.Z);
                endPoints.Add(point);
            }

            Point avgEndPoint = Point.GetAverage(endPoints);
            Vector resultingVector = new Vector(avgEndPoint);
            return resultingVector;
        }

        public Vector GetOpposite()
        {
            Vector opposite = new Vector(new Point(this.X * (-1), this.Y * (-1), this.Z * (-1)));
            return opposite;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Vector--------");
            sb.AppendLine($"         X = {this.X}");
            sb.AppendLine($"         Y = {this.Y}");
            sb.AppendLine($"         Z = {this.Z}");
            sb.AppendLine($"    Length = {this.Length}");
            sb.AppendLine($"     Theta = {this.Theta}");
            sb.AppendLine($"       Phi = {this.Phi}");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
