using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Random crandom = new Random();
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sp.SoundLocation = @"0211.WAV";
            pictureBox1.Left = 645;
            pictureBox1.Top = 40;
            pictureBox2.Left = 326;
            pictureBox2.Top = 380;
            pictureBox3.Left = -100;
            pictureBox4.Left=-500;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left -= crandom.Next(10) + 2;
            if (pictureBox1.Left <= -100)
                pictureBox1.Left = 645;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < pictureBox2.Left)
                pictureBox2.Left -= 15;
            if (e.X > pictureBox2.Left + 80)
                pictureBox2.Left += 15;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox3.Left = pictureBox2.Left + 30;
            pictureBox3.Top = pictureBox2.Top;
            pictureBox3.Visible = true;
            timer2.Start();

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox3.Top -= 30;
            if (pictureBox3.Top <= -200)
            {
                pictureBox3.Left = -100;
                timer2.Stop();
            }
            if(pictureBox3.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                pictureBox4.Left = pictureBox1.Left ;
                pictureBox4.Top = pictureBox1.Top;
                pictureBox1.Left = -500;
                pictureBox3.Left = -500;
                sp.Play();
            }
        }
    }
}
