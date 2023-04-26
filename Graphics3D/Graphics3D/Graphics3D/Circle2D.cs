using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game2D
{
    class Circle2D : Point2D
    {
        #region Class Parameters
        double radius = 0; // the radius of the circle
        #endregion

        #region Class Constructors
        /// <summary>
        /// Create a defult circle, at (0,0) with 0 radius
        /// </summary>
        public Circle2D() { }

        /// <summary>
        /// Construct a circle with specified x,y,radius values
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Circle2D(Point2D center, double radius)
        {
            X = center.X;
            Y = center.Y;
            this.radius = radius;
        }

        public Circle2D(double x, double y, double radius)
        {
            X = y;
            Y = y;
            this.radius = radius;
        }
        #endregion

        #region Class Properties

        public Point2D Center
        {
            get { return new Point2D(X, Y); }
            set { X = value.X; Y = value.Y; }
        }
        /// <summary>
        /// Get/Set radius
        /// </summary>
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        #endregion

        #region Class Methods
        public void Draw(Graphics gr, Color color)
        {
            gr.DrawEllipse(new Pen(color),
                (float)(X - radius), (float)(Y - radius),
                (float)(2 * radius), (float)(2 * radius));
        }

        public void Draw(Graphics gr, Pen pen)
        {
            gr.DrawEllipse(pen,
                (float)(X - radius), (float)(Y - radius),
                (float)(2 * radius), (float)(2 * radius));
        }

        public void Fill(Graphics gr, Color color)
        {
            gr.FillEllipse(new SolidBrush(color),
                (float)(X - radius), (float)(Y - radius),
                (float)(2 * radius), (float)(2 * radius));
        }

        public void Fill(Graphics gr, Brush brush)
        {
            gr.FillEllipse(brush,
                (float)(X - radius), (float)(Y - radius),
                (float)(2 * radius), (float)(2 * radius));
        }

        #endregion
    }
}
