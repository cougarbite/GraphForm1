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
            sb.Append("}");
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
        private void closeDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
