using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPointStorage
{
    public class Point
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }


        public Point(double x, double y, double z, int id = 0)
        {
            X = x;
            Y = y;
            Z = z;
            Id = id;
        }

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

        public static double CalculateDistance(Point origin, Point destination)
        {
            return Math.Sqrt(Math.Pow(destination.X - origin.X, 2) + Math.Pow(destination.Y - origin.Y, 2) + Math.Pow(destination.Z - origin.Z, 2));
        }
    }
}
