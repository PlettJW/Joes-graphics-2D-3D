using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    class Sphere : Point3D
    {
        #region Parameters
        #endregion

        #region Class Constructors
        /// <summary>
        /// Create a defult circle, at (0,0) with 0 radius
        /// </summary>
        public Sphere() { }

        /// <summary>
        /// Construct a circle with specified x,y,radius values
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Sphere(Point3D center, double radius)
        {
            X = center.X;
            Y = center.Y;
            Z = center.Z;
            Radius = radius;
        }

        public Sphere(double x, double y, double z,double radius)
        {
            X = y;
            Y = y;
            Z = Z;
           Radius = radius;
        }
        #endregion

        #region Properties
        public double Radius { get; set; } = 0;

        #endregion

        #region Methods

        #endregion
    }
}
