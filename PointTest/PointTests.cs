using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSPointStorage;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;

namespace PointTests
{
    [TestClass]
    public class PointTests
    {
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
        public void TestDistanceCalculationForSamePoint()
        {
            var point = new Point(1, 2, 3);
            var expectedDistance = 0;

            var distance = Point.CalculateDistance(point, point);

            Assert.AreEqual(expectedDistance, distance);
        }

        [TestMethod]
        public void TestDistanceCalculationWithNegativeCoordinates()
        {
            var point1 = new Point(-1, -2, -3);
            var point2 = new Point(-4, -5, -6);
            var expectedDistance = 5.196152422706632;

            var distance = Point.CalculateDistance(point1, point2);

            Assert.AreEqual(expectedDistance, distance, 0.00000001);
        }

        [TestMethod]
        public void TestDistanceCalculationWithLargeCoordinates()
        {
            var point1 = new Point(1000, 2000, 3000);
            var point2 = new Point(4000, 5000, 6000);
            var expectedDistance = 5196.152422706632;

            var distance = Point.CalculateDistance(point1, point2);

            Assert.AreEqual(expectedDistance, distance, 0.00000001);
        }

        [TestMethod]
        public void TestCubeVolumeCalculation() 
        {
            var point1 = new Point(0, 0, 0);
            var point2 = new Point(1, 1, 1);

            var expectedDistance = 1.0;

            var distance = Point.CalculateCubeVolume(point1, point2);

            Assert.AreEqual(expectedDistance, distance, 0.00000001);
        }
    }
}