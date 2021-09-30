using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public class Node : IDisplayUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public List<Node> AdjacentNodes;

        public List<Edge> AdjacentEdges;

        public static int createdNodes = 1;

        public Node(int X, int Y)
        {
            XCoord = X;
            YCoord = Y;
            Id = createdNodes++;
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.Name + $"v{this.Id} ({this.XCoord},{this.YCoord})";
        }
    }
}
