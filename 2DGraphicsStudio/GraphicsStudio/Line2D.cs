using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsStudio
{
    class Line2D
    {
        #region Class Parameters
        Ball2D[] endPoints = new Ball2D[2] { new Ball2D(), new Ball2D()};
        Pen pen = new Pen(Color.Violet);
        
        
        #endregion

        #region Class Constructors
        /// <summary>
        /// Construct a line with the specified end points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public Line2D(Point2D p1, Point2D p2)
        {
            endPoints[0].Center = p1;
            endPoints[1].Center = p2;
            endPoints[0].Mass = double.MaxValue;
            endPoints[1].Mass = double.MaxValue;
        }
        #endregion

        #region Class Properties
        public Pen Pen
        {
            get { return pen; }
            set { pen = value; }
        }

        public double Length { get { return(endPoints[0] - endPoints[1]).Magnitude; } }
        #endregion

        #region Class Methods
        public void Draw(Graphics gr)
        {
            gr.DrawLine(Pen, endPoints[0].ToPointF(), endPoints[1].ToPointF());
        }

        public void Rotate(double theta)
        {
            foreach (Point2D point in endPoints)
                point.Rotate(theta);
        }
        #endregion

        #region Line - Ball Bounce Code
        /// <summary>
        /// Determine the line that is normal from this line to a ball center
        /// </summary>
        /// <param name="ball"></param>
        /// <returns></returns>
        public Point2D NormalToBall(Ball2D ball)
        {
            Point2D v = endPoints[0] - endPoints[1];
            Point2D ballToLine = ball - endPoints[0];
            Point2D normal = v.Normal;
            // make normal a unit vector
            normal.Normalize();
            if (normal * ballToLine < 0)
                normal *= -1;
            return normal;
        }

        /// <summary>
        /// Determine if the bounding rectangle of the line contains point p;
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(Point2D p)
        {
            RectangleF boundingRect = new RectangleF(
                (float)Math.Min(endPoints[0].X, endPoints[1].X) - 0.5f,
                (float)Math.Min(endPoints[0].Y, endPoints[1].Y) - 0.5f,
                (float)Math.Abs(endPoints[0].X - endPoints[1].X) + 1f,
                (float)Math.Abs(endPoints[0].Y - endPoints[1].Y) + 1f);
            return boundingRect.Contains(p.ToPointF());
        }
        /// <summary>
        /// Determines the points of intersection between this line and another line (otherLine)
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public Point2D LineIntersectionPoint(Line2D otherLine)
        {
            // Get A,B,C of first line - points : endPoints[0] to endPoints[1]
            double A1 = endPoints[1].Y - endPoints[0].Y;
            double B1 = endPoints[0].X - endPoints[1].X;
            double C1 = A1 * endPoints[0].X + B1 * endPoints[0].Y;

            // Get A,B,C of second line - points : ps2 to pe2
            double A2 = otherLine.endPoints[1].Y - otherLine.endPoints[0].Y;
            double B2 = otherLine.endPoints[0].X - otherLine.endPoints[1].X;
            double C2 = A2 * otherLine.endPoints[0].X + B2 * otherLine.endPoints[0].Y;

            // Get delta and check if the lines are parallel
            double delta = A1 * B2 - A2 * B1;
            if (delta == 0)
                return new Point2D(double.MaxValue, double.MaxValue);
            //throw new System.Exception("Lines are parallel");

            // now return the Vector2 intersection point
            return new Point2D(
                (B2 * C1 - B1 * C2) / delta,
                (A1 * C2 - A2 * C1) / delta
            );
        }
        /// <summary>
        /// Computes the reflected segment at a point of a curve
        /// The line is the segment being reflected
        /// The normal is the normal of the reflection line and the ball
        /// </summary>
        /// <param name="normal"></param>
        /// <returns></returns>
        public Point2D Reflection(Point2D normal)
        {
            double rx, ry;
            Point2D direction = endPoints[1] - endPoints[0];
            double dot = direction * normal;
            rx = direction.X - 2 * normal.X * dot;
            ry = direction.Y - 2 * normal.Y * dot;
            return new Point2D(rx, ry);
        }

        public bool Bounce(Ball2D ball)
        {
            // determine the normal vector from the line to the ball
            Point2D normal = NormalToBall(ball);
            // make a temporary line of this line moved one radius towards the ball
            // this allows us to "bounce of the center"
            Line2D aLineTemp = new Line2D(endPoints[0] + normal * ball.Radius,
                endPoints[1] + normal * ball.Radius);


            // we need to know where the ball will be in one step
            Point2D ballNextStep = ball + ball.Velocity;
            // make a line from where the ball is now to the location at the next step
            Line2D ballPath = new Line2D(ball, ballNextStep);

            // find the point of intersection between the line and the path of the ball
            Point2D intersectionPt = aLineTemp.LineIntersectionPoint(ballPath);
            // perform the bounce if necessary

            // bounce off the endpoints if necessary
            if (endPoints[0].IsColliding(ball))
            {
                endPoints[0].Bounce(ball);
                return true;
            }
            else if (endPoints[1].IsColliding(ball))
            {
                endPoints[1].Bounce(ball);
                return true;
            }
            // if the point of intersection is within the line segment 
            // AND the ball is moving towards the line
            else if (ball.Velocity.Magnitude < (intersectionPt - ball).Magnitude
                && normal * ball.Velocity < 0)
                return false;
            else if (aLineTemp.Contains(intersectionPt)
                && normal * ball.Velocity < 0)
            {
                Line2D reflectionLine = new Line2D(ball + ball.Velocity, intersectionPt);
                if(reflectionLine.Length == 0)
                    reflectionLine = new Line2D(ball + ball.Velocity * 1.0001, intersectionPt);
                Point2D velocityDirection = -1 * reflectionLine.Reflection(NormalToBall(ball));
                velocityDirection.Normalize();
                ball.Velocity = velocityDirection * ball.Velocity.Magnitude;
                Point2D ballLocation =
                    intersectionPt -
                    reflectionLine.Reflection(NormalToBall(ball)) * ball.Elesticity;
                ball.X = ballLocation.X;
                ball.Y = ballLocation.Y;

                return true;
            }
            return false;
        }
        #endregion


    }
}
