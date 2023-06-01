using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSPointStorage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointTest
{
    [TestClass]
    public class PointRepositoryTests
    {
        private const string connectionString = "Data Source=192.168.243.101;Initial Catalog=master;Persist Security Info=True;User ID=SA;Password=yourStrong(!)Password";

        PointRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new PointRepository(connectionString, "Points");
        }

        [TestCleanup]
        public void Cleanup()
        {
            repository.DeleteRepository();
        }

        [TestMethod]
        public void TestRepositoryCreation()
        {}

        [TestMethod]
        public void TestStoringAndReadingSinglePoint()
        {
            var point = new Point(1, 2, 3);

            repository.Save(point);
            var retrievedPoint = repository.GetById(point.Id);


            Assert.AreEqual(point.X, retrievedPoint.X);
            Assert.AreEqual(point.Y, retrievedPoint.Y);
            Assert.AreEqual(point.Z, retrievedPoint.Z);
        }

        [TestMethod]
        public void TestStoringAndReadingMultiplePoints()
        {

            var points = new List<Point>
            {
                new Point(1, 2, 3, 1),
                new Point(4, 5, 6, 2),
                new Point(7, 8, 9, 3)
            };


            repository.Save(points);
            List<Point> retrievedPoints = repository.GetAll();

            Assert.AreEqual(points.Count, retrievedPoints.Count);
            foreach (var oldPoint in points)
            {
                bool found = false;
                foreach (var point in retrievedPoints)
                {
                    if (point.Id == oldPoint.Id)
                    {
                        Assert.AreEqual(point.X, oldPoint.X);
                        Assert.AreEqual(point.Y, oldPoint.Y);
                        Assert.AreEqual(point.Z, oldPoint.Z);

                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new Exception("Did not find one of the saved points!");
            }
        }

        [TestMethod]
        public void TestUpdatingPoint()
        {

            var point = new Point(1, 2, 3);


            repository.Save(point);
            point.X = 4;
            point.Y = 5;
            point.Z = 6;
            repository.Update(point);
            var retrievedPoint = repository.GetById(point.Id);


            Assert.AreEqual(point.X, retrievedPoint.X);
            Assert.AreEqual(point.Y, retrievedPoint.Y);
            Assert.AreEqual(point.Z, retrievedPoint.Z);
        }

        [TestMethod]
        public void TestRetrievingNonExistingPoint()
        {
            Assert.IsNull(repository.GetById(-1));
        }

        [TestMethod]
        public void TestDeletingAllPoints()
        {

            var points = new List<Point>
            {
                new Point(1, 2, 3, 1),
                new Point(4, 5, 6, 2),
                new Point(7, 8, 9, 3)
            };

            repository.Save(points);
            repository.DeleteAll();
            var retrievedPoints = repository.GetAll().ToList();


            Assert.AreEqual(0, retrievedPoints.Count);
        }

        [TestMethod]
        public void TestDeletingOnePoint()
        {
            var point = new Point(1, 2, 3);

            repository.Save(point);

            Assert.IsNotNull(repository.GetById(point.Id));

            repository.Delete(point);

            Assert.IsNull(repository.GetById(point.Id));
        }
    }
}
