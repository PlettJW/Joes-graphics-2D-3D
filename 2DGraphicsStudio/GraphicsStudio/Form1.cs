using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsStudio
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// Create a list of balls
        /// </summary>
        List<Ball2D> balls = new List<Ball2D>();
        /// <summary>
        /// Creates a list of lines
        /// </summary>
        List<Line2D> lines = new List<Line2D>();

        List<Rectangle2D> rectangles = new List<Rectangle2D>();

        List<Polygon2D> polygons = new List<Polygon2D>();
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Ball2D ball in balls)
                ball.Move();
            //perform ball ball collisions
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    balls[i].Bounce(balls[j]);
                }
            }

            foreach (Ball2D ball in balls)
                foreach (Line2D line in lines)
                    line.Bounce(ball);

            foreach (Ball2D ball in balls)
                foreach (Rectangle2D rect in rectangles)
                    foreach (Line2D edge in rect.Edges)
                        edge.Bounce(ball);

            foreach (Ball2D ball in balls)
                foreach (Polygon2D poly in polygons)
                    foreach (Line2D edge in poly.Edges)
                        edge.Bounce(ball);

            foreach (Polygon2D poly in polygons)
                poly.Rotate(0.1);

            this.Invalidate();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Ball2D aBall = new Ball2D(new Point2D(200, 700), 75.1);
            aBall.Velocity = new Point2D(10, 4);
            aBall.Pen = new Pen(Color.Crimson, 3);
            aBall.Brush = new SolidBrush(Color.Violet);
            balls.Add(aBall);

            aBall = new Ball2D(new Point2D(200, 800), 75.1);
            aBall.Velocity = new Point2D(10, 4);
            aBall.Pen = new Pen(Color.Crimson, 3);
            aBall.Brush = new SolidBrush(Color.Violet);
            balls.Add(aBall);

            aBall = new Ball2D(new Point2D(1200, 450), 12.1);
            aBall.Velocity = new Point2D(-10, 0);
            aBall.Pen = new Pen(Color.OrangeRed, 3);
            aBall.Brush = new SolidBrush(Color.LimeGreen);
            balls.Add(aBall);

            aBall = new Ball2D(new Point2D(1212, 462), 12.1);
            aBall.Velocity = new Point2D(-10, 0);
            aBall.Pen = new Pen(Color.OrangeRed, 3);
            aBall.Brush = new SolidBrush(Color.LimeGreen);
            balls.Add(aBall);

            Line2D aLine = new Line2D(new Point2D(600.1, 450.1), new Point2D(900.1, 450.1)); 
                aLine.Pen = new Pen(Color.Aquamarine,5);
            lines.Add(aLine);

            Rectangle2D rect = new Rectangle2D(new Point2D(0, 5), new Point2D(1500, 1000));
            rectangles.Add(rect);


            Polygon2D aPolygon = new Polygon2D();
            aPolygon.AddPt(new Point2D(200, 500));
            aPolygon.AddPt(new Point2D(500, 300));
            aPolygon.AddPt(new Point2D(50, 100));
            polygons.Add(aPolygon);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Dispose();
                    break;
                case Keys.Space:
                    timer1.Enabled = !timer1.Enabled;
                    break;
                case Keys.S:
                    timer1_Tick(null, null);
                    break;
                    

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ball2D ball in balls)
                ball.Draw(e.Graphics);

            foreach (Line2D line in lines)
                line.Draw(e.Graphics);

            foreach (Rectangle2D rect in rectangles)
                rect.Draw(e.Graphics);

            foreach (Polygon2D polygon in polygons)
                polygon.Draw(e.Graphics);
        }
    }
}
