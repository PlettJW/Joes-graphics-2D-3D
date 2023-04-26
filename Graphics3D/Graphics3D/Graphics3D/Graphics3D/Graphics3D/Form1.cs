using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game2D;

namespace Graphics3D
{


    public partial class Form1 : Form
    {
        List<Cube> cubes = new List<Cube>();
        List<Ball3D> balls = new List<Ball3D>();
        
       
        double distance = 1000;
        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += Form1_MouseWheel;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                distance *= 1.1;
            else if (e.Delta < 0)
                distance /= 1.1;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cube cube = new Cube();

            cube.Scale(100);

            cube.HideFace(Side.front);

            cubes.Add(cube);

           

            Ball3D theBall = new Ball3D(new Point3D(-30, 0, 0),25);
            theBall.Velocity = new Point3D(-1, 0, 0);
            theBall.Mass = 1;

            balls.Add(theBall);

            theBall = new Ball3D(new Point3D(30, 0, 0), 25);
            theBall.Velocity = new Point3D(1, 0, 0);
            theBall.Mass = 1;

            balls.Add(theBall);

            timer1.Enabled = true;



        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.X:
                    if (e.Modifiers == Keys.Shift)
                        foreach (Cube cube in cubes)
                            cube.Rotate(new Point3D(-1, 0, 0));
                    else
                        foreach (Cube cube in cubes)
                            cube.Rotate(new Point3D(1, 0, 0));
                    break;
                case Keys.Y:
                    if (e.Modifiers == Keys.Shift)
                        foreach (Cube cube in cubes)
                            cube.Rotate(new Point3D(0, -1, 0));
                    else
                        foreach (Cube cube in cubes)
                            cube.Rotate(new Point3D(0, 1, 0));
                    break;
                case Keys.Z:
                    if (e.Modifiers == Keys.Shift)
                        foreach (Cube cube in cubes)
                            cube.Rotate(new Point3D(0, 0, -1));
                    else
                        foreach (Cube cube in cubes)
                            cube.Rotate(new Point3D(0, 0, 1));
                    break;
                case Keys.P:
                    if (timer1.Enabled)
                        timer1.Enabled = false;
                    else
                        timer1.Enabled = true;
                    break;
                case Keys.F:
                    if (e.Modifiers == Keys.Shift)
                        foreach (Cube cube in cubes)
                            cube.UnRotate(new Point3D(-1, -1, -1));
                    else
                        foreach (Cube cube in cubes)
                            cube.Rotate(new Point3D(1, 1, 1));
                    break;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Ball3D ball in balls)
                ball.Move();

            foreach (Cube cube in cubes)
                foreach (Ball3D ball in balls)
                    cube.Bounce(ball);

          //  for (int i = 0; i < balls.Count; i++)
               // for (int j = 0; j < balls.Count; j++)
                 //   balls[i].Bounce(balls[j]);

                

            

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);

            foreach(Cube cube in cubes)
             cube.Draw(e.Graphics, distance, Face.back);

            //foreach (Ball3D ball in balls)
            foreach(Ball3D ball in balls)
                ball.Draw(e.Graphics, distance);

            foreach (Cube cube in cubes)
                cube.Draw(e.Graphics, distance, Face.front);

            

        }
    }
}
