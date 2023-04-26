using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Game2D;

namespace Graphics3D
{
    class Polygon3D
    {
        #region Parameters
        List<Line3D> edges = new List<Line3D>();
        List<Point3D> pts = new List<Point3D>();
        Brush brFront = Brushes.Crimson;
        Brush brBack = Brushes.Gold;
        #endregion

        #region Constructors
        public Polygon3D()
        {

        }
        #endregion

        #region Properties
        public List<Line3D> Edges { get { return edges; } }

        public bool Visible { get; set; } = true;

        public Point3D MidPoint
        {
            get
            {
                Point3D temp = new Point3D();

                foreach(Point3D p in pts)
                    temp += p;
                return temp / pts.Count;
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Methods
        public void Draw(Graphics gr, double distance)
        {
            foreach (Line3D edge in edges)
                edge.Draw(gr, distance);

            Polygon2D poly2D = new Polygon2D();
            foreach (Point3D pt in pts)
                poly2D.AddPt(pt.Projection(distance));
        }

        public void Fill(Graphics gr, double distance, Face whichFace, bool showOutline)
        {
            if (!Visible)
                return;

            Polygon2D poly2D = new Polygon2D();
            foreach (Point3D pt in pts)
                poly2D.AddPt(pt.Projection(distance));
            if (poly2D.Face == whichFace)
            {
                if (poly2D.Face == Face.front)
                {
                    poly2D.Fill(gr, brFront);
                    if (showOutline)
                        poly2D.Draw(gr);


                }
                else
                {
                    poly2D.Fill(gr, brBack);

                    if (showOutline)
                        poly2D.Draw(gr);

                    
                }
            }

        }
        
        
        

        

        /// <summary>
        /// add a point to the polygon
        /// </summary>
        /// <param name="pt"></param>
        public void AddPt(Point3D pt)
        {
            pts.Add(pt);
            if (pts.Count >= 3)
            {
                edges.Clear();
                for (int i = 0; i < pts.Count; i++)
                {
                    edges.Add(new Line3D(pts[i], pts[(i + 1) % pts.Count]));
                }
            }
        }

        /// <summary>
        /// Rotate the polygon by 3D point theta
        /// </summary>
        /// <param name="theta"></param>
        public void Rotate(Point3D theta)
        {
            foreach (Line3D edge in edges)
                edge.Rotate(theta);
            foreach (Point3D point in pts)
                point.Rotate(theta);
        }

        /// <summary>
        /// UnRotate the polygon by the 3D point theta
        /// </summary>
        /// <param name="theta"></param>
        public void UnRotate(Point3D theta)
        {
            foreach (Line3D edge in edges)
                edge.UnRotate(theta);
            foreach (Point3D point in pts)
                point.UnRotate(theta);
        }

        public void Scale(double amount)
        {
            for (int i = 0; i < pts.Count; i++)
                pts[i] *= amount;

            for (int i = 0; i < edges.Count; i++)
                edges[i].Scale(amount);
        }
        #endregion
    }
}
