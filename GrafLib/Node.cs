using System;
using System.Collections.Generic;

namespace GrafLib
{
    public class Node
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
            this.AdjacentNodes = new List<Node>();
            this.AdjacentEdges = new List<Edge>();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"v{this.Id} ({this.XCoord},{this.YCoord})";
        }
    }
}
