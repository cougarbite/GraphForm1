using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public class EdgeWeightComparer : IComparer<Edge>
    {
        public int Compare(Edge x, Edge y)
        {
            return (x.Weight.CompareTo(y.Weight));
        }
    }
}
