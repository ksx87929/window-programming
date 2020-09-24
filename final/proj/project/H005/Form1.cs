using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace H005
{
    public partial class Form1 : Form
    {
        /*
         * p3=頻台+
         * p4=平台-
            p5=球+
            p6=球-
        */
        int xx, yy;  //球的位置
        int SIZEX=200; //平台大小
        int dx, dy;  //球的變動量
        int ditem = 15; //道具掉落速度
        int[] item_use = new int[4];
        int ball_sizex = 50;
        bool time_stop = false;
        int ball = 3,score = 0 ; //球數
        int brickRowCount, brickColCount, brickPadding, brickOffsetTop, brickOffsetLeft;
        Label[] brick = new Label[60];

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        int[] hit = new int[60];
        int i = 0;
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Left && pictureBox2.Left>= 0 && time_stop == false)
            {
                pictureBox2.Left -= 40;
            }
            if(e.KeyCode==Keys.Right && pictureBox2.Left< 940 && time_stop == false)
            {
                pictureBox2.Left += 40;
            }
            if(e.KeyCode==Keys.Space && time_stop==true)
            {
                dx = 20; dy = -20;
                time_stop = false;
                label4.Visible = false;
            }
            if(e.KeyCode==Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left += dx;
            pictureBox1.Top += dy;
            ///道具是否掉落中
            if(item_use[0]==1)
            {
                pictureBox3.Top += ditem;
                if(pictureBox3.Top>700)
                {
                    item_use[0] = 0;
                    pictureBox3.Location = new Point(-100, -100);
                }
            }
            if (item_use[1] == 1)
            {
                pictureBox4.Top += ditem;
                if (pictureBox4.Top > 700)
                {
                    item_use[1] = 0;
                    pictureBox4.Location = new Point(-100, -100);
                }
            }
            if (item_use[2] == 1)
            {
                pictureBox5.Top += ditem;
                if (pictureBox5.Top > 700)
                {
                    item_use[2] = 0;
                    pictureBox5.Location = new Point(-100, -100);
                }
            }
            if (item_use[3] == 1)
            {
                pictureBox6.Top += ditem;
                if (pictureBox6.Top > 700)
                {
                    item_use[3] = 0;
                    pictureBox6.Location = new Point(-100, -100);
                }
            }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds) && time_stop == false)
            {
                dy = -20;
                sp.Play();
            }
            if (pictureBox1.Left + dx > 1030 || pictureBox1.Left + dx < 0)
            {
                dx = -dx;
                sp.Play();
            }

            ///球道具碰撞
            if (pictureBox2.Bounds.IntersectsWith(pictureBox5.Bounds))
            {
                ball_sizex += 10;
                pictureBox1.Size = new System.Drawing.Size(ball_sizex, ball_sizex);
                pictureBox5.Location = new Point(-100, -100);
                item_use[2] = 0;
            }
            if (pictureBox2.Bounds.IntersectsWith(pictureBox6.Bounds))
            {
                ball_sizex -= 10;
                pictureBox1.Size = new System.Drawing.Size(ball_sizex, ball_sizex);
                pictureBox6.Location = new Point(-100, -100);
                item_use[3] = 0;
            }
            if (ball_sizex == 0 )
            {
                label4.Visible = true;
                label4.Text = "You Lose...按下ESC離開遊戲";
                timer1.Stop();
            }

            ///平台道具碰撞
            if (pictureBox2.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                SIZEX += 50;
                pictureBox2.Size = new System.Drawing.Size(SIZEX, 50);
                pictureBox3.Location = new Point(-100, -100);
                item_use[0] = 0;
            }
            if (pictureBox2.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                SIZEX -= 50;
                pictureBox2.Size = new System.Drawing.Size(SIZEX, 50);
                pictureBox4.Location = new Point(-100, -100);
                item_use[1] = 0;
            }
            if (SIZEX == 0)
            {
                label4.Visible = true;
                label4.Text = "You Lose...按下ESC離開遊戲";
                timer1.Stop();
            }

            ///球出界
            if (pictureBox1.Top + dy > 700 )
            {
                dy = -20;
                if(ball==0)
                {
                    label4.Visible = true;
                    label4.Text = "You Lose...按下ESC離開遊戲";
                    timer1.Stop();
                }
                else
                {
                    ball--;
                    pictureBox1.Left = pictureBox2.Left + 50;
                    pictureBox1.Top = pictureBox2.Top - 40;
                    dx = 0; dy = 0;
                    time_stop = true;
                    label4.Visible = true;
                    label4.Text = "按下空白鍵繼續...";
                    Item_re();
                }
            }
            if (pictureBox1.Top + dy < 0)
            {
                dy = -dy;
                sp.Play();
            }

            ///撞到方塊
            for (int b = 0; b < 60; b++)
            {
                if (pictureBox1.Bounds.IntersectsWith(brick[b].Bounds))
                {
                    sp.Play();
                    hit[b]--;
                    if(hit[b]==0)
                    {
                        score++;
                        int temp = b % 4;
                        if(temp==0 && item_use[0]==0)
                        {
                            pictureBox3.Left = brick[b].Left;
                            pictureBox3.Top = brick[b].Top;
                            item_use[temp] = 1;
                        }
                        else if(temp==1 && item_use[1]==0)
                        {
                            pictureBox4.Left = brick[b].Left;
                            pictureBox4.Top = brick[b].Top;
                            item_use[temp] = 1;
                        }
                        else if (temp == 2 && item_use[2] == 0)
                        {
                            pictureBox5.Left = brick[b].Left;
                            pictureBox5.Top = brick[b].Top;
                            item_use[temp] = 1;
                        }
                        else if (temp == 3 && item_use[3] == 0)
                        {
                            pictureBox6.Left = brick[b].Left;
                            pictureBox6.Top = brick[b].Top;
                            item_use[temp] = 1;
                        }
                        brick[b].Left = -500; brick[b].Top = -200;
                        if (dy == 20)
                        {
                            dx = -dx;
                        }
                        dy = 20;
                    }
                    else if(hit[b]==1)
                    {
                        brick[b].Image = H005.Properties.Resources.磚頭1;
                        if (dy == 20)
                        {
                            dx = -dx;
                        }
                        dy = 20;
                        break;
                    }
                    else if (hit[b] == 2)
                    {
                        brick[b].Image = H005.Properties.Resources.磚頭2;
                        if (dy == 20)
                        {
                            dx = -dx;
                        }
                        dy = 20;
                        break;
                    }
                    if (score == brickRowCount * brickColCount)
                    {
                        label4.Visible = true;
                        label4.Text = "You WIN...按下ESC離開遊戲";
                        timer1.Stop();
                    }
                    b = 0;
                }
            }
            label1.Text = "分數: " + Convert.ToString(score);
            label2.Text = "球數: " + Convert.ToString(ball);
        }

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.SetBounds(0, 0, 1080, 750);  //設定邊界 0, 0, 1080, 750
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Size = new System.Drawing.Size(SIZEX, 50);
            pictureBox1.Size = new System.Drawing.Size(ball_sizex, ball_sizex);
            sp.SoundLocation = @"cat_like2a.wav";
            xx = 520; yy = 600; i = 0;
            dx = 20; dy = -20;
            brickRowCount = 6; brickColCount = 10;
            brickPadding = 25; brickOffsetTop = 50; brickOffsetLeft = 30;
            pictureBox1.Top = yy; pictureBox1.Left = xx;
            pictureBox3.Location = new Point(-100, -100);
            pictureBox4.Location = new Point(-100, -100);
            pictureBox5.Location = new Point(-100, -100);
            pictureBox6.Location = new Point(-100, -100);
            this.Controls.Add(pictureBox3);
            this.Controls.Add(pictureBox4);
            this.Controls.Add(pictureBox5);
            this.Controls.Add(pictureBox6);
            for (int a=0;a<4;a++)
            {
                item_use[a] = 0;
            }
            for (int c = 0 ; c < brickColCount ; c++)
            {
                for(int r=0; r<brickRowCount; r++)
                {
                    brick[i] = new Label();
                    brick[i].Image = H005.Properties.Resources.磚頭3;
                    brick[i].Size = new System.Drawing.Size(75, 30);
                    brick[i].Location = new Point((c * (75 + brickPadding)) + brickOffsetLeft, (r * (30 + brickPadding)) + brickOffsetTop);
                    brick[i].BringToFront();
                    brick[i].BackColor = System.Drawing.Color.Transparent;
                    this.Controls.Add(brick[i]);
                    hit[i] = 3;
                    i++;
                }
            }
            label4.Visible = false;

            timer1.Start();
        }
        private void Item_re()
        {
            pictureBox3.Location = new Point(-100, -100);
            pictureBox4.Location = new Point(-100, -100);
            pictureBox5.Location = new Point(-100, -100);
            pictureBox6.Location = new Point(-100, -100);
            for (int a = 0; a < 4; a++)
            {
                item_use[a] = 0;
            }
        }
    }
}
