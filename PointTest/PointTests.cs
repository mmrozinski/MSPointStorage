using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSPointStorage;

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

        [TestMethod]
        public void TestInRadiusCheck()
        {
            var point1 = new Point(0, 0, 0);
            var point2 = new Point(1, 1, 1);
            var radiusInRange = 2;
            var radiusNotInRange = 0.5;

            Assert.IsTrue(Point.IsInSphere(point1, point2, radiusInRange));

            Assert.IsFalse(Point.IsInSphere(point1, point2, radiusNotInRange));
        }

        [TestMethod]
        public void TestInCubeCheck()
        {
            var point1 = new Point(0, 0, 0);
            var point2 = new Point(1, 1, 1);
            var pointInCube = new Point(0.5, 0.5, 0.5);
            var pointNotInCube = new Point(-1, -1, -1);

            Assert.IsTrue(Point.IsInBox(pointInCube, point1, point2));

            Assert.IsFalse(Point.IsInBox(pointNotInCube, point1, point2));
        }
    }
}