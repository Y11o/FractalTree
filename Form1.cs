using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FractalTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int speed = 40;
        int lineThickness = 3;
        int degree = 1;
        Tree tree = new Tree();
        int[ , ] borders = new int[4,2];
        int fracIteration = 1;
        List<Tree.Node> nodes = new List<Tree.Node>();
        bool needGraph = false;
        int square = 1;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FillTree(int deg, int x, int y) {
            if (deg == 1) {
                tree.Add(x, y);
            } 
            else {
                int dist = Convert.ToInt32(Math.Pow(3, deg - 1));
                FillTree(deg - 1, x, y);
                FillTree(deg - 1, x - dist, y - dist);
                FillTree(deg - 1, x - dist, y + dist);
                FillTree(deg - 1, x + dist, y - dist);
                FillTree(deg - 1, x + dist, y + dist);
            } 
        }

        private void DrawGraph(Graphics graphics, List<Tree.Node> nod)
        {
            Pen edgesPen = new Pen(Color.Black);
            int rad = 30;
            List<Tree.Node> nodes = nod;
            List<List<Point>> points = new List<List<Point>>();
            for (int i = 0; i < degree + 1; i++)
            {
                points.Add(new List<Point>());
            }
            points[0].Add(new Point(1200, 700));
            int cnt = 0;
            for (int i = 1; i < degree + 1; i++) {
                foreach (Tree.Node node in nodes) {
                    if (node.depth == i) cnt++;
                }
                for (int j = 0; j < cnt; j++) {
                    points[i].Add(new Point(900 + j * 50, 700 - i * 100));
                }
                cnt = 0;
            }
            int counter = 0;
            graphics.FillEllipse(new SolidBrush(Color.Red), points[0][0].X - rad, points[0][0].Y - rad, rad * 2, rad * 2);
            for (int i = 1; i < degree + 1; i++) {
                for (int j = 0; j < points[i].Count; j++) {
                    Color color = new Color();
                    color = ChooseColor(counter % 6);
                    SolidBrush vertexBrush = new SolidBrush(Color.Red);
                    graphics.DrawLine(edgesPen, points[i][j], points[i - 1][j % points[i - 1].Count]);
                    graphics.FillEllipse(vertexBrush, points[i][j].X - (rad - i * 3) / 2, points[i][j].Y - (rad - i * 3) / 2, (rad - i * 3) * 2, (rad - i * 3) * 2);
                    counter++;
                }
                
            }
        }

        private void DrawFractal(Graphics graphics, int w, int h) {
            while (fracIteration <= degree) {
                if (fracIteration < 4) FillTree(fracIteration, w, 200 + h);
                if (fracIteration == 4) {
                    h = 0;
                    FillTree(fracIteration, 200 + w, 200 + h);
                }
                if (fracIteration == 5)
                {
                    w = 200;
                    h = 400;
                    FillTree(fracIteration, 200 + w, 200 + h);
                }
                if (fracIteration > 5)
                {
                    w = 750;
                    h = 250;
                    FillTree(fracIteration, 200 + w, 200 + h);
                }
                borders = tree.GetEdges();
                DisplayFractal(graphics, fracIteration);
                fracIteration++;
                tree.Delete();
                DrawFractal(graphics, borders[3, 0], Convert.ToInt32((borders[3, 1] + borders[2, 1]) / 2));
            }       
        }

        private Color ChooseColor(int index) {
            switch (index) {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.Red;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Blue;
                case 4:
                    return Color.DarkSalmon;
                case 5:
                    return Color.Magenta;
                case 6:
                    return Color.Brown;
            }
            return Color.Chocolate;
        }

        private void DisplayFractal(Graphics graphics, int index) {
            List<Tree.Node> nodes = tree.BFS();
            bool flag = true;
            int counter = 0;
            foreach (Tree.Node node in nodes) {
                Color color = new Color();
                color = ChooseColor(counter % 7);
                Pen squarePen = new Pen(Color.Red, lineThickness);
                graphics.DrawRectangle(squarePen, node.x, node.y, square, square);
                if (index == degree && flag && needGraph) {
                    flag = false;
                    DrawGraph(graphics, nodes);
                } 
                Thread.Sleep(speed);
                counter++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            int h = pictureBox1.Height / 2;
            int w = pictureBox1.Width / 2;
            tree = new Tree();
            borders = new int[4, 2];
            fracIteration = 1;
            nodes.Clear();
            graphics.TranslateTransform(w, h);
            graphics.ScaleTransform(1, -1);
            graphics.TranslateTransform(-w, -h);
            graphics.Clear(BackColor);
            DrawFractal(graphics, 100, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            degree = Convert.ToInt32(textBox1.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            lineThickness = Convert.ToInt32(textBox2.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex) {
                case 0:
                    speed = 80; break;
                case 1:
                    speed = 40; break;
                case 2:
                    speed = 20; break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBox1.Checked) {
                case false:
                    needGraph = false; break;
                case true:
                    needGraph = true; break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            square = Convert.ToInt32(textBox3.Text);
        }
    }
}
