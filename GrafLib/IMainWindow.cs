using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public interface IMainWindow
    {
        List<Node> Nodes { get; set; }
        List<Edge> Edges { get; set; }
        int[,] AdjacencyMatrix { get; set; }
        int[,] IncidencyMatrix { get; set; }
        int[,] KirchhoffMatrix { get; set; }
        //CreateGraf()
        //DeleteGraf()
    }
}
