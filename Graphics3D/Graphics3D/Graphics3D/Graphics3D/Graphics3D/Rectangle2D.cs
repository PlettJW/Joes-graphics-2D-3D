using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game2D
{
    class Rectangle2D
    {
        #region Class Parameters
        List<Line2D> edges = new List<Line2D>();
        #endregion

        #region Class Constructors
        public Rectangle2D() { }
        public Rectangle2D(Point2D p1, Point2D p2)
        {
            Point2D ul = new Point2D(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            Point2D ur = new Point2D(Math.Max(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            Point2D lr = new Point2D(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));
            Point2D ll = new Point2D(Math.Min(p1.X, p2.X), Math.Max(p1.Y, p2.Y));
            edges.Add(new Line2D(ul, ur));
            edges.Add(new Line2D(ur, lr));
            edges.Add(new Line2D(lr, ll));
            edges.Add(new Line2D(ll, ul));
        }

        public Rectangle2D(Point2D center, double width, double height)
        {
            Point2D tl = new Point2D(center.X - (width / 2), center.Y - (height / 2));
            Point2D tr = new Point2D(center.X + (width / 2), center.Y - (height / 2));
            Point2D bl = new Point2D(center.X - (width / 2), center.Y + (height / 2));
            Point2D br = new Point2D(center.X + (width / 2), center.Y + (height / 2));
            edges.Add(new Line2D(tl, tr));
            edges.Add(new Line2D(tr, br));
            edges.Add(new Line2D(br, bl));
            edges.Add(new Line2D(bl, tl));
        }
        #endregion

        #region Class Properties

        public List<Line2D> Edges { get { return edges; } set { edges = value; } }
        #endregion

        #region Class Methods
        public void Draw(Graphics gr)
        {
            foreach (Line2D edge in edges)
                edge.Draw(gr);
        }
        public void Rotate(double theta)
        {
            foreach (Line2D edge in edges)
                edge.Rotate(theta);
        }
        #endregion


    }
}
