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
    public class Node
    {
        //public List<Node> nodes = new List<Node>();
        public int X, Y;
        string name;
        public Node(int xCoord, int yCoord, string nodeName)
        {
            X = xCoord;
            Y = yCoord;
            name = nodeName;
        }

        public override string ToString()
        {
            return this.name + $" ({this.X},{this.Y})";
        }
    }
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        Point cursor;
        int k = 0, l = 0; 
        static int n = 1;

        List<Node> nodes = new List<Node>();


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
            //if (radioDrawNode.Checked)
            //{
            //    string name = "v" + k;
            //    g.DrawEllipse(p, cursor.X, cursor.Y, 3, 3);
            //    Point n = new Point(cursor.X, cursor.Y);

            //    nodesListBox.Items.Add(n);
            //}

            if (radioDrawNode.Checked)
            {
                string name = "v" + Form1.n++;
                Node node = new Node(cursor.X, cursor.Y, name);
                g.DrawEllipse(p, cursor.X, cursor.Y, 3, 3);
                //Point n = new Point(cursor.X, cursor.Y);
                nodes.Add(node);
                nodesListBox.Items.Add(node);
            }

        }

        private void nodesListBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (l % 2 == 0)
            {
                start.X = ((Node)nodesListBox.SelectedItem).X;
                start.Y = ((Node)nodesListBox.SelectedItem).Y;
                l++;
            }
            else if (l % 2 == 1)
            {
                end.X = ((Node)nodesListBox.SelectedItem).X;
                end.Y = ((Node)nodesListBox.SelectedItem).Y;
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
