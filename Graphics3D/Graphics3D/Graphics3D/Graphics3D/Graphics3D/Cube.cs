using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2D;
using System.Drawing;

namespace Graphics3D
{
    enum Side { front = 0, back = 1, left = 2, right = 3, top = 4, bottom = 5 }
    class Cube
    {
        #region Parameters
        List<Point3D> corners = new List<Point3D>();
        List<Line3D> edges = new List<Line3D>();
        List<Polygon3D> faces = new List<Polygon3D>();
        #endregion

        #region Constructors
        /// <summary>
        /// Create a basic cube, centered at the origin, of size 1x1x1
        /// </summary>
        public Cube()
        {
            for (int i = -1; i <= 1; i += 2)
                for (int j = -1; j <= 1; j += 2)
                    for (int k = -1; k <= 1; k += 2)
                        corners.Add(new Point3D(i, j, k));
            for (int i = 0; i < corners.Count; i++)
                for (int j = i + 1; j < corners.Count; j++)
                    if ((corners[i] - corners[j]).Magnitude == 2)
                        edges.Add(new Line3D(corners[i], corners[j]));
            Polygon3D face;

            //make the front face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, -1, 1));
            face.AddPt(new Point3D(1, -1, 1));
            face.AddPt(new Point3D(1, 1, 1));
            face.AddPt(new Point3D(-1, 1, 1));
            faces.Add(face);
            face.Visible = false;
            //make the back face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, -1, -1));
            face.AddPt(new Point3D(-1, 1, -1));
            face.AddPt(new Point3D(1, 1, -1));
            face.AddPt(new Point3D(1, -1, -1));
            faces.Add(face);
            //make the left face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, -1, 1));
            face.AddPt(new Point3D(-1, 1, 1));
            face.AddPt(new Point3D(-1, 1, -1));
            face.AddPt(new Point3D(-1, -1, -1));
            faces.Add(face);
            //make the right face
            face = new Polygon3D();
            face.AddPt(new Point3D(1, -1, 1));
            face.AddPt(new Point3D(1, -1, -1));
            face.AddPt(new Point3D(1, 1, -1));
            face.AddPt(new Point3D(1, 1, 1));
            faces.Add(face);
            //make the top face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, -1, 1));
            face.AddPt(new Point3D(-1, -1, -1));
            face.AddPt(new Point3D(1, -1, -1));
            face.AddPt(new Point3D(1, -1, 1));
            faces.Add(face);
            //make the bottom face
            face = new Polygon3D();
            face.AddPt(new Point3D(-1, 1, 1));
            face.AddPt(new Point3D(1, 1, 1));
            face.AddPt(new Point3D(1, 1, -1));
            face.AddPt(new Point3D(-1, 1, -1));
            faces.Add(face);
        }
        #endregion

        #region Properties
        #endregion

        #region Operators
        #endregion

        #region Methods
        /// <summary>
        /// Show a specified face
        /// </summary>
        /// <param name="side"></param>
        public void ShowFace(Side side)
        {
            faces[(int)side].Visible = true;
        }
        /// <summary>
        /// Hide a specified face
        /// </summary>
        /// <param name="side"></param>
        public void HideFace(Side side)
        {
            faces[(int)side].Visible = false;
        }
        /// <summary>
        /// UnRotate the cube by the 3dimensional angle theta
        /// </summary>
        /// <param name="theta"></param>
        public void UnRotate(Point3D theta)
        {
            for (int i = 0; i < corners.Count; i++)
                corners[i].UnRotate(theta);
            for (int i = 0; i < edges.Count; i++)
                edges[i].UnRotate(theta);
            foreach (Polygon3D face in faces)
                face.UnRotate(theta);
        }
        /// <summary>
        /// Rotate the cube by the 3dimensional angle theta
        /// </summary>
        /// <param name="theta"></param>
        public void Rotate(Point3D theta)
        {
            for (int i = 0; i < corners.Count; i++)
                corners[i].Rotate(theta);
            for (int i = 0; i < edges.Count; i++)
                edges[i].Rotate(theta);
            foreach (Polygon3D face in faces)
                face.Rotate(theta);
        }
        public void Scale(double amount)
        {
            for (int i = 0; i < corners.Count; i++)
                corners[i] *= amount;
            for (int i = 0; i < edges.Count; i++)
                edges[i].Scale(amount);
            for (int i = 0; i < faces.Count; i++)
                faces[i].Scale(amount);
        }

        public void Draw(Graphics gr, double distance, Face whichFace)
        {
            foreach (Polygon3D face in faces)
                face.Fill(gr, distance, whichFace, true);
        }
        /// <summary>
        /// Bounce the ball off the inside sides of the cube.
        /// </summary>
        /// <param name="ball"></param>
        /// <returns></returns>
        public bool Bounce(Ball3D ball)
        {
            bool didBounce = false;
            // left side bounce
            if (ball.X - ball.Radius < faces[(int)Side.left].MidPoint.X)
            {
                ball.X += (faces[(int)Side.left].MidPoint.X + ball.Radius - ball.X);
                ball.Velocity.X *= -1 * ball.Elesticity;
            }

            //right side bounce
            if (ball.X + ball.Radius > faces[(int)Side.right].MidPoint.X)
            {
                ball.X += (faces[(int)Side.right].MidPoint.X - ball.Radius - ball.X);
                ball.Velocity.X *= -1 * ball.Elesticity;
            }

            //back side bounce
            if (ball.Z - ball.Radius < faces[(int)Side.back].MidPoint.Z)
            {
                ball.Z += (faces[(int)Side.back].MidPoint.Z + ball.Radius - ball.Z);
                ball.Velocity.Z *= -1 * ball.Elesticity;
            }

            //front side bounce
            if (ball.Z + ball.Radius > faces[(int)Side.front].MidPoint.Z)
            {
                ball.Z += (faces[(int)Side.front].MidPoint.Z - ball.Radius - ball.Z);
                ball.Velocity.Z *= -1 * ball.Elesticity;
            }

            
            //back top bounce
            if (ball.Y - ball.Radius < faces[(int)Side.top].MidPoint.Y)
            {
                ball.Y += (faces[(int)Side.top].MidPoint.Y + ball.Radius - ball.Y);
                ball.Velocity.Y *= -1 * ball.Elesticity;
            }

            //front bottom bounce
            if (ball.Y + ball.Radius > faces[(int)Side.bottom].MidPoint.Y)
            {
                ball.Y += (faces[(int)Side.bottom].MidPoint.Y - ball.Radius - ball.Y);
                ball.Velocity.Y *= -1 * ball.Elesticity;
            }
            
            return didBounce;
        }
        #endregion

    }
}
