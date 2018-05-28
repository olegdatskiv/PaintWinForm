using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paint.Utils;
using Paint.BL;
using Paint.Interfaces;
using Paint.Shapes;

namespace Paint
{
    public partial class Form1 : Form
    {
        private bool isSave = false;
        private int pointX = 0;
        private int pointY = 0;
        private Bitmap bmap;
        private Graphics grap;
        private SolidBrush brush;
        private bool isShapes = false;
        private List<IShapes> listShape = new List<IShapes>();
        private IShapes shape;

        public Form1()
        {
            InitializeComponent();
            bmap = new Bitmap(panel1.Width, panel1.Height);
            grap = panel1.CreateGraphics();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            
            brush = new SolidBrush(Color.White);

            if (listColor.SelectedIndex == 0)
            {
                brush.Color = Color.Red;
            }
            else if (listColor.SelectedIndex == 1)
            {
                brush.Color = Color.Green;
            }
            else if (listColor.SelectedIndex == 2)
            {
                brush.Color = Color.Yellow;
            }
            else if (listColor.SelectedIndex == 3)
            {
                brush.Color = Color.Blue;
            }
            else if (listColor.SelectedIndex == 4)
            {
                brush.Color = Color.White;
            }
            else if (listColor.SelectedIndex == 5)
            {
                brush.Color = Color.Black;
            }


            if (listShapes.SelectedIndex == 0)
            {
                RectangleForm form = new RectangleForm();
                form.ShowDialog();

                string width = form.textBox1.Text;
                string height = form.textBox2.Text;
                if (width != "" && height != "")
                {
                    int width1, height1;
                    if (!Int32.TryParse(width, out width1) || !Int32.TryParse(height, out height1))
                    {
                        MessageBox.Show("Incorrect input data");
                    }
                    else
                    {
                        Point[] point = new Point[4];
                        point[0] = new Point(pointX, pointY);
                        point[1] = new Point(pointX, pointY + height1);
                        point[2] = new Point(pointX + width1, pointY + height1);
                        point[3] = new Point(pointX + width1, pointY);
                        Shapes.Rectangle rectangle = new Shapes.Rectangle(point);

                        rectangle.Name = "Rectangle" + listShape.Count.ToString();
                        listShape.Add(rectangle);

                        rectangle.Color = brush.Color;
                        grap.FillPolygon(new SolidBrush(rectangle.Color), rectangle.ToArray());
                        isSave = false;
                    }
                }
            }
            panel1.BackColor = Color.White;
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UI.CreateInformationWindow() == DialogResult.Yes)
            {
                Close();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditShapes form = new EditShapes();
            foreach (var it in listShape)
            {
                form.listBox1.Items.Add(it.Name.ToString());
            }
            form.ShowDialog();
            if (form.listBox1.SelectedIndex != -1 && form.listBox2.SelectedIndex != -1)
            {
                isSave = false;
                shape = listShape.FirstOrDefault(p => p.Name == form.listBox1.GetItemText(form.listBox1.SelectedItem));
                listShape.Remove(shape);
                if (form.listBox2.SelectedIndex == 0)
                {
                    ColorDialog colorDialog = new ColorDialog();
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        shape.Color = colorDialog.Color;
                        listShape.Add(shape);
                        shape = null;
                        listShapes.ClearSelected();
                        listColor.ClearSelected();
                        panel1.Refresh();
                        foreach (var it in listShape)
                        {
                            grap.FillPolygon(new SolidBrush(it.Color), it.ToArray());
                        }
                    }
                }
                else if (form.listBox2.SelectedIndex == 1)
                {
                    isShapes = true;
                    MessageBox.Show("Please select new position");
                }
                else if(form.listBox2.SelectedIndex == 2)
                {
                    listShapes.ClearSelected();
                    listColor.ClearSelected();
                    panel1.Refresh();
                    foreach (var it in listShape)
                    {
                        grap.FillPolygon(new SolidBrush(it.Color), it.ToArray());
                    }
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSave)
            {
                DialogResult result1 = MessageBox.Show("This file is not saved, you are sure you want to close it?",
    "Important Question",
    MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    listShapes.ClearSelected();
                    listColor.ClearSelected();
                    panel1.Refresh();
                }
            }
            else
            {
                listShapes.ClearSelected();
                listColor.ClearSelected();
                panel1.Refresh();
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                int width = panel1.Size.Width;
                int height = panel1.Size.Height;

                panel1.DrawToBitmap(bmap, new System.Drawing.Rectangle(0, 0, width, height));

                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bmap.Save(fs,
                            System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bmap.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bmap.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                pointX = e.X;
                pointY = e.Y;
                panel_Paint(this, null);
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (isShapes)
                {
                    listShape.Remove(shape);
                    shape = ShapesBL.MoveToPoint(shape, new Point(e.X, e.Y));
                    listShape.Add(shape);
                    shape = null;
                    isShapes = false;
                    listShapes.ClearSelected();
                    listColor.ClearSelected();
                    panel1.Refresh();
                    foreach (var it in listShape)
                    {
                        grap.FillPolygon(new SolidBrush(it.Color), it.ToArray());
                    }
                    isSave = false;
                }
                else
                {
                    shape = listShape.FirstOrDefault(p => Geometry.IsInPolygon(p.ToArray(), new Point(e.X, e.Y)) == true);
                    if (shape != null)
                    {
                        isShapes = true;
                        MessageBox.Show("Please select new position");
                    }
                }
            }

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = UI.CreateSaveFile();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ShapesBL.SerializeList(listShape, saveFileDialog.FileName);
                isSave = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = UI.CreateOpenFile();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                listShapes.ClearSelected();
                listColor.ClearSelected();
                panel1.Refresh();
                listShape = ShapesBL.DeserializeList(openFileDialog.FileName);
                foreach (var it in listShape)
                {
                    grap.FillPolygon(new SolidBrush(it.Color), it.ToArray());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listShapes.ClearSelected();
            listColor.ClearSelected();
        }
    }
}
