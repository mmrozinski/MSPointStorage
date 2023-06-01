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
    public class PointRepositoryUDT
    {
        private String ConnectionString { get; set; }

        public PointRepositoryUDT (String connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Save(PointUDT point)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Points (point) VALUES (@point)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@point", point) { UdtTypeName = "Point" });
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Save(List<PointUDT> pointList)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                foreach (PointUDT point in pointList)
                {
                    string query = "INSERT INTO Points (point) VALUES (@point)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@point", point) { UdtTypeName = "Point" });
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(PointUDT point)
        {

        }

        public void DeleteAll() 
        {

        }

        public PointUDT GetById(int id)
        {
            return new PointUDT();
        }

        public List<PointUDT> GetAll()
        {
            return new List<PointUDT>();
        }
    }
}
