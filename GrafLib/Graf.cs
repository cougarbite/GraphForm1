using System;
using System.Collections.Generic;

namespace GrafLib
{
    public class Graf : IMainWindow
    {
        public string Name { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
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

        ////////////////////////////////////////////////////////////////////
        // Static methods for creating matrix representation of the Graph //
        ////////////////////////////////////////////////////////////////////
        
        public static int[,] CreateAdjacencyMatrix(Graf graf)
        {
            //TODO - check for graph without nodes
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
                j = 0;
                i++;
            }
            return output;
        }

        public static int[,] CreateIncidencyMatrix(Graf graf)
        {
            int i = 0, j = 0, nodes = graf.Nodes.Count, edges = graf.Edges.Count;
            int[,] output = new int[edges, nodes];

            //TODO - check for graph without edges
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
                j = 0;
                i++;
            }
            return output;
        }

        public static int[,] CreateKirchhoffMatrix(Graf graf)
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
                            output[i, j] = -1;
                        }
                        else
                            output[i, j] = 0;
                    }
                    j++;
                }
                j = 0;
                i++;
            }
            return output;
        }

        public static int FindMaxGrade(Graf graf)
        {
            int output = 0;
            foreach (Node node in graf.Nodes)
            {
                if (node.Grade > output)
                    output = node.Grade;
            }
            return output;
        }

        public static int FindMinGrade(Graf graf)
        {
            int output = graf.Nodes[0].Grade;
            foreach (Node node in graf.Nodes)
            {
                if (node.Grade < output)
                {
                    output = node.Grade;
                }
            }
            return output;
        }

        /////////////////////////////////////////////////////////
        // Methods for transforming from one matrix to another //
        /////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aMatrix"></param>
        /// <returns></returns>
        public int[,] CreateIfromA(int[,] aMatrix)
        {
            //TODO - Implement i from a
            int nodes = (int)Math.Sqrt(aMatrix.Length), edges = 0;

            //Aflam numarul de muchii
            foreach (int val in aMatrix)
                if (val == 1)
                    edges++;
            edges /= 2;

            int[,] resultingMatrix = new int[edges,nodes];

            int k;
            for (int i = 0; i < nodes; i++)
            {
                for (int j = 0; j < nodes; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (aMatrix[i, j] == 1)
                        resultingMatrix[i, j] = 1;
                    else
                        resultingMatrix[i, j] = 0;
                }
            }

            return resultingMatrix;
        }

        /// <summary>
        /// Creaza matricea lui Kirchoff din matricea de adiacenta.
        /// </summary>
        /// <param name="aMatrix">Matricea de baza.</param>
        /// <returns>Matricea lui Kirchoff rezultata din matricea de baza.</returns>
        public int[,] CreateKfromA(int[,] aMatrix)
        {
            int grade = 0, n = (int)Math.Sqrt(aMatrix.Length);
            int[,] resultingMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Daca e vorba despre acelasi nod, ii aflam gradul si il punem in matrice
                    if (i == j)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            if (aMatrix[i, k] == 1)
                            {
                                grade++;
                            }
                        }
                        resultingMatrix[i, j] = grade;
                        grade = 0;
                    }
                    //Daca nodurile sunt adiacente, punem un -1 in matrice
                    else if (aMatrix[i, j] == 1)
                        resultingMatrix[i, j] = -1;
                    //Daca nodurile nu sunt adiacente, punem un 0 in matrice
                    else
                        resultingMatrix[i, j] = 0;
                }
            }
            return resultingMatrix;
        }
        /// <summary>
        /// Creaza matricea de adiacenta din matricea lui Kirchoff.
        /// </summary>
        /// <param name="kMatrix">Matricea de baza.</param>
        /// <returns>Matricea de adiacenta rezultata din matricea de baza.</returns>
        public int[,] CreateAfromK(int[,] kMatrix)
        {
            int n = (int)Math.Sqrt(kMatrix.Length);
            int[,] resultingMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Daca e vorba despre acelasi nod, punem in matrice 0
                    if (i == j)
                    {
                        resultingMatrix[i, j] = 0;
                    }
                    //Daca nodurile sunt adiacente, punem un 1 in matrice
                    else if (kMatrix[i, j] == -1)
                        resultingMatrix[i, j] = 1;
                    //Daca nodurile nu sunt adiacente, punem un 0 in matrice
                    else
                        resultingMatrix[i, j] = 0;
                }
            }
            return resultingMatrix;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kMatrix"></param>
        /// <returns></returns>
        public int[,] CreateIfromK(int[,] kMatrix)
        {
            //TODO - Implement i from k
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creaza matricea de adiacenta din matricea de incidenta.
        /// </summary>
        /// <param name="iMatrix">Matricea de baza.</param>
        /// <returns>Matricea de adiacenta rezultata din matricea de baza.</returns>
        public int[,] CreateAfromI(int[,] iMatrix)
        {
            int edges = 0;
            //Aflam numarul de muchii
            foreach (int val in iMatrix)
                if (val == 1)
                    edges++;
            edges /= 2;

            int nodes = iMatrix.Length/edges;

            int[,] resultingMatrix = new int[nodes, nodes];
            int x = 0, y = 0, counter = 0;
            for (int i = 0; i < edges; i++)
            {
                for (int j = 0; j < nodes; j++)
                {
                    if (iMatrix[i,j] == 1 && counter % 2 == 0)
                    {
                        x = j;
                        counter++;
                    }
                    else if (iMatrix[i,j] == 1 && counter % 2 == 1)
                    {
                        y = j;
                        counter++;
                    }
                }
                resultingMatrix[x, y] = 1;
                resultingMatrix[y, x] = 1;
            }
            return resultingMatrix;
        }
        /// <summary>
        /// Creaza matricea lui Kirchoff din matricea de incidenta.
        /// </summary>
        /// <param name="iMatrix">Matricea de baza.</param>
        /// <returns>Matricea lui Kirchoff rezultata din matricea de baza.</returns>
        public int[,] CreateKfromI(int[,] iMatrix)
        {
            //TODO - Check for solutions when edges < nodes
            int edges = 0;
            //Aflam numarul de muchii
            foreach (int val in iMatrix)
                if (val == 1)
                    edges++;
            edges /= 2;

            int nodes = iMatrix.Length / edges;

            int[,] resultingMatrix = new int[nodes, nodes];
            int x = 0, y = 0, counter = 0, grade = 0;
            for (int i = 0; i < edges; i++)
            {
                for (int j = 0; j < nodes; j++)
                {
                    //Daca e vorba despre acelasi nod, ii aflam gradul si il punem in matrice
                    if (i == j)
                    {
                        for (int k = 0; k < edges; k++)
                        {
                            if (iMatrix[k, j] == 1)
                            {
                                grade++;
                            }
                        }
                        resultingMatrix[i, j] = grade;
                        grade = 0;
                    }

                    if (iMatrix[i, j] == 1 && counter % 2 == 0)
                    {
                        x = j;
                        counter++;
                    }
                    else if (iMatrix[i, j] == 1 && counter % 2 == 1)
                    {
                        y = j;
                        counter++;
                    }
                }
                resultingMatrix[x, y] = -1;
                resultingMatrix[y, x] = -1;
            }
            return resultingMatrix;
        }
    }
}
