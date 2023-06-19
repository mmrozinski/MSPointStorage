using System.Data.SqlClient;

namespace MSPointStorage
{
    public class PointRepository
    {
        /// <summary>
        /// This property is the connection string used to connect to the database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// This property is the name of a table in the database specified by <see cref="ConnectionString"/>.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// This constructor creates a new <see cref="PointRepository">PointRepository</see> and, if necessary, creates a new table with the specified
        /// <paramref name="name"/> in the database pointed to by <paramref name="connectionString"/>.
        /// </summary>
        /// <param name="connectionString">The connection string pointing to the database to use for point storage.</param>
        /// <param name="name">The name of the table to store the points in.</param>
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

        /// <summary>
        /// This method saves the given <paramref name="point"/> to the database
        /// </summary>
        /// <param name="point">The point to save.</param>
        public void Save(Point point)
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name)) 
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

            if (point is null)
            {
                throw new ArgumentNullException();
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"INSERT INTO {Name} (id, x, y, z) VALUES (@id, @x, @y, @z)";

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

        /// <summary>
        /// This method saves all points in a list to the database
        /// </summary>
        /// <param name="pointList">The list of points to save.</param>
        public void Save(List<Point> pointList)
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name))
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

            if (pointList is null)
            {
                throw new ArgumentNullException();
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"INSERT INTO {Name} (id, x, y, z) VALUES (@id, @x, @y, @z)";

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

        /// <summary>
        /// This method updates an already existing point from the <see cref="PointRepository"/>.
        /// </summary>
        /// <param name="point">The point with an id parameter matching the id parameter of a point in the database and updated coordinates.</param>
        public void Update(Point point)
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name))
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

            if (point is null)
            {
                throw new ArgumentNullException();
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"UPDATE {Name} SET x = @x, y = @y, z = @z WHERE id = @id";

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

        /// <summary>
        /// This method removes all points from the repository.
        /// </summary>
        public void DeleteAll()
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name))
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

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

        /// <summary>
        /// This method removes a point with an id equal to the provided point's from the <see cref="PointRepository"/>.
        /// </summary>
        /// <remarks>
        /// Note: This method ONLY checks if the ids are matching. Do not try to use this to remove points 
        /// with specific coordinates.
        /// </remarks>
        /// <param name="point">The point to remove.</param>
        public void Delete(Point point)
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name))
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

            if (point is null)
            {
                throw new ArgumentNullException();
            }

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

        /// <summary>
        /// This method drops the table associated with this <see cref="PointRepository"/>.
        /// </summary>
        public void DeleteRepository()
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name))
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

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

        /// <summary>
        /// This method is used to retrieve a point from the <see cref="PointRepository"/> with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the point to find.</param>
        /// <returns>If a matching id was found, a <see cref="Point"/> object with the coordinates and id obtained from the repository, otherwise <see langword="null"/></returns>
        public Point? GetById(int id)
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name))
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

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

        /// <summary>
        /// This method is used to obtain a <see cref="List{T}"/> containing all points saved in the <see cref="PointRepository"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Point"/> objects, containing all points saved in the <see cref="PointRepository"/></returns>
        public List<Point> GetAll()
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Name))
            {
                throw new Exception("Both the ConnectionString and the Name must be initialized first!");
            }

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
