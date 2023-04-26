using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game2D
{
    class Ball2D : Circle2D
    {
        #region Class Parameters
        Point2D velocity = new Point2D();
        double mass = 0;
        Pen pen = Pens.Gold;
        Brush brush = Brushes.Gold;
        double friction = .995;
        double elasticity = .995;

        #endregion

        #region Class Constructors
        /// <summary>
        /// Default Ball constructor
        /// </summary>
        public Ball2D() { }

        public Ball2D(Point2D center, double radius)
        {
            Center = center;
            Radius = radius;
            mass = radius * radius;
        }
        public Ball2D(Point2D center, double radius, Point2D velocity)
        {
            Center = center;
            Radius = radius;
            this.velocity = velocity;
            mass = radius * radius;
        }
        #endregion

        #region Class Properties
        //get/set for  vel, pen, br, mass, fric, elas
        /// <summary>
        /// Get/Set Mass
        /// </summary>
        public double Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        /// <summary>
        /// Get/Set Pen
        /// </summary>
        public Pen Pen
        {
            get { return pen; }
            set { pen = value; }
        }

        /// <summary>
        /// Get/Set Friction
        /// </summary>
        public double Friction
        {
            get { return friction; }
            set { friction = value; }
        }

        /// <summary>
        /// Get/Set brush
        /// </summary>
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        public Point2D Velocity
        {
            get { return velocity; }

            set
            {
                velocity = value;
                if (velocity.Magnitude > Radius * 0.8)
                {
                    velocity.Normalize();
                    velocity *= .8 * Radius;
                }
            }
        }

        /// <summary>
        /// Get/Set Elesticity
        /// </summary>
        public double Elesticity
        {
            get { return elasticity; }
            set { elasticity = value; }
        }

        #endregion

        #region Class Methods
        public void Move()
        {
            Center += Velocity;
        }

        public void Draw(Graphics gr)
        {
            this.Draw(gr, Pen);
        }

        public void Fill(Graphics gr)
        {
            this.Fill(gr, Brush);
            this.Draw(gr, Pen);
        }

        public void Draw(Graphics gr, Brush brush)
        {
            this.Draw(gr, Pen);
            this.Fill(gr, brush);
            
        }

        /// <summary>
        /// Bounce this ball off of another ball
        /// </summary>
        /// <param name="otherBall"></param>
        public void Bounce(Ball2D otherBall)
        {
            if (!IsColliding(otherBall))
                return;

            Point2D difference = this - otherBall;
            double distance = difference.Magnitude;
            // mtd = minimum translation distance
            // we fudge the mtd by a small factor 1.1 to force them to move apart by at least slight gap
            Point2D mtd = difference * (this.Radius + otherBall.Radius - distance) / distance * 1.1;

            // get the reciprocal of the masses
            double thisMassReciprocal = 1 / Mass;
            double otherMassReciprocal = 1 / otherBall.Mass;

            // push the balls apart by the minimum translation distance
            Point2D center = mtd * (thisMassReciprocal / (thisMassReciprocal + otherMassReciprocal));
            this.X += center.X;
            this.Y += center.Y;
            Point2D otherBallCenter = mtd * (otherMassReciprocal / (thisMassReciprocal + otherMassReciprocal));
            otherBall.X -= otherBallCenter.X;
            otherBall.Y -= otherBallCenter.Y;

            // now we "normalize" the mtd to get a unit vector of length 1 in the mtd direction
            mtd.Normalize();

            // impact the velocity due to the collision
            Point2D v = this.Velocity - otherBall.Velocity;
            double vDotMtd = v * mtd;
            if (double.IsNaN(vDotMtd))
                return;
            if (vDotMtd > 0)
                return; // the balls are already moving in opposite direction

            // work the collision effect
            double i = -(1 + Elesticity) * vDotMtd / (thisMassReciprocal + otherMassReciprocal);
            Point2D impulse = mtd * i;

            // change the balls velocities
            this.Velocity += impulse * thisMassReciprocal;
            otherBall.Velocity -= impulse * otherMassReciprocal;
        }
        /// <summary>
        /// Determines if this ball is colliding with the other ball
        /// </summary>
        /// <param name="otherball"></param>
        /// <returns>true if colliding, false otherwise</returns>
        public bool IsColliding(Ball2D otherball)
        {
            return ((this.Center - otherball.Center).Magnitude < this.Radius + otherball.Radius);
        }

        #endregion
    }
}
