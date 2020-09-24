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
        static int[,] map =
     {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,2,1,0,1},
            {1,0,0,0,1,1,1,0,0,0,0,0,0,1,0,0,1,1,0,1},
            {1,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,1,0,1},
            {1,0,0,0,1,0,0,3,0,0,0,0,0,1,1,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1},
            {1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,1,1,0,0,0,0,0,1,0,0,0,0,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,1},
            {1,0,1,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,0,1},
            {1,0,1,0,0,0,4,0,0,0,0,5,0,0,1,1,1,1,1,1},
            {1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {1,1,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1},
            {1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,0,0,1,0,1},
            {1,1,0,0,1,0,0,1,0,1,1,1,1,1,0,0,1,1,1,1},
            {1,2,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,2,1},
            {1,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        int x=1, y=1,sx, sy ;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*pictureBox1.Left = 22;pictureBox1.Top = 22;
            pictureBox2.Left = 150;pictureBox2.Top = 90;
            pictureBox4.Left = 130;pictureBox4.Top = 230;
            pictureBox3.Left = 235;pictureBox3.Top = 230;*/
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            sx = x;
            sy = y;
            if(e.KeyCode==Keys.Down)
            { x++; }//picturebox1.top+=32
            if(e.KeyCode==Keys.Up)
            { x--; }
            if (e.KeyCode == Keys.Right)
            { y++; }
            if (e.KeyCode == Keys.Left)
            { y--; }
            if(map[x,y]==1)
            {
                x = sx;
                y = sy;
            }
            if (map[x, y] == 3)
            {
                if (map[2 * x - sx, 2 * y - sy] == 0 || map[2 * x - sx, 2 * y - sy] == 2)
                {
                    pictureBox2.Top -= (sx - x) * 32;
                    pictureBox2.Left -= (sy - y) * 32;
                    map[x, y] = 0;
                    if (map[2 * x - sx, 2 * y - sy] == 2)
                    {
                        pictureBox2=null;
                    }
                    map[2 * x - sx, 2 * y - sy] = 3;
                }
                else
                {
                    x = sx;
                    y = sy;

                }
            }
            if (map[x, y] == 4)
            {
                if (map[2 * x - sx, 2 * y - sy] == 0 || map[2 * x - sx, 2 * y - sy] == 2)
                {
                    pictureBox4.Top -= (sx - x) * 32;
                    pictureBox4.Left -= (sy - y) * 32;
                    map[x, y] = 0;
                    if (map[2 * x - sx, 2 * y - sy] == 2)
                    {
                        pictureBox4 = null;
                    }
                    map[2 * x - sx, 2 * y - sy] = 4;
                }
                else
                {
                    x = sx;
                    y = sy;

                }
            }
            if (map[x, y] == 5)
            {
                if (map[2 * x - sx, 2 * y - sy] == 0 || map[2 * x - sx, 2 * y - sy] == 2)
                {
                    pictureBox3.Top -= (sx - x) * 32;
                    pictureBox3.Left -= (sy - y) * 32;
                    map[x, y] = 0;
                    if (map[2 * x - sx, 2 * y - sy] == 2)
                    {
                        pictureBox3 = null;
                    }
                    map[2 * x - sx, 2 * y - sy] = 5;
                }
                else
                {
                    x = sx;
                    y = sy;

                }
            }
            if (pictureBox2 == null && pictureBox3 == null&&pictureBox4==null)
            {
                label2.Text = "   完成囉";
            }
            pictureBox1.Top = -11 + x * 32;
            pictureBox1.Left = -8+y * 32;
        } 
    }
}
