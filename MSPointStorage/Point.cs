using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPointStorage
{
    public class Point
    {
        /// <summary>
        /// This property represents the <see cref="Point"/>'s Id field in the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This property represents the value of the <see cref="Point"/>'s X coordinate.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// This property represents the value of the <see cref="Point"/>'s X coordinate.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// This property represents the value of the <see cref="Point"/>'s X coordinate.
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// This constructor creates a new <see cref="Point"/> object with specified coorinates and id.
        /// </summary>
        /// <param name="x">The <see cref="Point"/>'s X coordinate.</param>
        /// <param name="y">The <see cref="Point"/>'s Y coordinate.</param>
        /// <param name="z">The <see cref="Point"/>'s Z coordinate.</param>
        /// <param name="id">The <see cref="Point"/>'s id.</param>
        public Point(double x, double y, double z, int id = 0)
        {
            X = x;
            Y = y;
            Z = z;
            Id = id;
        }

        /// <summary>
        /// This method is used to obtain the text representation of a <see cref="Point"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> containing the text representation of the <see cref="Point"/> in the format "<c>id:X,Y,Z</c>".</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Id);
            builder.Append(':');
            builder.Append(X);
            builder.Append(',');
            builder.Append(Y);
            builder.Append(',');
            builder.Append(Z);
            return builder.ToString();
        }

        /// <summary>
        /// This static method is used to calculate the distance between two points.
        /// </summary>
        /// <param name="origin">The first of the points.</param>
        /// <param name="destination">The second of the points.</param>
        /// <returns>The euclidean distance between <paramref name="origin"/> and <paramref name="destination"/></returns>
        public static double CalculateDistance(Point origin, Point destination)
        {
            return Math.Sqrt(Math.Pow(destination.X - origin.X, 2) + Math.Pow(destination.Y - origin.Y, 2) + Math.Pow(destination.Z - origin.Z, 2));
        }

        /// <summary>
        /// This static method is used to calculate the volume of a rectangular cuboid specified by its two opposite vertices.
        /// </summary>
        /// <param name="vertex1">The first of the cuboid's opposite vertices.</param>
        /// <param name="vertex2">The second of the cuboid's opposite vertices.</param>
        /// <returns>The value of the cuboid's volume.</returns>
        public static double CalculateCubeVolume(Point vertex1, Point vertex2)
        {
            return Math.Abs(vertex1.X - vertex2.X) * Math.Abs(vertex1.Y - vertex2.Y) * Math.Abs(vertex1.Z - vertex2.Z);
        }

        /// <summary>
        /// This static method checks whether a <paramref name="point"/> is within a certain <paramref name="radius"/> from a sphere's <paramref name="center"/>.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <param name="center">The center of the sphere.</param>
        /// <param name="radius">The radius of the sphere.</param>
        /// <returns><see langword="true"/> if the <paramref name="point"/>'s distance from the <paramref name="center"/> 
        /// is less or equal to the <paramref name="radius"/>, <see langword="false"/> otherwise</returns>.
        public static bool IsInSphere(Point point, Point center, double radius)
        {
            return Point.CalculateDistance(point, center) <= radius;
        }
    }
}
