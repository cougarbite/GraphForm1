using GrafLib;
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
        Pen p = new Pen(Color.Black,5);
        Font f = new Font("Arial", 10);
        Point cursor;

        Graf graf = new Graf();
        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();

        public Form1()
        {
            InitializeComponent();
        }

        private void grafPanel_MouseMove(object sender, MouseEventArgs e)
        {
            cursor = this.PointToClient(Cursor.Position);
            mouseStatus.Text = "x: " + cursor.X + "y: " + cursor.Y;
        }

        private void grafPanel_Click(object sender, EventArgs e)
        {
            g = grafPanel.CreateGraphics();

            if (radioDrawNode.Checked)
            {
                Node node = new Node(cursor.X, cursor.Y);
                g.DrawString("v" + node.Id.ToString(), f, Brushes.Black, cursor.X - 5, cursor.Y + 7);
                g.DrawEllipse(p, cursor.X, cursor.Y, 5, 5);

                nodes.Add(node);
                graf.Nodes = nodes;
                RefreshNodeList();
            }
            else if (radioDrawEdge.Checked)
            {
                Edge edge = new Edge();
                //TODO - Check the method below
                g.DrawLine(p,100,100,300,300);
                graf.Edges = edges;
                RefreshEdgeList();
            }
        }

        private void RefreshNodeList()
        {
            nodesListBox.DataSource = null;
            nodesListBox.DataSource = graf.Nodes;
            nodesListBox.SelectedIndex = graf.Nodes.Count - 1;
        }

        private void deleteNodeButton_Click(object sender, EventArgs e)
        {
            if (graf.Nodes.Count > 0)
            {
                graf.Nodes.RemoveAt(nodesListBox.SelectedIndex);
                RefreshNodeList();
            }
        }

        private void RefreshEdgeList()
        {
            edgesListBox.DataSource = null;
            edgesListBox.DataSource = graf.Edges;
            edgesListBox.SelectedIndex = graf.Edges.Count - 1;
        }

        private void deleteEdgeButton_Click(object sender, EventArgs e)
        {
            if (graf.Edges.Count > 0)
            {
                graf.Edges.RemoveAt(edgesListBox.SelectedIndex);
                RefreshNodeList();
            }
        }

        private Node GetNode()
        {
            Node pointedNode = new Node(cursor.X, cursor.Y);

            foreach (Node node in graf.Nodes)
            {
                if (node.XCoord == pointedNode.XCoord && node.YCoord == pointedNode.YCoord)
                {
                    return node;
                }
                else
                    return null;

            }
            return new Node(cursor.X, cursor.Y);
        }
        private void grafPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
