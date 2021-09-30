using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public class Edge : IDisplayUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Edge> AdjacentEdges { get; set; } = new List<Edge>();
        public List<Node> AdjacentNodes { get; set; } = new List<Node>();
        public static int createdEdges = 1;

        public Edge()
        {
            //createdEdges++;
        }
        public Edge(Node p1, Node p2)
        {
            AdjacentNodes.Add(p1);
            AdjacentNodes.Add(p2);
            Id = createdEdges++;
        }
        public void Draw()
        {

        }

        public override string ToString()
        {
            return $"e{this.Id} = (v{this.AdjacentNodes[0].Id},v{this.AdjacentNodes[1].Id})";
        }
    }
}
