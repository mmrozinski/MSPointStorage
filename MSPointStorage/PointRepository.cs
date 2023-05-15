using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPointStorage
{
    public class PointRepository
    {
        private String ConnectionString { get; set; }

        public PointRepository (String connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Save(Point point)
        {

        }

        public void Save(List<Point> pointList)
        {
            
        }

        public void Update(Point point)
        {

        }

        public void DeleteAll() 
        {

        }

        public Point GetById(int id)
        {
            return new Point();
        }

        public List<Point> GetAll()
        {
            return new List<Point>();
        }
    }
}
