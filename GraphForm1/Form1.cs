using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphForm1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        Point cursor;
        int k = 0, l = 0;


        Point[] points = new Point[20];

        Point start = new Point();
        Point end = new Point();

        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            p = new Pen(Color.Black, 4);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            cursor = this.PointToClient(Cursor.Position);
            mouseStatus.Text = "x: " + cursor.X + "y: " + cursor.Y;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (radioDrawNode.Checked)
            {
                string name = "v" + k;
                g.DrawEllipse(p, cursor.X, cursor.Y, 3, 3);
                Point n = new Point(cursor.X, cursor.Y);

                nodesListBox.Items.Add(n);
            }
        }

        private void nodesListBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (l % 2 == 0)
            {
                start.X = ((Point)nodesListBox.SelectedItem).X;
                start.Y = ((Point)nodesListBox.SelectedItem).Y;
                l++;
            }
            else if (l % 2 == 1)
            {
                end.X = ((Point)nodesListBox.SelectedItem).X;
                end.Y = ((Point)nodesListBox.SelectedItem).Y;
                DrawEdge(new Pen(Color.GreenYellow,2), start, end);
                edgesListBox.Items.Add($"From ({start.X},{start.Y}) to ({end.X},{end.Y})");
                l++;
            }
        }

        private void DrawEdge(Pen pen, Point start, Point end)
        {
            g.DrawLine(pen, start, end);
        }
    }
}
