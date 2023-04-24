using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PointTests
{
    [TestClass]
    public class PointTests
    {
        private const string connectionString = "Data Source=(local);Initial Catalog=TestDB;Integrated Security=True;";

        [TestMethod]
        public void TestStoringAndReadingSinglePoint()
        {

            var point = new Point(1, 2, 3);
            var repository = new PointRepository(connectionString);


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
            new Point(1, 2, 3),
            new Point(4, 5, 6),
            new Point(7, 8, 9)
        };
            var repository = new PointRepository(connectionString);


            repository.Save(points);
            var retrievedPoints = repository.GetAll().ToList();


            Assert.AreEqual(points.Count, retrievedPoints.Count);
            for (int i = 0; i < points.Count; i++)
            {
                Assert.AreEqual(points[i].X, retrievedPoints[i].X);
                Assert.AreEqual(points[i].Y, retrievedPoints[i].Y);
                Assert.AreEqual(points[i].Z, retrievedPoints[i].Z);
            }
        }

        [TestMethod]
        public void TestUpdatingPoint()
        {

            var point = new Point(1, 2, 3);
            var repository = new PointRepository(connectionString);


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

            var repository = new PointRepository(connectionString);

            Assert.ThrowsException<SqlException>(() => repository.GetById(-1));
        }

        [TestMethod]
        public void TestDeletingAllPoints()
        {

            var points = new List<Point>
            {
            new Point(1, 2, 3),
            new Point(4, 5, 6),
            new Point(7, 8, 9)
            };
            var repository = new PointRepository(connectionString);


            repository.Save(points);
            repository.DeleteAll();
            var retrievedPoints = repository.GetAll().ToList();


            Assert.AreEqual(0, retrievedPoints.Count);
        }

        [TestMethod]
        public void TestDistanceCalculation()
        {

            var point1 = new Point(1, 2, 3);
            var point2 = new Point(4, 5, 6);
            var expectedDistance = 5.196152422706632;


            var distance = Point.CalculateDistance(point1, point2);


            Assert.AreEqual(expectedDistance, distance, 0.00000001);
        }

        [TestMethod]
        public void TestDistanceCalculation()
        {
            // Arrange
            var point1 = new Point(1, 2, 3);
            var point2 = new Point(4, 5, 6);
            var expectedDistance = 5.196152422706632;

            // Act
            var distance = Point.CalculateDistance(point1, point2);

            // Assert
            Assert.AreEqual(expectedDistance, distance, 0.00000001);
        }

        [TestMethod]
        public void TestDistanceCalculationForSamePoint()
        {
            // Arrange
            var point = new Point(1, 2, 3);
            var expectedDistance = 0;

            // Act
            var distance = Point.CalculateDistance(point, point);

            // Assert
            Assert.AreEqual(expectedDistance, distance);
        }

        [TestMethod]
        public void TestDistanceCalculationWithNegativeCoordinates()
        {
            // Arrange
            var point1 = new Point(-1, -2, -3);
            var point2 = new Point(-4, -5, -6);
            var expectedDistance = 5.196152422706632;

            // Act
            var distance = Point.CalculateDistance(point1, point2);

            // Assert
            Assert.AreEqual(expectedDistance, distance, 0.00000001);
        }

        [TestMethod]
        public void TestDistanceCalculationWithLargeCoordinates()
        {
            // Arrange
            var point1 = new Point(1000, 2000, 3000);
            var point2 = new Point(4000, 5000, 6000);
            var expectedDistance = 5196.152422706632;

            // Act
            var distance = Point.CalculateDistance(point1, point2);

            // Assert
            Assert.AreEqual(expectedDistance, distance, 0.00000001);
        }
    }
}