using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _005
{
    public partial class Form1 : Form
    {
        int xx, yy;
        int dx, dy;
        int i,score;
        int life;
        int brickRowCount, brickColumnCount, brickPadding, brickOffsetTop,brickOffsetLeft;
        Label[] brick = new Label[60];
        int[] flag = new int[60];
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        System.Media.SoundPlayer qw = new System.Media.SoundPlayer();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.SetBounds(0, 0, 1080, 750);//設定邊界
            sp.SoundLocation = @"0211.WAV";
            qw.SoundLocation = @"0013.WAV";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left += dx;
            pictureBox1.Top += dy;
            if (pictureBox1.Left + dx > 1030 || pictureBox1.Left + dx < 0)
                dx = -dx;
            //if (pictureBox1.Top + dy > 680 || pictureBox1.Top + dy < 0)
            //  dy = -dy;
            if (pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                dy = -20;//dy = -dy;
                qw.Play();
            }
            if (pictureBox1.Top + dy < 0)
                dy = -dy;
            if (pictureBox1.Top + dy > 700)
            {
                if (life > 0)
                {
                    life--;
                    pictureBox1.Left = pictureBox2.Left + 50;
                    pictureBox1.Top = pictureBox2.Top;
                    dy = -20;
                }
                else
                {
                    label1.Text = "你輸了";
                    timer1.Stop();

                }
            }
                //timer1.Stop();
            for(i=0 ;i<60;i++)//Flag = 1 代表球出現  Flag= 0 表示打到
            {
                if (flag[i] == 1 && pictureBox1.Bounds.IntersectsWith(brick[i].Bounds))
                {
                    flag[i] = 0; score++ ;
                    sp.Play();
                    brick[i].Left = -500;brick[i].Top = -200;
                    dy = 20;
                    if (score==brickRowCount*brickColumnCount)
                    {
                        label1.Text = "你贏了";
                        timer1.Stop();
                    }
                }
            }
            label2.Text = " 分數 ：" + Convert.ToString(score);
            label3.Text = " 球數 ：" + Convert.ToString(life);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && pictureBox2.Left >= 0)
                pictureBox2.Left -= 30;
            if (e.KeyCode == Keys.Right && pictureBox2.Left < 940)
                pictureBox2.Left += 30;
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xx = 540; yy = 600; i = 0;//球的起始點
            dx = 20; dy = -20;//球的方向
            score = 0;life = 2;
            brickRowCount = 6; brickColumnCount = 10;
            brickPadding = 15;brickOffsetTop = 30;
            brickOffsetLeft = 30;
            pictureBox1.Left = xx;pictureBox1.Top = yy;
            for(var c=0;c<brickColumnCount;c++)
                for(var r=0;r<brickRowCount;r++)
                {
                    brick[i] = new Label();
                    brick[i].Image = _005.Properties.Resources._0215;
                    brick[i].Size = new System.Drawing.Size(85, 30);
                    brick[i].Location = new Point((c * (85 + brickPadding)) + brickOffsetLeft, (r * (30 + brickPadding)) + brickOffsetTop);
                    brick[i].BringToFront();
                    brick[i].BackColor = System.Drawing.Color.Transparent;
                    this.Controls.Add(brick[i]);
                    flag[i] = 1;
                    i++;
                }
            timer1.Start();
        }
    }
}
