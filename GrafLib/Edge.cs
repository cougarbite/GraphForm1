using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public class Edge : IDisplayUI
    {
        public int L { get; set; } = 1;
        public string Name { get; set; }
        public List<Edge> AdjacentEdges { get; set; }
        public List<Node> AdjacentNodes { get; set; }

        public void Draw()
        {

        }

        public override string ToString()
        {
            return this.Name + $" = ({this.AdjacentNodes[0].Name},{this.AdjacentNodes[1].Name})";
        }
    }
}
