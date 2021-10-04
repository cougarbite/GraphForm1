using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public class Graf : IDisplayUI
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

        public static int createdGrafs = 1;

        public Graf()
        {
            DateTime time = DateTime.Now;
            Name = $"Graf {time.Hour}{time.Minute}{time.Second} " + createdGrafs++;
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

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public int[,] CreateAfromK(int[,] kMatrix)
        {
            int rows = kMatrix.Length / 3, cols = rows;
            int[,] resultingMatrix = new int[rows,cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (kMatrix[i,j] == -1)
                        resultingMatrix[i, j] = 1;
                    else
                        resultingMatrix[i, j] = 0;
                }
            }
            return resultingMatrix;
        }

        public int[,] CreateAfromI(int[,] iMatrix)
        {
            throw new NotImplementedException();
        }
        public int[,] CreateIfromA(int[,] aMatrix)
        {
            throw new NotImplementedException();
        }
        public int[,] CreateIfromK(int[,] kMatrix)
        {
            throw new NotImplementedException();
        }
        public int[,] CreateKfromA(int[,] aMatrix)
        {
            throw new NotImplementedException();
        }
        public int[,] CreateKfromI(int[,] iMatrix, int nodes, int edges)
        {
            int grade = 0;
            int[,] resultingMatrix = new int[nodes, nodes];
            for (int i = 0; i < nodes; i++)
            {
                for (int j = 0; j < edges; j++)
                {
                    grade++;
                }
                if (grade == 0)
                    resultingMatrix[i, 0] = 0;
                else
                    resultingMatrix[i, 0] = grade;
                grade = 0;
                //TODO - de continuat logica
            }
            return resultingMatrix;
        }
    }
}
