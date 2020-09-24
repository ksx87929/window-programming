using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace h003
{
    public partial class Form1 : Form
    {
        Random crandom = new Random();
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        int Firy;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sp.SoundLocation = @"0060.WAV";
            DoubleBuffered = true;
            timer1.Enabled = true;
            pictureBox1.Top = -3170; pictureBox1.Left = 375;
            pictureBox2.Top = 3600; pictureBox2.Left = 200;
            pictureBox6.Top = 3000; pictureBox6.Left = 155;
            pictureBox7.Top = 850; pictureBox7.Left = 120;
            pictureBox8.Top = 2150; pictureBox8.Left = 100;
            pictureBox1.Controls.Add(this.pictureBox6);
            pictureBox1.Controls.Add(this.pictureBox7);
            pictureBox1.Controls.Add(this.pictureBox8);
            Firy = pictureBox1.Top;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Controls.Add(this.pictureBox2);
            pictureBox2.Top -= 20;
            pictureBox1.Top += 20;
            sp.Play();
            if (pictureBox1.Top == 30)
            {
                pictureBox1.Top = Firy;
                pictureBox2.Top = 3600;
            }
            if (pictureBox2.Left <= 0 || pictureBox2.Left + pictureBox2.Width >= pictureBox1.Width)
            {
                timer1.Stop();
                pictureBox2.Image = pictureBox5.Image;
            }
            if (pictureBox1.Width >= 350)
            {
                pictureBox1.Width -= crandom.Next(4) + 1;
            }
            if (pictureBox2.Bounds.IntersectsWith(pictureBox6.Bounds))
            {
                timer1.Stop();
                pictureBox2.Image = pictureBox5.Image;
                pictureBox6.Image = null;
            }
            if (pictureBox2.Bounds.IntersectsWith(pictureBox7.Bounds))
            {
                timer1.Stop();
                pictureBox2.Image = pictureBox5.Image;
                pictureBox7.Image = null;
            }
            if (pictureBox2.Bounds.IntersectsWith(pictureBox8.Bounds))
            {
                timer1.Stop();
                pictureBox2.Image = pictureBox5.Image;
                pictureBox8.Image = null;
            }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox2.Left -= 30;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox2.Left += 30;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
    }
}
