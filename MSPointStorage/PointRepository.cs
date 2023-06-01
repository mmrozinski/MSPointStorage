using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSPointStorage
{
    public class PointRepository
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        

        public PointRepository(string connectionString, string name)
        {
            ConnectionString = connectionString;
            Name = name;

            string creationQuery = "IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id) WHERE" +
                $" s.name = 'dbo' AND t.name = '{name}') " +
                $"CREATE TABLE dbo.{name} (id int NOT NULL PRIMARY KEY, x float, y float, z float)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(creationQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Save(Point point)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Points (id, x, y, z) VALUES (@id, @x, @y, @z)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", point.Id);
                    command.Parameters.AddWithValue("@x", point.X);
                    command.Parameters.AddWithValue("@y", point.Y);
                    command.Parameters.AddWithValue("@z", point.Z);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Save(List<Point> pointList)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Points (id, x, y, z) VALUES (@id, @x, @y, @z)";

                foreach (Point point in pointList)
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", point.Id);
                        command.Parameters.AddWithValue("@x", point.X);
                        command.Parameters.AddWithValue("@y", point.Y);
                        command.Parameters.AddWithValue("@z", point.Z);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(Point point)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Points SET x = @x, y = @y, z = @z WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", point.Id);
                    command.Parameters.AddWithValue("@x", point.X);
                    command.Parameters.AddWithValue("@y", point.Y);
                    command.Parameters.AddWithValue("@z", point.Z);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAll()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"DELETE FROM {Name}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Point point)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"DELETE FROM {Name} WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", point.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRepository()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"DROP TABLE {Name}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public Point? GetById(int id)
        {
            int foundId;
            double x, y, z;
            x = y = z = foundId = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM {Name} WHERE id='{id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            x = Double.Parse(String.Format("{0}", reader["x"]));
                            y = Double.Parse(String.Format("{0}", reader["y"]));
                            z = Double.Parse(String.Format("{0}", reader["z"]));
                            foundId = Int32.Parse(String.Format("{0}", reader["id"]));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            return new Point(x, y, z, foundId);
        }

        public List<Point> GetAll()
        {
            List<Point> points = new List<Point>();

            int foundId;
            double x, y, z;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM {Name}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            x = Double.Parse(String.Format("{0}", reader["x"]));
                            y = Double.Parse(String.Format("{0}", reader["y"]));
                            z = Double.Parse(String.Format("{0}", reader["z"]));
                            foundId = Int32.Parse(String.Format("{0}", reader["id"]));

                            points.Add(new Point(x, y, z, foundId));
                        }
                    }
                }
            }

            return points;
        }
    }
}
