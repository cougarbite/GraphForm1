using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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
        public int[,] IncidenceMatrix { get; set; }
        public int[,] KirchhoffMatrix { get; set; }

        public static int createdGrafs = 1;

        List<Node> noduriFolosite = new List<Node>();

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
            if (graf.Nodes is null)
            {
                List<Node> nodes = new List<Node>();
                graf.Nodes = nodes;
                return new int[graf.Nodes.Count, graf.Nodes.Count];
            }
            else
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
                    j = 0;
                    i++;
                }
                return output;
            }
        }

        public static int[,] CreateIncidenceMatrix(Graf graf)
        {
            if (graf.Edges is null)
            {
                List<Edge> edges = new List<Edge>();
                graf.Edges = edges;
                return new int[1,graf.Nodes.Count];
            }
            else
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
            if (graf.Nodes.Count == 0)
            {
                return 0;
            }
            else
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
        }
        public static int FindNodeCover(Graf graf)
        {
            //TODO - (Optional) De implementat logica gasirii acoperiri minime de varfuri

            return 0;
        }

        public static int FindEdgeCover(Graf graf)
        {
            int output = 0;

            output = (int)Math.Ceiling(graf.Nodes.Count / 2.0);

            return output;
        }
        
        public static List<Node> MultimeaStabilaInteriorMaximala(Node start, List<Node> GraphNodes)
        {
            List<Node> AvailableNodes = new List<Node>(GraphNodes);
            List<Node> Rezultat = new List<Node>();
            Node selectedNode = start;

            while (AvailableNodes.Count != 0)
            {
                foreach (Node adiacentNode in selectedNode.AdjacentNodes)
                {
                    AvailableNodes.Remove(adiacentNode);
                }
                AvailableNodes.Remove(selectedNode);
                Rezultat.Add(selectedNode);
                if (AvailableNodes.Count != 0)
                    selectedNode = (Node)AvailableNodes[0];
            }
            return Rezultat;
        }

        public static List<Node> BronKerboschRecursiv(List<Node> Disponibile, List<Node> Folosite)
        {
            List<Node> S = new List<Node>();
            List<Node> qPlus = new List<Node>(Disponibile);
            List<Node> qMinus = new List<Node>(Folosite);

            //test
            //qPlus.Clear();


            while (qPlus.Count != 0)
            {
                foreach (Node node in qPlus)
                {
                    S.Add(node);
                    List<Node> qPlusNew = new List<Node>(qPlus);
                    List<Node> qMinusNew = new List<Node>(qMinus);
                    qPlusNew.Remove(node);
                    foreach (Node adiacent in node.AdjacentNodes)
                    {
                        qPlusNew.Remove(adiacent);
                        qMinusNew.Add(adiacent);
                    }

                    if (qPlusNew.Count == 0 && qMinusNew.Count == 0)
                    {
                        return S;
                    }
                    BronKerboschRecursiv(qPlusNew, qMinusNew);
                    S.Remove(node);
                    qPlus.Remove(node);
                    qMinus.Add(node);
                }
            }

            return S;
        }

        public static List<Node> BronKerboschIterativ(List<Node> Disponibile)
        {
            Stack<Node> S = new Stack<Node>();
            throw new NotImplementedException();
        }

        /////////////////////////////////////////////////////////
        // Methods for transforming from one matrix to another //
        /////////////////////////////////////////////////////////

        /// <summary>
        /// Creaza matricea de incidenta din matricea de adiacenta.
        /// </summary>
        /// <param name="aMatrix">Matricea de baza.</param>
        /// <returns>Matricea de incidenta rezultata din matricea de baza.</returns>
        public int[,] CreateIfromA(int[,] aMatrix)
        {
            int nodes = (int)Math.Sqrt(aMatrix.Length), edges = 0;

            //Aflam numarul de muchii
            foreach (int val in aMatrix)
                if (val == 1)
                    edges++;
            edges /= 2;

            //Cream noua matrice
            int[,] resultingMatrix = new int[edges,nodes];

            int k = 0;
            for (int i = 0; i < nodes; i++)
            {
                for (int j =i+1; j < nodes; j++)
                {
                    if (aMatrix[i, j] == 1)
                    {
                        resultingMatrix[k, i] = 1;
                        resultingMatrix[k, j] = 1;
                        k++;
                    }
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
        /// Creaza matricea de adiacenta din matricea lui Kirchoff.
        /// </summary>
        /// <param name="kMatrix"></param>
        /// <returns>Matricea de incidenta rezultata din matricea de baza.</returns>
        public int[,] CreateIfromK(int[,] kMatrix)
        {
            int nodes = (int)Math.Sqrt(kMatrix.Length), edges = 0;

            //Aflam numarul de muchii
            foreach (int val in kMatrix)
                if (val == -1)
                    edges++;
            edges /= 2;

            //Cream noua matrice
            int[,] resultingMatrix = new int[edges, nodes];

            int k = 0;
            for (int i = 0; i < nodes; i++)
            {
                for (int j = i + 1; j < nodes; j++)
                {
                    if (kMatrix[i, j] == -1)
                    {
                        resultingMatrix[k, i] = 1;
                        resultingMatrix[k, j] = 1;
                        k++;
                    }
                }
            }
            return resultingMatrix;
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
            int edges = 0;
            //Aflam numarul de muchii
            foreach (int val in iMatrix)
                if (val == 1)
                    edges++;
            edges /= 2;

            int nodes = iMatrix.Length / edges;

            int[,] resultingMatrix = new int[nodes, nodes];
            int nod = 0, x = 0, y = 0, counter = 0, grade = 0;

            for (int i = 0; i < edges; i++)
            {
                for (int j = 0; j < nodes; j++)
                {
                    //Daca e vorba despre acelasi nod
                    if (nod == j)
                    {
                        //Aflam gradul lui nod
                        for (int k = 0; k < edges; k++)
                        {
                            if (iMatrix[k, nod] == 1)
                            {
                                grade++;
                            }
                        }
                        resultingMatrix[nod, nod] = grade;
                        grade = 0;
                        nod++;
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
