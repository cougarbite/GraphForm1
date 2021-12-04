﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace GrafLib
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int Grade { get; set; }
        public List<Node> AdjacentNodes;
        public List<Edge> AdjacentEdges;
        public bool isVisited = false;
        public bool isColored = false;
        //TODO - DE adaugat culoarea?!
        public bool isDisabled = false;
        public int group = 0;
        public static int createdNodes = 1;

        public Node()
        {

        }
        public Node(int X, int Y)
        {
            XCoord = X;
            YCoord = Y;
            Id = createdNodes++;
            Name = "v" + this.Id;
            this.AdjacentNodes = new List<Node>();
            this.AdjacentEdges = new List<Edge>();
        }
        public override string ToString()
        {
            return $"v{this.Id} ({this.XCoord},{this.YCoord})";
        }
    }
}
 