using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace MSPointStorage
{
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
    public struct Point : INullable
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        private bool is_Null;

        public bool IsNull
        {
            get
            {
                return (is_Null);
            }
        }

        public static Point Null
        {
            get
            {
                Point pt = new Point();
                pt.is_Null = true;
                return pt;
            }
        }

        public Point(double x, double y, double z, int id = 0)
        {
            X = x;
            Y = y;
            Z = z;
            Id = id;
            is_Null = false;
        }

        [SqlMethod(OnNullCall = false)]
        public static Point Parse(SqlString s)
        {
            if (s.IsNull)
                return Null;

            // Parse input string to separate out points.  
            Point pt = new Point();
            string[] xyz = s.Value.Split(",".ToCharArray());
            pt.X = Double.Parse(xyz[0]);
            pt.Y = Double.Parse(xyz[1]);
            pt.Z = Double.Parse(xyz[2]);
            return pt;
        }

        public override string ToString()
        {
            if (this.IsNull)
                return "NULL";
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(X);
                builder.Append(',');
                builder.Append(Y);
                builder.Append(',');
                builder.Append(Z);
                return builder.ToString();
            }
        }

        public static double CalculateDistance(Point origin, Point destination)
        {
            return Math.Sqrt(Math.Pow(destination.X - origin.X, 2) + Math.Pow(destination.Y - origin.Y, 2) + Math.Pow(destination.Z - origin.Z, 2));
        }
    }
}
