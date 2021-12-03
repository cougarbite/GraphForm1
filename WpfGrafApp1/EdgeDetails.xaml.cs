using GrafLib;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EdgeDetails.xaml
    /// </summary>
    public partial class EdgeDetails : Window
    {
        Edge selectedEdge = null;
        public EdgeDetails(Edge edge)
        {
            InitializeComponent();
            selectedEdge = edge;
            edgeNameLabel.Content = selectedEdge;
            edgeWeightTextBox.Text = selectedEdge.Weight.ToString();

        }
        private void UpdateWeight(Edge edge)
        {
            edge.Weight = int.Parse(edgeWeightTextBox.Text);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            UpdateWeight(selectedEdge);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
