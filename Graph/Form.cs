using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Graph
{
    public partial class FormMap : Form
    {
        private Dictionary<int, Vertex> vertexList;
        private int vertexCount;
        private int[,] adjacencyMatrix;
        private Vertex startVertex;
        private Vertex targetVertex;
        private Label[] vertexLabel;
        private List<Vertex> pathList;

        private int[] vertexColorList;
        private int vertexColors;
        private int x;
        private int y;
        private Point connectFrom;
        private Point connectTo;
        private int connectFromIndex;
        private int connectToIndex;
        private bool isConnect;
        private bool isAdd;
        private bool isSelect;
        private Vertex rootVertex;
        private int colorCount;

        public FormMap()
        {
            InitializeComponent();
            vertexList = new Dictionary<int, Vertex>();
            vertexColorList = new int[100];
            vertexLabel = new Label[100];
            adjacencyMatrix = new int[100, 100];
            vertexColors = 10;

            btnConnect.Enabled = true;
            btnAdd.Enabled = false;
            btnSelect.Enabled = true;
            isConnect = false;
            isAdd = true;
            isSelect = false;
        }

        private void PanelGraph_MouseMove(object sender, MouseEventArgs e)
        {
            x = e.X - 10;
            y = e.Y - 10;
        }

        private void PanelGraph_MouseClick(object sender, MouseEventArgs e)
        {
            if (isAdd)
            {
                vertexLabel[vertexCount] = new Label
                {
                    Width = 22,
                    Height = 22,
                    AutoSize = false,
                    Location = new Point(x, y),
                    BackColor = Color.Red,
                    Font = new Font("Arial", 8),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = vertexCount.ToString()
                };
                vertexLabel[vertexCount].Click += new EventHandler(Vertex_Click);
                panelGraph.Controls.Add(vertexLabel[vertexCount]);
                AddVertex(vertexCount.ToString(), x, y);

                if (null == rootVertex)
                {
                    rootVertex = vertexList[vertexCount - 1];
                }
            }
        }

        private void Vertex_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                Label label = sender as Label;

                x = label.Location.X + 10;
                y = label.Location.Y + 10;

                if (connectFrom.IsEmpty)
                {
                    connectFrom = new Point(x, y);
                    connectFromIndex = Int32.Parse(label.Text);
                }
                else if (connectTo.IsEmpty)
                {
                    connectTo = new Point(x, y);
                    connectToIndex = Int32.Parse(label.Text);
                }

                if (!connectFrom.IsEmpty && !connectTo.IsEmpty)
                {
                    Pen pen = new Pen(Color.Black, 3);
                    Graphics graphics = panelGraph.CreateGraphics();
                    graphics.DrawLine(pen, connectFrom, connectTo);
                    pen.Dispose();
                    graphics.Dispose();
                    connectFrom = new Point();
                    connectTo = new Point();

                    AddEdge(vertexList[connectFromIndex], vertexList[connectToIndex]);
                }
            }

            if (isSelect)
            {
                Label label = sender as Label;
                int vertexIndex = Int32.Parse(label.Text);

                if (null == startVertex)
                {
                    label.BackColor = Color.Green;
                    startVertex = vertexList[vertexIndex];
                    lblStartVertex.Text = "Start Vertex: " + startVertex.Name;
                }
                else if (null == targetVertex)
                {
                    label.BackColor = Color.Green;
                    targetVertex = vertexList[vertexIndex];
                    lblTargetVertex.Text = "Target Vertex: " + targetVertex.Name;
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            bool isSearch = false;

            // Breadth-First Search
            if (cboSearch.SelectedIndex == 0)
            {
                if (null != startVertex && null != targetVertex)
                {
                    BreadthFirstSearch(startVertex, targetVertex);
                    isSearch = true;
                }
            }
            // Depthth-First Search
            else if (cboSearch.SelectedIndex == 1)
            {
                if (null != startVertex && null != targetVertex)
                {
                    DepthFirstSearch(startVertex, targetVertex);
                    isSearch = true;
                }
            }
            // Greedy Best-First Search
            else if (cboSearch.SelectedIndex == 2)
            {
                if (null != startVertex && null != targetVertex)
                {
                    GreedyBestFirstSearch(startVertex, targetVertex);
                    isSearch = true;
                }
            }
            // A* Search
            else if (cboSearch.SelectedIndex == 3)
            {
                if (null != startVertex && null != targetVertex)
                {
                    AStarSearch(startVertex, targetVertex);
                    isSearch = true;
                }
            }
            // CSP Search
            else if (cboSearch.SelectedIndex == 4)
            {
                if (null != rootVertex)
                {
                    CSPSearch(rootVertex);
                    isSearch = true;
                }
            }

            if (isSearch)
            {
                cboSearch.Enabled = false;
                btnSearch.Enabled = false;
                btnRemove.Enabled = true;
                btnRemoveAll.Enabled = true;
                btnConnect.Enabled = false;
                btnAdd.Enabled = false;
                btnSelect.Enabled = false;

                isConnect = false;
                isAdd = false;
                isSelect = false;
            }
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            lblStartVertex.Text = "Start Vertex:";
            lblTargetVertex.Text = "Target Vertex:";

            rootVertex = null;
            startVertex = null;
            targetVertex = null;
            cboSearch.Enabled = true;
            btnSearch.Enabled = true;
            btnRemove.Enabled = true;
            btnRemoveAll.Enabled = true;
            btnSelect.Enabled = true;
            btnConnect.Enabled = true;
            btnAdd.Enabled = false;

            isConnect = false;
            isAdd = true;
            isSelect = false;

            for (int i = 0; i < vertexCount; i++)
            {
                panelGraph.Controls.Remove(vertexLabel[i]);
            }

            connectFrom = new Point();
            connectTo = new Point();
            connectFromIndex = 0;
            connectToIndex = 0;
            colorCount = 0;
            vertexCount = 0;

            vertexList = new Dictionary<int, Vertex>();
            vertexColorList = new int[100];
            vertexLabel = new Label[100];
            adjacencyMatrix = new int[100, 100];

            panelGraph.Refresh();
            panelGraph.Invalidate();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            lblStartVertex.Text = "Start Vertex:";
            lblTargetVertex.Text = "Target Vertex:";

            startVertex = null;
            targetVertex = null;
            cboSearch.Enabled = true;
            btnSearch.Enabled = true;
            btnRemove.Enabled = true;
            btnSelect.Enabled = true;
            btnConnect.Enabled = true;
            btnAdd.Enabled = false;

            isConnect = false;
            isAdd = true;
            isSelect = false;

            for (int i = 0; i < vertexCount; i++)
            {
                vertexLabel[i].BackColor = Color.Red;
            }

            connectFrom = new Point();
            connectTo = new Point();
            connectFromIndex = 0;
            connectToIndex = 0;
            colorCount = 0;

            DisplayEdges();

            vertexColorList = new int[100];
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            btnAdd.Enabled = true;
            btnSelect.Enabled = true;
            isConnect = true;
            isAdd = false;
            isSelect = false;
            connectFrom = new Point();
            connectTo = new Point();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
            btnAdd.Enabled = false;
            btnSelect.Enabled = true;
            isConnect = false;
            isAdd = true;
            isSelect = false;
            connectFrom = new Point();
            connectTo = new Point();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
            btnAdd.Enabled = true;
            btnSelect.Enabled = false;
            isConnect = false;
            isAdd = false;
            isSelect = true;
            connectFrom = new Point();
            connectTo = new Point();
        }

        public void AddVertex(string name, int x, int y)
        {
            vertexList.Add(vertexCount, new Vertex(name, vertexCount, x, y));
            vertexCount++;
        }

        public void AddEdge(Vertex v1, Vertex v2)
        {
            if (v1.Index == v2.Index)
            {
                Console.WriteLine("Not a valid edge");
            }
            else
            {
                if (adjacencyMatrix[v1.Index, v2.Index] != 0)
                {
                    Console.WriteLine("Edge is already present.");
                }
                else
                {
                    adjacencyMatrix[v1.Index, v2.Index] = GetDistance(v1, v2);
                    adjacencyMatrix[v2.Index, v1.Index] = adjacencyMatrix[v1.Index, v2.Index];
                }
            }
        }

        public void DisplayEdges()
        {
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        Point point1 = new Point(vertexList[i].X + 10, vertexList[i].Y + 10);
                        Point point2 = new Point(vertexList[j].X + 10, vertexList[j].Y + 10);
                        Pen pen = new Pen(Color.Black, 3);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();
                    }
                }
            }
        }

        public int GetVertexIndex(String vertexName)
        {
            foreach (KeyValuePair<int, Vertex> entry in vertexList)
            {
                if (vertexName.Equals(entry.Value.Name))
                {
                    return entry.Key;
                }
            }
            return -1;
        }

        public int GetDistance(Vertex v1, Vertex v2)
        {
            double x = Math.Pow(v2.X - v1.X, 2);
            double y = Math.Pow(v2.Y - v1.Y, 2);
            return (int)Math.Round(Math.Pow((x + y), 0.5));
        }


        public void BreadthFirstSearch(Vertex startVertex, Vertex targetVertex)
        {
            bool[] visited = new bool[vertexCount];

            Queue<Vertex> queue = new Queue<Vertex>();
            List<string> pathList = new List<string>();

            visited[startVertex.Index] = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                Vertex vertex = queue.Dequeue();
                pathList.Add(vertex.Name);

                if (vertex.Name.Equals(targetVertex.Name))
                {
                    RetracePath(startVertex, targetVertex);
                    break;
                }

                List<Vertex> neighbors = GetNeighbors(vertex).OrderBy(e => e.Distance).ThenBy(e => e.Name).ToList();

                foreach (Vertex neighbor in neighbors)
                {
                    if (!visited[neighbor.Index])
                    {
                        Point point1 = new Point(vertex.X + 10, vertex.Y + 10);
                        Point point2 = new Point(neighbor.X + 10, neighbor.Y + 10);
                        Pen pen = new Pen(Color.Blue, 3);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();

                        visited[neighbor.Index] = true;
                        neighbor.Parent = vertex;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        public void DepthFirstSearch(Vertex startVertex, Vertex targetVertex)
        {
            bool[] visited = new bool[vertexCount];

            Stack<Vertex> stack = new Stack<Vertex>();
            List<string> pathList = new List<string>();

            visited[startVertex.Index] = true;
            stack.Push(startVertex);

            while (stack.Count > 0)
            {
                Vertex vertex = stack.Pop();
                pathList.Add(vertex.Name);

                if (vertex.Name.Equals(targetVertex.Name))
                {
                    RetracePath(startVertex, targetVertex);
                    break;
                }

                List<Vertex> neighbors = GetNeighbors(vertex).OrderByDescending(e => e.Distance).ThenBy(e => e.Name).ToList();

                foreach (Vertex neighbor in neighbors)
                {
                    if (!visited[neighbor.Index])
                    {
                        Point point1 = new Point(vertex.X + 10, vertex.Y + 10);
                        Point point2 = new Point(neighbor.X + 10, neighbor.Y + 10);
                        Pen pen = new Pen(Color.Blue, 3);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();

                        visited[neighbor.Index] = true;
                        neighbor.Parent = vertex;
                        stack.Push(neighbor);
                    }
                }
            }
        }

        public void GreedyBestFirstSearch(Vertex startVertex, Vertex targetVertex)
        {
            List<Vertex> openVertices = new List<Vertex>();
            HashSet<Vertex> closedVertices = new HashSet<Vertex>();
            openVertices.Add(startVertex);

            while (openVertices.Count > 0)
            {
                Vertex vertex = openVertices[0];

                for (int i = 1; i < openVertices.Count; i++)
                {
                    if (openVertices[i].FCost < vertex.FCost)
                    {
                        vertex = openVertices[i];
                    }
                }

                openVertices.Remove(vertex);
                closedVertices.Add(vertex);

                if (vertex == targetVertex)
                {
                    RetracePath(startVertex, targetVertex);
                    return;
                }

                foreach (Vertex neighbor in GetNeighbors(vertex))
                {
                    if (closedVertices.Contains(neighbor))
                    {
                        continue;
                    }

                    if (!openVertices.Contains(neighbor))
                    {
                        Point point1 = new Point(vertex.X + 10, vertex.Y + 10);
                        Point point2 = new Point(neighbor.X + 10, neighbor.Y + 10);
                        Pen pen = new Pen(Color.Blue, 3);
                        Graphics graphics = panelGraph.CreateGraphics();
                        graphics.DrawLine(pen, point1, point2);
                        pen.Dispose();
                        graphics.Dispose();

                        neighbor.HCost = GetDistance(neighbor, targetVertex);
                        neighbor.Parent = vertex;
                        openVertices.Add(neighbor);
                    }
                }
            }
        }

        public void AStarSearch(Vertex startVertex, Vertex targetVertex)
        {
            List<Vertex> openVertices = new List<Vertex>();
            HashSet<Vertex> closedVertices = new HashSet<Vertex>();
            openVertices.Add(startVertex);

            while (openVertices.Count > 0)
            {
                Vertex vertex = openVertices[0];

                for (int i = 1; i < openVertices.Count; i++)
                {
                    if (openVertices[i].FCost <= vertex.FCost)
                    {
                        if (openVertices[i].HCost < vertex.HCost)
                        {
                            vertex = openVertices[i];
                        }
                    }
                }

                openVertices.Remove(vertex);
                closedVertices.Add(vertex);

                if (vertex == targetVertex)
                {
                    RetracePath(startVertex, targetVertex);
                    return;
                }

                List<Vertex> neighbors = GetNeighbors(vertex).OrderByDescending(e => e.Distance).ThenBy(e => e.Name).ToList();

                foreach (Vertex neighbor in neighbors)
                {
                    if (closedVertices.Contains(neighbor))
                    {
                        continue;
                    }

                    int newCostToNeighbour = vertex.GCost + GetDistance(vertex, neighbor);
                    if (newCostToNeighbour < neighbor.GCost || !openVertices.Contains(neighbor))
                    {
                        neighbor.GCost = newCostToNeighbour;
                        neighbor.HCost = GetDistance(neighbor, targetVertex);
                        neighbor.Parent = vertex;

                        if (!openVertices.Contains(neighbor))
                        {
                            Point point1 = new Point(vertex.X + 10, vertex.Y + 10);
                            Point point2 = new Point(neighbor.X + 10, neighbor.Y + 10);
                            Pen pen = new Pen(Color.Blue, 3);
                            Graphics graphics = panelGraph.CreateGraphics();
                            graphics.DrawLine(pen, point1, point2);
                            pen.Dispose();
                            graphics.Dispose();

                            openVertices.Add(neighbor);
                        }
                    }
                }
            }
        }

        public void CSPSearch(Vertex rootVertex)
        {
            for (int color = 1; color <= vertexColors; color++)
            {
                if (colorCount < vertexCount)
                {
                    bool isSafe = true;

                    for (int i = 0; i < vertexCount; i++)
                    {
                        if (adjacencyMatrix[rootVertex.Index, i] != 0 && color == vertexColorList[i])
                        {
                            isSafe = false;
                            break;
                        }
                    }

                    if (isSafe)
                    {
                        vertexColorList[rootVertex.Index] = color;
                        VertexColor(rootVertex.Index, color);
                        colorCount++;

                        if (rootVertex.Index + 1 < vertexCount)
                        {
                            CSPSearch(vertexList[rootVertex.Index + 1]);
                        }
                    }
                }
            }
        }

        public List<Vertex> GetNeighbors(Vertex parentVertex)
        {
            List<Vertex> neighbors = new List<Vertex>();

            for (int i = 0; i < vertexCount; i++)
            {
                if (adjacencyMatrix[parentVertex.Index, i] != 0)
                {
                    Vertex vertex = vertexList[i];
                    vertex.Distance = GetDistance(parentVertex, vertex);
                    neighbors.Add(vertex);
                }
            }

            return neighbors;
        }

        public void RetracePath(Vertex startVertex, Vertex endVertex)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex currentVertex = endVertex;

            while (currentVertex != startVertex)
            {
                path.Add(currentVertex);
                Point point1 = new Point(currentVertex.X + 10, currentVertex.Y + 10);
                Point point2 = new Point(currentVertex.Parent.X + 10, currentVertex.Parent.Y + 10);
                Pen pen = new Pen(Color.Green, 3);
                Graphics graphics = panelGraph.CreateGraphics();
                graphics.DrawLine(pen, point1, point2);
                pen.Dispose();
                graphics.Dispose();
                currentVertex = currentVertex.Parent;
            }
            path.Add(startVertex);
            path.Reverse();
            pathList = path;
        }

        public void VertexColor(int index, int color)
        {
            Label labelVertex = vertexLabel[index];
            switch (color)
            {
                case 1:
                    labelVertex.BackColor = Color.Yellow;
                    break;
                case 2:
                    labelVertex.BackColor = Color.Blue;
                    break;
                case 3:
                    labelVertex.BackColor = Color.Green;
                    break;
                case 4:
                    labelVertex.BackColor = Color.Orange;
                    break;
                case 5:
                    labelVertex.BackColor = Color.Violet;
                    break;
                case 6:
                    labelVertex.BackColor = Color.YellowGreen;
                    break;
                case 7:
                    labelVertex.BackColor = Color.BlueViolet;
                    break;
                case 8:
                    labelVertex.BackColor = Color.OrangeRed;
                    break;
                case 9:
                    labelVertex.BackColor = Color.Brown;
                    break;
                case 10:
                    labelVertex.BackColor = Color.Maroon;
                    break;
            }
        }
    }
}