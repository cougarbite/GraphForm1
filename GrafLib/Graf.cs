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

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public int[,] CreateAfromK(int[,] aMatrix)
        {

            throw new NotImplementedException();

        }

        public int[,] CreateAfromI(int[,] aMatrix)
        {
            throw new NotImplementedException();

        }
        public int[,] CreateIfromA(int[,] iMatrix)
        {
            throw new NotImplementedException();

        }
        public int[,] CreateIfromK(int[,] iMatrix)
        {
            throw new NotImplementedException();

        }
        public int[,] CreateKfromA(int[,] kMatrix)
        {
            throw new NotImplementedException();

        }
        public int[,] CreateKfromI(int[,] kMatrix)
        {
            throw new NotImplementedException();

        }
    }
}
