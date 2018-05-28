using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using Paint.Utils;

namespace UnitTestProject.Utils
{
    [TestClass]
    public class GeometryUtilsTests
    {
        public void IsInPolygonTestFalse()
        {
            Point[] points = new Point[]
            {
                new Point(0,0),
                new Point(10,0),
                new Point(99,97),
                new Point(0,10)
            };
            Point point = new Point(5, 7);

            Assert.AreEqual(true, Geometry.IsInPolygon(points, point));

            point = new Point(-1, 2);

            Assert.AreEqual(false, Geometry.IsInPolygon(points, point));

        }

        public void IsInPolygonTestTrue()
        {
            Point[] points = new Point[]
            {
                new Point(0,0),
                new Point(10,0),
                new Point(99,97),
                new Point(0,10)
            };
            Point point = new Point(5, 7);

            Assert.AreEqual(true, Geometry.IsInPolygon(points, point));

            point = new Point(10, 0);

            Assert.AreEqual(true, Geometry.IsInPolygon(points, point));

        }
    }
}
