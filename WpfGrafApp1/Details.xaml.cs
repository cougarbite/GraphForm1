﻿using GrafLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGrafApp1
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        Graf selectedGraf;
        //public Details()
        //{
        //    InitializeComponent();
        //}

        public Details(Graf graf)
        {
            selectedGraf = graf;
            InitializeComponent();
            PopulateForm();
        }

        private void PopulateForm()
        {
            grafNameLabel.Content = $"Nume : {selectedGraf.Name}";
            grafImage.Source = new BitmapImage(new Uri($@"C:\Users\darks\source\repos\GraphForm1\WpfGrafApp1\Grafuri\{selectedGraf.Name}.png"));
            grafMaxGradeLabel.Content = $"Δ(G) = {selectedGraf.MaxGrade}";
            grafMinGradeLabel.Content = $"δ(G) = {selectedGraf.MinGrade}";
            grafNCountLabel.Content = $"|G| = {selectedGraf.Nodes.Count}";
            grafECountLabel.Content = $"||G|| = {selectedGraf.Edges.Count}";
            grafNodesLabel.Content = ConvertNodesToString(selectedGraf.Nodes);
            grafEdgesLabel.Content = ConvertEdgesToString(selectedGraf.Edges);
            grafNCoverage.Content = $"β0(G) = {Graf.FindNodeCover(selectedGraf)}";
            grafECoverage.Content = $"β1(G) = {Graf.FindEdgeCover(selectedGraf)}";
            PopulateAdjacencyGrid(selectedGraf.AdjacencyMatrix);
            PopulateIncidenceGrid(selectedGraf.IncidenceMatrix);
            PopulateKirchhoffGrid(selectedGraf.KirchhoffMatrix);
        }

        private string ConvertNodesToString(List<Node> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("V(G) = {");

            if (list.Count == 0)
                sb.Append(" Ø ");
            else
            {
                foreach (Node node in list)
                {
                    sb.Append($" {node.Name} ");
                }
            }
            sb.Append(" }");
            return sb.ToString();
        }

        private string ConvertEdgesToString(List<Edge> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("E(G) = {");

            if (list.Count == 0)
            {
                sb.Append(" Ø ");
            }
            else
            {
                foreach (Edge edge in list)
                {
                    sb.Append($" ({edge.AdjacentNodes[0].Name}, {edge.AdjacentNodes[1].Name}) ");
                }
            }
            sb.Append("}");
            return sb.ToString();
        }

        private void DFS(Node start, Stack<Node> unvisitedNodes, StringBuilder visited)
        {
            start.isVisited = true;
            //visited.Add(start);
            visited.Append(start.Name + " -> ");
            foreach (Node node in start.AdjacentNodes)
                if (!node.isVisited && !unvisitedNodes.Contains(node))
                    unvisitedNodes.Push(node);

            while (unvisitedNodes.Count > 0)
                DFS(unvisitedNodes.Pop(), unvisitedNodes, visited);
        }

        private void BFS(Node start, Queue<Node> queuedNodes, StringBuilder visited)
        {
            start.isVisited = true;
            visited.Append(start.Name + " -> ");
            foreach (Node node in start.AdjacentNodes)
                if (!node.isVisited && !queuedNodes.Contains(node))
                    queuedNodes.Enqueue(node);

            while (queuedNodes.Count > 0)
                BFS(queuedNodes.Dequeue(), queuedNodes, visited);
        }
        private void closeDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResetVisitedNodes(Graf graf)
        {
            foreach (Node node in graf.Nodes)
                node.isVisited = false;
        }
        private void ResetCoveredEdges(Graf graf)
        {
            foreach (Edge node in graf.Edges)
                node.isCovered = false;
        }

        private void algoritmBFSButton_Click(object sender, RoutedEventArgs e)
        {
            ResetVisitedNodes(selectedGraf);
            Random random = new Random();
            int index = random.Next(selectedGraf.Nodes.Count);
            Node start = selectedGraf.Nodes[index];
            Queue<Node> queue = new Queue<Node>();
            StringBuilder visited = new StringBuilder();
            BFS(start, queue, visited);
            visited.Remove(visited.Length - 4, 4);
            string output = visited.ToString();
            MessageBox.Show(output, "BFS - Parcurgerea in adancime", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void algoritmDFSButton_Click(object sender, RoutedEventArgs e)
        {
            ResetVisitedNodes(selectedGraf);
            Random random = new Random();
            int index = random.Next(selectedGraf.Nodes.Count);
            Node start = selectedGraf.Nodes[index];
            Stack<Node> stack = new Stack<Node>();
            StringBuilder visited = new StringBuilder();
            DFS(start, stack, visited);
            visited.Remove(visited.Length - 4, 4);
            string output = visited.ToString();
            MessageBox.Show(output, "DFS - Parcurgerea in latime", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MaximalSizeCliqueButton_Click(object sender, RoutedEventArgs e)
        {
            ResetVisitedNodes(selectedGraf);
            ResetCoveredEdges(selectedGraf);
            Graf.FindMaximalSizeClique(selectedGraf);
            string output = "";
            MessageBox.Show(output, "Bron Kerbosch - Multimea stabila interior maxima", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DrawText(Canvas canvas, double x, double y, string text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            Canvas.SetZIndex(textBlock, 1);
            canvas.Children.Add(textBlock);
        }

        private void PopulateAdjacencyGrid(int[,] aMatrix)
        {
            int rows = (int)Math.Sqrt(aMatrix.Length);
            int delta = 0;
            int beta = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    DrawText(aCanvas, delta, beta, aMatrix[i, j].ToString(), Color.FromRgb(0, 0, 0));
                    beta += 15;
                }
                beta = 0;
                delta += 15;
            }
        }

        private void PopulateIncidenceGrid(int[,] iMatrix)
        {
            int rows = selectedGraf.Nodes.Count;
            int columns = selectedGraf.Edges.Count;
            int delta = 0;
            int beta = 0;
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    DrawText(iCanvas, delta, beta, iMatrix[i, j].ToString(), Color.FromRgb(0, 0, 0));
                    beta += 15;
                }
                beta = 0;
                delta += 15;
            }
        }

        private void PopulateKirchhoffGrid(int[,] kMatrix)
        {
            int rows = (int)Math.Sqrt(kMatrix.Length);
            int delta = 0;
            int beta = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    DrawText(kCanvas, delta, beta, kMatrix[i, j]< 0 ? kMatrix[i, j].ToString() : " " + kMatrix[i, j].ToString(), Color.FromRgb(0, 0, 0));
                    beta += 15;
                }
                beta = 0;
                delta += 15;
            }
        }

        private void CloseDetailsButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
