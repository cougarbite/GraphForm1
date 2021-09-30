using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public class Graf
    {
        public string Name { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public int Grade { get; set; }
        public int MaxGrade { get; set; }
        public int MinGrade { get; set; }
        public int[,] AdjacencyMatrix { get; set; }
        public int[,] IncidencyMatrix { get; set; }
        public int[,] KirchhoffMatrix { get; set; }
    }
}
