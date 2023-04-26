using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Game2D;

namespace Graphics3D
{
    class Point3D
    {
        #region Parameters
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a Point3D with 0 as cordinate values
        /// </summary>
        public Point3D() { }
        /// <summary>
        /// Construct a new object of type Point3D
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion

        #region Properties
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;

        public Point3D Center { get { return new Point3D(X, Y, Z); }  }

        public double Magnitude
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        
        #endregion

        #region Operators

        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Point3D operator *(Point3D p1, double value)
        {
            return new Point3D(p1.X * value, p1.Y * value, p1.Z * value);
        }

        public static Point3D operator *(double value, Point3D p1)
        {
            return new Point3D(value * p1.X, value * p1.Y, value * p1.Z);
        }

        public static Point3D operator /(Point3D p1, double value)
        {
            return new Point3D(p1.X / value, p1.Y / value, p1.Z / value);
        }

        public static double operator *(Point3D p1, Point3D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }
        #endregion

        #region Methods
        public void Normalize()
        {
            double magnitude = Magnitude;
            X /= magnitude;
            Y /= magnitude;
        }

        public Point2D Projection(double distance)
        {
            return new Point2D(X, Y) * distance / (distance - Z);
        }

        /// <summary>
        /// Draw the point to the user interface
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr, double distance)
        {
            // we need to project the 3D point onto a 2D surface
            Projection(distance).Draw(gr, Color.White);
        }

        public void Rotate(Point3D theta)
        {
            //z axis
            double xNew = X * Math.Cos(theta.Z) - Y * Math.Sin(theta.Z);

            Y = Y * Math.Cos(theta.Z) + X * Math.Sin(theta.Z);

            X = xNew;

            //Y axis
            double zNew = Z * Math.Cos(theta.Y) - X * Math.Sin(theta.Y);

            X = X * Math.Cos(theta.Y) + Z * Math.Sin(theta.Y);

            Z = zNew;

            //z =y y =x x =z

            //X axis
            double yNew = Y * Math.Cos(theta.X) - Z * Math.Sin(theta.X);

            Z = Z * Math.Cos(theta.X) + Y * Math.Sin(theta.X);

            Y = yNew;
        }

        public void UnRotate(Point3D theta)
        {
            theta *= -1;
            //x axis
            double yNew = Y * Math.Cos(theta.X) - Z * Math.Sin(theta.X);

            Z = Z * Math.Cos(theta.X) + Y * Math.Sin(theta.X);

            Y = yNew;

            //Y axis
            double zNew = Z * Math.Cos(theta.Y) - X * Math.Sin(theta.Y);

            X = X * Math.Cos(theta.Y) + Z * Math.Sin(theta.Y);

            Z = zNew;

            //z axis
            double xNew = X * Math.Cos(theta.Z) - Y * Math.Sin(theta.Z);

            Y = Y * Math.Cos(theta.Z) + X * Math.Sin(theta.Z);

            X = xNew;
        }
        #endregion
    }
}
