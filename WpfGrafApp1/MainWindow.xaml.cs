using GrafLib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            fromMatrixComboBox.Items.Add("Adjacency Matrix");
            fromMatrixComboBox.Items.Add("Incidency Matrix");
            fromMatrixComboBox.Items.Add("Kirchhoff Matrix");

            toMatrixComboBox.Items.Add("Adjacency Matrix");
            toMatrixComboBox.Items.Add("Incidency Matrix");
            toMatrixComboBox.Items.Add("Kirchhoff Matrix");
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
        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseStatus.Content = $"X: {Mouse.GetPosition(drawCanvas).X} Y:{Mouse.GetPosition(drawCanvas).Y}";
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

                    //remove associated TexBox from graphics
                    drawCanvas.Children.RemoveAt(drawCanvas.Children.IndexOf(selectedRectangle)+1);

                    //remove from graphics
                    drawCanvas.Children.Remove(selectedRectangle);
                    //remove from dictionary
                    nodePairs.Remove(selectedRectangle);
                    RefreshNodesListBox();
                }
                else
                {
                    Rectangle newRectangle = new Rectangle { Width = 10, Height = 10, Stroke = Brushes.Black, StrokeThickness = 5 };
                    Canvas.SetLeft(newRectangle, Mouse.GetPosition(drawCanvas).X - 5);
                    Canvas.SetTop(newRectangle, Mouse.GetPosition(drawCanvas).Y - 5);
                    Canvas.SetZIndex(newRectangle, 2);
                    drawCanvas.Children.Add(newRectangle);
                    Node newNode = CreateNode(newRectangle);
                    nodePairs.Add(newRectangle, newNode);

                    DrawText(Mouse.GetPosition(drawCanvas).X, Mouse.GetPosition(drawCanvas).Y, newNode.Name, Color.FromRgb(0,0, 0));
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

        private void DrawText(double x, double y, string text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            Canvas.SetZIndex(textBlock, 1);
            drawCanvas.Children.Add(textBlock);
        }

        private void ClearCanvasAndDeleteGraf()
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
            graf.AdjacencyMatrix = Graf.CreateAdjacencyMatrix(graf);
            graf.KirchhoffMatrix = Graf.CreateKirchhoffMatrix(graf);
            graf.IncidencyMatrix = Graf.CreateIncidencyMatrix(graf);
            graf.MaxGrade = Graf.FindMaxGrade(graf);
            graf.MinGrade = Graf.FindMinGrade(graf);
            SaveGrafToFile();
            mouseStatus.Content = $"Successfully saved {graf.Name} to file.";
        }
        private void DeleteGrafButton_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvasAndDeleteGraf();
        }
        private void DrawMatrixes(Graf graf)
        {
            //TODO - implement method for listing matrix transformation & graf details
            Grid grid = new Grid();
            grid.Width = 400;
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.AliceBlue);


            drawCanvas.Children.Add(grid);
        }
        private void SaveGrafToFile()
        {
            Rect rect = new Rect(drawCanvas.Margin.Left, drawCanvas.Margin.Top, drawCanvas.ActualWidth, drawCanvas.ActualHeight);
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
            if (!System.IO.Directory.Exists(@"..\..\Grafuri"))
            {
                System.IO.Directory.CreateDirectory(@"..\..\Grafuri");
            }
            System.IO.File.WriteAllBytes($@"..\..\Grafuri\{graf.Name}.png", ms.ToArray());
        }
        private int CreateSwitch()
        {
            int output = 0;

            return output;
        }

        private void ViewGrafButton_Click(object sender, RoutedEventArgs e)
        {
            Details details = new Details(graf);
            details.Show();
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            // From Adjacency Matrix
            if (fromMatrixComboBox.SelectedIndex == 0)
            {
                // To Incidency Matrix
                if (toMatrixComboBox.SelectedIndex == 1)
                {
                    graf.CreateIfromA(graf.AdjacencyMatrix);
                }
                // To Kirchhoff Matrix
                else if (toMatrixComboBox.SelectedIndex == 2)
                {
                    graf.CreateKfromA(graf.AdjacencyMatrix);
                }
                else
                {
                    MessageBox.Show("Select a final matrix!");
                }
            }
            // From Incidency Matrix
            else if (fromMatrixComboBox.SelectedIndex == 1)
            {
                // To Adjacency Matrix
                if (toMatrixComboBox.SelectedIndex == 0)
                {
                    graf.CreateAfromI(graf.IncidencyMatrix);
                }
                // To Kirchhoff Matrix
                else if (toMatrixComboBox.SelectedIndex == 2)
                {
                    graf.CreateKfromI(graf.IncidencyMatrix);
                }
                else
                {
                    MessageBox.Show("Select a final matrix!");
                }
            }
            // From Kirchhoff Matrix
            else if (fromMatrixComboBox.SelectedIndex == 2)
            {
                // To Adjacency Matrix
                if (toMatrixComboBox.SelectedIndex == 0)
                {
                    graf.CreateAfromK(graf.KirchhoffMatrix);
                }
                // To Incidency Matrix
                else if (toMatrixComboBox.SelectedIndex == 1)
                {
                    graf.CreateIfromK(graf.KirchhoffMatrix);
                }
                else
                {
                    MessageBox.Show("Select a final matrix!");
                }
            }
            else
            {
                MessageBox.Show("Select a matrix to start from!");
            }

            switch (CreateSwitch())
            {
                case 0:
                    break;
                case 1:
                    graf.CreateAfromK(graf.AdjacencyMatrix);
                    break;
                case 2:
                    graf.CreateIfromA(graf.IncidencyMatrix);
                    break;
                case 3:
                    graf.CreateIfromK(graf.IncidencyMatrix);
                    break;
                case 4:
                    graf.CreateKfromA(graf.KirchhoffMatrix);
                    break;
                case 5:
                    //graf.CreateKfromI(graf.KirchhoffMatrix);
                    break;
                default:
                    break;
            }
        }
    }
}
