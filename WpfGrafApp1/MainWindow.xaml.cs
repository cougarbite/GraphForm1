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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGrafApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graf graf = new Graf();
        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();
        Line newLine = new Line();
        Node node1, node2;

        // Variabila pentru capetele unei muchii.
        int n = 0;

        Dictionary<Rectangle, Node> nodePairs = new Dictionary<Rectangle, Node>();
        Dictionary<Line, Edge> edgePairs = new Dictionary<Line, Edge>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void drawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (drawNodeRadioButton.IsChecked == true)
            {
                if (e.OriginalSource is Rectangle)
                {
                    Rectangle selectedRectangle = (Rectangle)e.OriginalSource;
                    Node selectedNode = nodePairs[selectedRectangle];

                    //remove from Graf list
                    graf.Nodes.Remove(selectedNode);
                    nodes.Remove(selectedNode);

                    //remove from graphics
                    drawCanvas.Children.Remove(selectedRectangle);

                    //remove from dictionary
                    nodePairs.Remove(selectedRectangle);
                    RefreshNodesListBox();
                }
                else
                {
                    //Ellipse ellipse = new Ellipse { Width = 20, Height = 20, Stroke = Brushes.Black, StrokeThickness = 5 };
                    //Canvas.
                    Rectangle newRectangle = new Rectangle { Width = 10, Height = 10, Stroke = Brushes.Black, StrokeThickness = 5 };
                    Canvas.SetLeft(newRectangle, Mouse.GetPosition(drawCanvas).X - 5);
                    Canvas.SetTop(newRectangle, Mouse.GetPosition(drawCanvas).Y - 5);
                    Canvas.SetZIndex(newRectangle, 1);
                    drawCanvas.Children.Add(newRectangle);
                    Node newNode = CreateNode(newRectangle);
                    nodePairs.Add(newRectangle, newNode);
                }
            }
            else if (drawEdgeRadioButton.IsChecked == true)
            {
                if (e.OriginalSource is Line)
                {
                    Line selectedLine = (Line)e.OriginalSource;
                    Edge selectedEdge = edgePairs[selectedLine];
                    selectedEdge.DeleteEdge();

                    //remove from Graf list
                    graf.Edges.Remove(selectedEdge);
                    edges.Remove(selectedEdge);

                    //remove from graphics
                    drawCanvas.Children.Remove(selectedLine);

                    //remove from dictionary
                    edgePairs.Remove(selectedLine);
                    RefreshEdgesListBox();
                }
                else
                {
                    if (n % 2 == 0)
                    {
                        if (e.OriginalSource is Rectangle)
                        {
                            newLine.Stroke = Brushes.Red;
                            newLine.StrokeThickness = 2;
                            newLine.X1 = Mouse.GetPosition(drawCanvas).X;
                            newLine.Y1 = Mouse.GetPosition(drawCanvas).Y;
                            Rectangle selectedRectangle = (Rectangle)e.OriginalSource;
                            node1 = nodePairs[selectedRectangle];
                            n++;
                        }
                    }
                    else if (n % 2 == 1)
                    {
                        if (e.OriginalSource is Rectangle)
                        {
                            newLine.X2 = Mouse.GetPosition(drawCanvas).X;
                            newLine.Y2 = Mouse.GetPosition(drawCanvas).Y;
                            Rectangle selectedRectangle = (Rectangle)e.OriginalSource;
                            node2 = nodePairs[selectedRectangle];
                            n++;
                            Canvas.SetZIndex(newLine, 0);
                            drawCanvas.Children.Add(newLine);
                            Edge newEdge = CreateEdge(node1, node2);
                            edgePairs.Add(newLine, newEdge);
                            newLine = new Line();
                        }
                    }
                }
            }
        }
        private Node CreateNode(Rectangle newRectangle)
        {
            Node node = new Node((int)Mouse.GetPosition(drawCanvas).X, (int)Mouse.GetPosition(drawCanvas).Y);
            nodes.Add(node);
            graf.Nodes = nodes;
            RefreshNodesListBox();
            return node;
        }
        private Edge CreateEdge(Node n1, Node n2)
        {
            Edge edge = new Edge(n1, n2);
            edges.Add(edge);
            graf.Edges = edges;
            RefreshEdgesListBox();
            return edge;
        }
        private void RefreshNodesListBox()
        {
            nodesListBox.ItemsSource = null;
            nodesListBox.ItemsSource = graf.Nodes;
        }
        private void RefreshEdgesListBox()
        {
            edgesListBox.ItemsSource = null;
            edgesListBox.ItemsSource = graf.Edges;
        }

        private void DeleteGrafButton_Click(object sender, RoutedEventArgs e)
        {
            drawCanvas.Children.Clear();
            graf = new Graf();
            nodes = new List<Node>();
            edges = new List<Edge>();
            RefreshNodesListBox();
            RefreshEdgesListBox();
            Node.createdNodes = 1;
            Edge.createdEdges = 1;
        }

        private void CreateGrafButton_Click(object sender, RoutedEventArgs e)
        {
            SaveGrafToFile();
            //CheckForAdjacentNodes(graf);
            //CheckForAdjacentEdges(graf);
        }

        private void SaveGrafToFile()
        {
            Rect rect = new Rect(drawCanvas.Margin.Left, drawCanvas.Margin.Top, drawCanvas.ActualWidth, drawCanvas.ActualHeight);
            //Rect rect = new Rect(drawCanvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
              (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(drawCanvas);
            //endcode as PNG
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            //save to memory stream
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            System.IO.File.WriteAllBytes($@"..\..\Grafuri\{graf.Name}.png", ms.ToArray());
        }

        private void CheckForAdjacentNodes(Graf g)
        {

            throw new NotImplementedException();
        }
        private void CheckForAdjacentEdges(Graf g)
        {
            throw new NotImplementedException();
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseStatus.Content = $"X: {Mouse.GetPosition(drawCanvas).X} Y:{Mouse.GetPosition(drawCanvas).Y}";
        }
    }
}
