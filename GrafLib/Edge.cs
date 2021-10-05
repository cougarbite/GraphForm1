using System.Collections.Generic;

namespace GrafLib
{
    public class Edge
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
            Id = createdEdges++;

            //Adaug nodurile unite de muchie
            this.AdjacentNodes.Add(p1);
            this.AdjacentNodes.Add(p2);

            //Adaug la lista muchiilor adiacente, muchiile fiecarui varfului
            foreach (Node node in this.AdjacentNodes)
            {
                foreach (Edge edge in node.AdjacentEdges)
                {
                    this.AdjacentEdges.Add(edge);
                    edge.AdjacentEdges.Add(this);
                }
            }

            p1.AdjacentNodes.Add(p2);
            p1.AdjacentEdges.Add(this);
            p2.AdjacentNodes.Add(p1);
            p2.AdjacentEdges.Add(this);
        }
        public void DeleteEdge()
        {
            Node p1 = this.AdjacentNodes[0];
            Node p2 = this.AdjacentNodes[1];
            p1.AdjacentNodes.Remove(p2);
            p2.AdjacentNodes.Remove(p1);
            foreach (Edge adjacentEdge in this.AdjacentEdges)
            {
                adjacentEdge.AdjacentEdges.Remove(this);
            }
            p1.AdjacentEdges.Remove(this);
            p2.AdjacentEdges.Remove(this);
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
