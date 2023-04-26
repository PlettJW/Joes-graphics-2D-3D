using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Game2D;

namespace Graphics3D
{
    class Line3D
    {
        #region Parameters
        Point3D[] endPts = new Point3D[2] { new Point3D(), new Point3D() };
        #endregion

        #region Constructors
        public Line3D() { }

        public Line3D(Point3D p1, Point3D p2)
        {
            endPts[0] = p1;
            endPts[1] = p2;
        }
        #endregion

        #region Properties

        public Point3D[] Endpoints { get { return endPts; } }
        #endregion

        #region Methods
        public void Draw(Graphics gr, double distance)
        {
            Line2D line2D = new Line2D(endPts[0].Projection(distance), endPts[1].Projection(distance));
            line2D.Draw(gr);
        }

        public void Scale(double amount)
        {
            endPts[0] *= amount;
            endPts[1] *= amount;
        }

        public void Rotate(Point3D theta)
        {
            foreach (Point3D point in endPts)
                point.Rotate(theta);
        }

        public void UnRotate(Point3D theta)
        {
            foreach (Point3D point in endPts)
                point.UnRotate(theta);
        }
        #endregion
    }
}
