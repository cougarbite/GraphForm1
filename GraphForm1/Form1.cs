using GrafLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphForm1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p = new Pen(Color.Black, 5);
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
                int count = 0;
                if (count % 2 == 0)
                {
                    count++;
                }
                else if (count % 2 == 1)
                {
                    Node n2 = new Node(cursor.X, cursor.Y);
                    count++;
                    //g.DrawLine(p,n1.XCoord, n1.YCoord, n2.XCoord, n2.YCoord);
                    graf.Edges = edges;
                    RefreshEdgeList();
                }
                //TODO - Check the method below
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
