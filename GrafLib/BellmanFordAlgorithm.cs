using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public class BellmanFordAlgorithm
    {
        public static int[,] GenerateMatrix(Graf graf)
        {
            int[,] output = new int[graf.Nodes.Count,graf.Nodes.Count];

            foreach (Edge edge in graf.Edges)
            {
                int index = 0;
                output[edge.Id - 1, index] = edge.AdjacentNodes[0].Id - 1;
                index++;
                output[edge.Id - 1, index] = edge.AdjacentNodes[1].Id - 1;
                index++;
                output[edge.Id - 1, index] = edge.Weight;
            }

            return output;
        }

        public static void BellmanFord(int[,] matrix, int nodes, int edges, int src)
        {
            int[] distances = new int[nodes];

            for (int i = 0; i < nodes; i++)
                distances[i] = int.MaxValue;

            distances[src] = 0;

            for (int i = 0; i < nodes - 1; i++)
            {
                for (int j = 0; j < edges; j++)
                {
                    if (distances[matrix[j, 0]] == int.MaxValue && distances[matrix[j, 0]] + matrix[j, 2] < distances[matrix[j, 1]])
                        distances[matrix[j, 1]] = distances[matrix[j, 0]] + matrix[j, 2];
                }
            }

            for (int i = 0; i < edges; i++)
            {
                int x = matrix[i, 0];
                int y = matrix[i, 1];
                int weight = matrix[i, 2];
                if (distances[x] != int.MaxValue && distances[x] + weight < distances[y])
                    throw new ArgumentException("Graful contine ciclu cu pondere negativa!");
            }

            Console.WriteLine("Vertex Distance from Source");
            for (int i = 0; i < nodes; i++)
                Console.WriteLine(i + "\t\t" + distances[i]);
        }





        public static int?[] StanescuBellmanFord(Graf graf, Node start)
        {
            int?[] distance = new int?[graf.Nodes.Count];
            //int?[] previous = new int?[graf.Nodes.Count];
            List<Edge> shuffledEdges = new List<Edge>(graf.Edges);

            foreach (Node node in graf.Nodes)
            {
                distance[node.Id-1] = int.MaxValue;
                //previous[node.Id-1] = null;
            }

            distance[start.Id-1] = 0;

            for (int i = 0; i < graf.Nodes.Count - 1; i++)
            {
                shuffledEdges.Shuffle();

                foreach (Edge edge in shuffledEdges)
                {
                    if (distance[edge.AdjacentNodes[0].Id-1] + edge.Weight < distance[edge.AdjacentNodes[1].Id-1])
                    {
                        if (distance[edge.AdjacentNodes[0].Id-1] + edge.Weight < 0)
                            distance[edge.AdjacentNodes[0].Id-1] = 0;

                        distance[edge.AdjacentNodes[1].Id-1] = distance[edge.AdjacentNodes[0].Id-1] + edge.Weight;
                        //previous[edge.AdjacentNodes[0].Id-1] = edge.AdjacentNodes[0].Id-1;                                                /// ???????
                    } 
                }
            }

            foreach (Edge edge in graf.Edges)
            {
                if (distance[edge.AdjacentNodes[0].Id-1] + edge.Weight < distance[edge.AdjacentNodes[1].Id-1])
                {
                    throw new ArgumentException("Graful contine un ciclu cu valoare negativa!");                                      /// ???????
                }
            }

            return distance;
        }
    }
}
