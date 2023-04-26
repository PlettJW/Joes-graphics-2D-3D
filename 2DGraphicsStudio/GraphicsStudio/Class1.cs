using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsStudio
{
    class Point2D
    {
        #region Class Parameters
        double x = 0;
        double y = 0;
        #endregion

        #region ClassConstructors
        /// <summary>
        /// Create a defult point point (0,0)
        /// </summary>
        public Point2D() { }

        /// <summary>
        /// create a new 2D point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Class Properties
        /// <summary>
        /// Get/Set the x-value
        /// </summary>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Get/Set the y-value
        /// </summary>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// return the Magnitude of a point
        /// </summary>
        public double Magnitude
        {
            get { return Math.Sqrt(x * x + y * y); }
        }

        public Point2D Normal
        {
            get { return new Point2D(-Y, X); }
        }
        #endregion

        #region Class Operators
        /// <summary>
        /// Add two points and return their sum
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point2D operator +(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.x + p2.x, p1.y + p2.y);
        }

        /// <summary>
        /// Subtract two points and return their difference
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point2D operator -(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.x - p2.x, p1.y - p2.y);
        }

        public static Point2D operator *(Point2D p1, double value)
        {
            return new Point2D(p1.x * value, p1.y * value);
        }

        public static Point2D operator *(double value, Point2D p1)
        {
            return new Point2D(value * p1.x, value * p1.y);
        }

        public static Point2D operator /(Point2D p1, double value)
        {
            return new Point2D(p1.x / value, p1.y / value);
        }

        public static double operator *(Point2D p1, Point2D p2)
        {
            return p1.x * p2.x + p1.y * p2.y;
        }

        #endregion

        #region Class Methods

        public PointF ToPointF()
        {
            return new PointF((float)x, (float)y);
        }
        /// <summary>
        /// Normalize the point to a unit vector (magnitude =1)
        /// </summary>
        public void Normalize()
        {
            double magnitude = Magnitude;
            x /= magnitude;
            y /= magnitude;
        }

        /// <summary>
        /// Draw the point on the graphics device with the specified color
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        public void Draw(Graphics gr, Color color)
        {
            gr.FillEllipse(new SolidBrush(color), (float)x - 1, (float)y - 1, 2, 2);
        }

        public void Rotate(double theta)
        {
            double xNew = x * Math.Cos(theta) - y * Math.Sin(theta);

            y = y * Math.Cos(theta) + x * Math.Sin(theta);

            x = xNew;
        }
        #endregion
    }
}
