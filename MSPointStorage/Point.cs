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
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="id"></param>
        public Point(double x, double y, double z, int id = 0)
        {
            X = x;
            Y = y;
            Z = z;
            Id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(X);
            builder.Append(',');
            builder.Append(Y);
            builder.Append(',');
            builder.Append(Z);
            return builder.ToString();
        }

        /// <summary>
        /// sasaa
        /// </summary>
        /// <param name="origin">swdweew</param>
        /// <param name="destination">dewewdwwqq</param>
        /// <returns>
        /// asas
        /// </returns>
        public static double CalculateDistance(Point origin, Point destination)
        {
            return Math.Sqrt(Math.Pow(destination.X - origin.X, 2) + Math.Pow(destination.Y - origin.Y, 2) + Math.Pow(destination.Z - origin.Z, 2));
        }
    }
}
