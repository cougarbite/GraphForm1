using GrafLib;
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
            //grafDataGrid.ItemsSource = selectedGraf.IncidencyMatrix;
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
    }
}
