using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafLib
{
    public class Actions : IDisplayUI
    {
        public void Draw()
        {
            throw new NotImplementedException();
        }

        public int[,] CreateAdjacencyMatrix(Graf graf)
        {
            int i = 0, j = 0, length = graf.Nodes.Count;
            int[,] output = new int[length, length];

            foreach (Node node1 in graf.Nodes)
            {
                foreach (Node node2 in graf.Nodes)
                {
                    if (node1.AdjacentNodes.Contains(node2))
                    {
                        output[i, j] = 1;
                    }
                    else
                        output[i, j] = 0;
                    j++;
                }
                i++;
            }
            return output;
        }

        public int[,] CreateIncidencyMatrix(Graf graf)
        {
            int i = 0, j = 0, length = graf.Nodes.Count;
            int[,] output = new int[length, length];

            foreach (Edge edge in graf.Edges)
            {
                foreach (Node node in graf.Nodes)
                {
                    if (edge.AdjacentNodes.Contains(node))
                    {
                        output[i, j] = 1;
                    }
                    else
                        output[i, j] = 0;
                    j++;
                }
                i++;
            }
            return output;
        }

        public int[,] CreateKirchhoffMatrix(Graf graf)
        {
            int i = 0, j = 0, length = graf.Nodes.Count;
            int[,] output = new int[length, length];

            foreach (Node node1 in graf.Nodes)
            {
                foreach (Node node2 in graf.Nodes)
                {
                    if (node1 == node2)
                    {
                        output[i, j] = node1.AdjacentEdges.Count;
                    }
                    else
                    {
                        if (node1.AdjacentNodes.Contains(node2))
                        {
                            output[i, j] = 1;
                        }
                        else
                            output[i, j] = 0;
                    }
                    j++;
                }
                i++;
            }
            return output;
        }
    }
}
