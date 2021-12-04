using GrafLib;
using System;
using System.Collections.Generic;
using System.Reflection;
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
        
        //Pentru functia de GenerateColor
        //Random rnd = new Random();

        List<Brushes> brushes = new List<Brushes>();

        // Variabila pentru capetele unei muchii.
        int n = 0;

        Dictionary<Rectangle, Node> nodePairs = new Dictionary<Rectangle, Node>();
        Dictionary<Line, Edge> edgePairs = new Dictionary<Line, Edge>();

        Dictionary<Node, Rectangle> rectPairs = new Dictionary<Node, Rectangle>();
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
                    drawCanvas.Children.RemoveAt(drawCanvas.Children.IndexOf(selectedRectangle) + 1);

                    //remove from graphics
                    drawCanvas.Children.Remove(selectedRectangle);
                    //remove from dictionary
                    nodePairs.Remove(selectedRectangle);

                    //TODO - Just inserted. Check for behaviour.
                    rectPairs.Remove(selectedNode);

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

                    //TODO - Just inserted. Check for behaviour.
                    rectPairs.Add(newNode, newRectangle);

                    DrawText(drawCanvas, Mouse.GetPosition(drawCanvas).X, Mouse.GetPosition(drawCanvas).Y, newNode.Name, Color.FromRgb(0, 0, 0));
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

                    //remove associated TexBox from graphics
                    drawCanvas.Children.RemoveAt(drawCanvas.Children.IndexOf(selectedLine) + 1);

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

                            DrawText(drawCanvas, (newLine.X2 - newLine.X1) / 2 + newLine.X1, (newLine.Y2 - newLine.Y1) / 2 + newLine.Y1, newEdge.Name, Color.FromRgb(0, 0, 0));

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
            graf.IncidenceMatrix = Graf.CreateIncidenceMatrix(graf);
            graf.MaxGrade = Graf.FindMaxGrade(graf);
            graf.MinGrade = Graf.FindMinGrade(graf);
            SaveGrafToFile();
            mouseStatus.Content = $"Successfully saved {graf.Name} to file.";
        }
        private void DeleteGrafButton_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvasAndDeleteGraf();
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
            if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Grafuri\\"))
            {
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Grafuri");
            }
            System.IO.File.WriteAllBytes($@"Grafuri\{graf.Name}.png", ms.ToArray());
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
        private void CreateMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            mCanvas.Children.Clear();
            // From Adjacency Matrix
            if (fromMatrixComboBox.SelectedIndex == 0)
            {
                // To Incidency Matrix
                if (toMatrixComboBox.SelectedIndex == 1)
                {
                    //graf.CreateIfromA(graf.AdjacencyMatrix);
                    PopulateIncidenceGrid(graf.CreateIfromA(graf.AdjacencyMatrix));
                }
                // To Kirchhoff Matrix
                else if (toMatrixComboBox.SelectedIndex == 2)
                {
                    //graf.CreateKfromA(graf.AdjacencyMatrix);
                    PopulateGrid(graf.CreateKfromA(graf.AdjacencyMatrix));
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
                    //graf.CreateAfromI(graf.IncidenceMatrix);
                    PopulateGrid(graf.CreateAfromI(graf.IncidenceMatrix));
                }
                // To Kirchhoff Matrix
                else if (toMatrixComboBox.SelectedIndex == 2)
                {
                    //graf.CreateKfromI(graf.IncidenceMatrix);
                    PopulateGrid(graf.CreateKfromI(graf.IncidenceMatrix));
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
                    //graf.CreateAfromK(graf.KirchhoffMatrix);
                    PopulateGrid(graf.CreateAfromK(graf.KirchhoffMatrix));
                }
                // To Incidency Matrix
                else if (toMatrixComboBox.SelectedIndex == 1)
                {
                    //graf.CreateIfromK(graf.KirchhoffMatrix);
                    PopulateIncidenceGrid(graf.CreateIfromK(graf.KirchhoffMatrix));
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
                    graf.CreateIfromA(graf.IncidenceMatrix);
                    break;
                case 3:
                    graf.CreateIfromK(graf.IncidenceMatrix);
                    break;
                case 4:
                    graf.CreateKfromA(graf.KirchhoffMatrix);
                    break;
                case 5:
                    graf.CreateKfromI(graf.KirchhoffMatrix);
                    break;
                default:
                    break;
            }
        }
        private void fromMatrixComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mCanvas.Children.Clear();
            if (fromMatrixComboBox.SelectedIndex == 0)
            {
                PopulateGrid(graf.AdjacencyMatrix);
            }
            else if (fromMatrixComboBox.SelectedIndex == 1)
            {
                PopulateIncidenceGrid(graf.IncidenceMatrix);
            }
            else if (fromMatrixComboBox.SelectedIndex == 2)
            {
                PopulateGrid(graf.KirchhoffMatrix);
            }
            else
            {
                mCanvas.Children.Clear();
            }
        }
        private void PopulateGrid(int[,] aMatrix)
        {
            int rows = (int)Math.Sqrt(aMatrix.Length);
            int delta = 0;
            int beta = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    DrawText(mCanvas, delta, beta, aMatrix[i, j] < 0 ? aMatrix[i, j].ToString() : " " + aMatrix[i, j].ToString(), Color.FromRgb(0, 0, 0));
                    beta += 15;
                }
                beta = 0;
                delta += 15;
            }
        }
        private void PopulateIncidenceGrid(int[,] iMatrix)
        {
            int rows = graf.Nodes.Count;
            int columns = graf.Edges.Count;
            int delta = 0;
            int beta = 0;
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    DrawText(mCanvas, delta, beta, iMatrix[i, j].ToString(), Color.FromRgb(0, 0, 0));
                    beta += 15;
                }
                beta = 0;
                delta += 15;
            }
        }

        private void nodesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void edgesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Edge selectedEdge = (Edge)e.AddedItems[0];
            EdgeDetails ed = new EdgeDetails(selectedEdge);
            ed.Show();
        }

        private void ColourGraphButton_Click(object sender, RoutedEventArgs e)
        {
            ColorGraph(graf);
        }

        private void ColorGraph(Graf graf)
        {
            //Node startNode = PickRandomNode(graf.Nodes);
            ColorGraf(graf);
        }

        private Node PickRandomNode(List<Node> nodes)
        {
            Random rnd = new Random();
            int nodeId = rnd.Next(nodes.Count);
            return nodes[nodeId];
        }

        private void ColorGraf(Graf graf)
        {
            //Cleanup
            foreach (Node node in graf.Nodes)
            {
                node.isDisabled = false;
                node.isColored = false;
            }

            List<Node> varfuriGraf = new List<Node>(graf.Nodes);
            List<Node> varfuriColorate = new List<Node>();
            List<Node> varfuriDisponibile = new List<Node>(varfuriGraf);


            List<Brush> colors = new List<Brush>();
            colors.Add(Brushes.Blue);
            colors.Add(Brushes.Green);
            colors.Add(Brushes.Red);
            colors.Add(Brushes.Orange);
            colors.Add(Brushes.Orchid);
            colors.Add(Brushes.Purple);
            colors.Add(Brushes.Coral);
            colors.Add(Brushes.Yellow);
            int i = 0;


            while (varfuriColorate.Count < varfuriGraf.Count)
            {
                Brush color = colors[i++];
                //Brush color = GenerateColor();
                while (varfuriDisponibile.Count > 0)
                {
                    Node selectedNode = PickRandomNode(varfuriDisponibile);
                    ColorNode(selectedNode, color);
                    selectedNode.isColored = true;
                    selectedNode.isDisabled = true;
                    varfuriColorate.Add(selectedNode);
                    varfuriDisponibile.Remove(selectedNode);
                    //Disable adjacent nodes, if any
                    if (selectedNode.AdjacentNodes.Count > 0)
                    {
                        foreach (Node adjacentNode in selectedNode.AdjacentNodes)
                        {
                            adjacentNode.isDisabled = true;
                            varfuriDisponibile.Remove(adjacentNode);
                        }
                    }
                }
                //reseteaza varfurile indisponibile necolorate
                foreach (Node uncoloredNode in varfuriGraf.FindAll(x => x.isColored == false))
                {
                    uncoloredNode.isDisabled = false;
                    //   if (!varfuriDisponibile.Contains(uncoloredNode))
                    //{
                    varfuriDisponibile.Add(uncoloredNode);
                    //}
                }
            }
        }

        //Probleme cu compilerul
        [Obsolete("Aceasta metoda creaza probleme cu functionarea normala a programului.", true)]
        private Brush GenerateColor()
        {
            Brush result = Brushes.Transparent;

            //TODO - comenteaza linia urmatoare
            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        //Probleme cu compilerul
        [Obsolete("Aceasta metoda creaza probleme cu functionarea normala a programului.", true)]
        private Brush GenerateColor2()
        {
            List<Brush> colors = new List<Brush>();
            Random rnd = new Random();
            colors.Add(Brushes.Blue);
            colors.Add(Brushes.Green);
            colors.Add(Brushes.Red);
            colors.Add(Brushes.Orange);
            colors.Add(Brushes.Orchid);
            colors.Add(Brushes.Purple);
            colors.Add(Brushes.Coral);
            colors.Add(Brushes.Yellow);

            Brush color = colors[rnd.Next(colors.Count)];
            return color;
        }

        private void ColorNode(Node selectedNode, Brush color)
        {
            Rectangle selectedRectangle = rectPairs[selectedNode];
            selectedRectangle.Stroke = color;
        }

        private void generateMSTButton_Click(object sender, RoutedEventArgs e)
        {
            generateMST_Kruskal(graf);
        }

        private void generateMST_Kruskal(Graf graf)
        {
            //Cleanup
            int groupNumber = 0;
            foreach (Node node in graf.Nodes)
                node.group = 0;

            List<Edge> orderedEdges = new List<Edge>(graf.Edges);
            EdgeComparer ec = new EdgeComparer();
            orderedEdges.Sort(ec);

            List<Edge> minimalSpanningTree = new List<Edge>();

            foreach (Edge edge in orderedEdges)
            {
                if (edge.AdjacentNodes[0].group == 0 && edge.AdjacentNodes[1].group == 0)
                {
                    edge.AdjacentNodes[0].group = ++groupNumber;
                    edge.AdjacentNodes[1].group = edge.AdjacentNodes[0].group;
                }
                else if (edge.AdjacentNodes[0].group != edge.AdjacentNodes[1].group)
                {
                    if (edge.AdjacentNodes[0].group > edge.AdjacentNodes[1].group)
                    {
                        foreach (Node node in graf.Nodes.FindAll(x => x.group == edge.AdjacentNodes[1].group))
                        {
                            node.group = edge.AdjacentNodes[0].group;
                        }
                    }
                    else
                    {
                        foreach (Node node in graf.Nodes.FindAll(x => x.group == edge.AdjacentNodes[0].group))
                        {
                            node.group = edge.AdjacentNodes[1].group;
                        }
                    }
                    minimalSpanningTree.Add(edge);
                }
            }
            MessageBox.Show(minimalSpanningTree.ToString());
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
