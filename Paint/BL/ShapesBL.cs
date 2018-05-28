using System;
using System.Collections.Generic;
using System.Linq;
using Paint.Interfaces;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using Paint.Shapes;

namespace Paint.BL
{
    public class ShapesBL
    {
        public static void SerializeList(List<IShapes> shapes, string path)
        {
            XmlSerializer formatterRectangle = new XmlSerializer(typeof(List<Shapes.Rectangle>));
            shapes.ForEach(p => p.RgbaColor = p.Color.ToArgb());
            List<Shapes.Rectangle> rectangle = new List<Shapes.Rectangle>();
            foreach(var it in shapes)
            {
                rectangle.Add((Shapes.Rectangle)it);
            }
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatterRectangle.Serialize(fs, rectangle);
            }
        }

        public static List<IShapes> DeserializeList(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Shapes.Rectangle>));
            List<Shapes.Rectangle> rectangle = null;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                rectangle = (List<Shapes.Rectangle>)formatter.Deserialize(fs);
            }

            if (rectangle == null)
            {
                throw new ApplicationException(string.Format("cannot deserialize file {0}", path));
            }
            rectangle.ForEach(p => p.Color = Color.FromArgb(p.RgbaColor));

            List<IShapes> shapes = rectangle.ToList<IShapes>();

            return shapes;
        }

        public static IShapes MoveToPoint(IShapes shape, Point newCenter)
        {
            Point previouseCenter = shape.Center();
            int xShifting = previouseCenter.X - newCenter.X;
            int yShifting = previouseCenter.Y - newCenter.Y;

            var points = shape.ToArray();
            for (int i = 0; i < points.Count(); i++)
            {
                points[i].X -= xShifting;
                points[i].Y -= yShifting;
            }
            shape.Points = points.ToList();

            return shape;
        }

    }
}
