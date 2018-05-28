using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paint.Shapes;
using System.Drawing;
using System.Collections.Generic;

namespace UnitTestProject.Shapes
{
    [TestClass]
    public class RectangleUnitTest
    {
        [TestMethod]
        public void CenterTest()
        {
            Paint.Shapes.Rectangle rectangle = new Paint.Shapes.Rectangle
            {
                Points = new List<Point> { new Point(0, 0), new Point(10, 0), new Point(0, 4), new Point(10, 4) }
            };
            var center = rectangle.Center();
            Assert.AreEqual(5, center.X);
            Assert.AreEqual(2, center.Y);
        }

        [TestMethod()]
        public void AddPointTest()
        {
            Paint.Shapes.Rectangle rectangle = new Paint.Shapes.Rectangle();
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(true, rectangle.AddPoint(new Point(0, i)));
            }
            Assert.IsFalse(rectangle.IsCompleted());
            Assert.AreEqual(false, rectangle.AddPoint(new Point(0, 1234)));
            Assert.IsTrue(rectangle.IsCompleted());
        }
    }
}
