using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Interfaces
{
    public interface IShapes
    {
        string Name { get; set; }
        List<Point> Points { get; set; }
        int RgbaColor { get; set; }
        Color Color { get; set; }
        int Count();
        bool IsCompleted();
        bool AddPoint(Point point);
        Point Center();
        Point[] ToArray();
    }
}
