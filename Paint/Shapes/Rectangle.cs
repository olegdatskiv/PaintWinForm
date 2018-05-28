using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paint.Interfaces;
using System.Drawing;

namespace Paint.Shapes
{
    [Serializable]
    public class Rectangle : IShapes
    {
        public const uint SIZE = 4;
        public string Name { get; set; }
        public List<Point> Points { get; set; }
        public Color Color { get; set; }
        public int RgbaColor { get; set; }

        public Rectangle()
        {
            Points = new List<Point>();
            Color = Color.Aqua;
        }

        public Rectangle(params Point[] points)
        {
            if (points.Count() != Rectangle.SIZE)
            {
                throw new ArgumentException(string.Format("Rectangle must contain only {0} points", Rectangle.SIZE));
            }

            Points = new List<Point>(points);
        }

        public int Count()
        {
            return Points.Count();
        }

        public bool IsCompleted()
        {
            return Points.Count() == Rectangle.SIZE && Points.TrueForAll(p => p != null);
        }

        public bool AddPoint(Point point)
        {
            Points.Add(point);
            if (Points.Count == Rectangle.SIZE)
            {
                return false;
            }
            return true;
        }

        public Point Center()
        {
            int x = Points.Sum(p => p.X) / (int)SIZE;
            int y = Points.Sum(p => p.Y) / (int)SIZE;
            Point point = new Point(x, y);

            return point;
        }


        public Point[] ToArray()
        {
            return Points.ToArray();
        }
    }
}
