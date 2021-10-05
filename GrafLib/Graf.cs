using System;
using System.Collections.Generic;

namespace GrafLib
{
    public class Graf : IMainWindow
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
        public int[,] CreateIfromA(int[,] aMatrix)
        {
            //TODO - Implement this method A => K
            throw new NotImplementedException();
        }
        public int[,] CreateKfromA(int[,] aMatrix)
        {
            //TODO - Implement this method A => K
            throw new NotImplementedException();
        }
        public int[,] CreateAfromK(int[,] kMatrix)
        {
            int rows = kMatrix.Length / 3, cols = rows;
            int[,] resultingMatrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (kMatrix[i, j] == -1)
                        resultingMatrix[i, j] = 1;
                    else
                        resultingMatrix[i, j] = 0;
                }
            }
            return resultingMatrix;
        }
        public int[,] CreateIfromK(int[,] kMatrix)
        {
            //TODO - Implement this method A => K
            throw new NotImplementedException();
        }
        public int[,] CreateAfromI(int[,] iMatrix, int nodes, int edges)
        {
            //TODO - Implement i to a
            int[,] resultingMatrix = new int[nodes, nodes];
            resultingMatrix[0, 0] = 0;
            for (int i = 0; i < nodes; i++)
            {
                for (int j = 1; j <= edges; j++)
                {
                    if (iMatrix[i, j] == 1 && iMatrix[i, j + 1] == 1)
                    {
                        resultingMatrix[i, j + 1] = 1;
                        resultingMatrix[i + 1, j] = 1;
                    }
                    else
                    {
                        resultingMatrix[i, j + 1] = 0;
                        resultingMatrix[i + 1, j] = 0;
                    }
                }
            }

            return resultingMatrix;
        }
        public int[,] CreateKfromI(int[,] iMatrix, int nodes, int edges)
        {
            //TODO - Implement this crappy method
            int grade = 0, n = 0, m = 0;
            int[,] resultingMatrix = new int[nodes, nodes];
            for (int i = 0; i < nodes; i++)
            {
                for (int j = 0; j < edges; j++)
                {
                    if (i == j)
                    {
                        for (int k = 0; k < edges; k++)
                        {
                            if (iMatrix[i, k] == 1)
                                grade++;
                        }
                        resultingMatrix[i, j] = grade;
                        grade = 0;
                    }
                    else
                    {
                        if (iMatrix[i, j] == 1)
                            for (int l = 0; l < nodes; l++)
                            {
                                //TODO - finish this shit



                                resultingMatrix[i, j] = -1;
                            }
                        else
                            resultingMatrix[i, j] = 0;
                    }
                }
            }
            return resultingMatrix;
        }
    }
}
