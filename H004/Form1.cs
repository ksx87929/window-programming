using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace H004
{
    public partial class Form1 : Form
    {
        Button[] btn = new Button[9];
        Boolean isO = true;
        Boolean solo = true;
        static private int[,] WinGroup = new int[8, 3]
        {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };
        public Form1()
        {
            InitializeComponent();
            btn = new Button[9] { button3, button4, button5, button6, button7, button8, button9, button10, button11 };
        }

        private void initButtons()
        {
            button13.Visible = false;
            isO = true;
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i].Text = "";
                btn[i].BackColor = Color.LightBlue;
                btn[i].Font = new System.Drawing.Font("Arial", 50, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)1));
                btn[i].Click += new EventHandler(this.buttons_Click);
                btn[i].Visible = false;
            }
        }
        private void again()
        {
            isO = true;
            button13.Visible = false;
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i].Text = "";
                btn[i].BackColor = Color.LightBlue;
                btn[i].Font = new System.Drawing.Font("Arial", 50, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)1));
                btn[i].Visible = false;
            }
            button1.Visible = true;
            button12.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)///單人
        {
            isO = false;
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i].Text = "";
                btn[i].BackColor = Color.LightBlue;
                btn[i].Font = new System.Drawing.Font("Arial", 50, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)1));
                btn[i].Visible = true;
            }
            solo = true;
            button1.Visible = false;
            button12.Visible = false;
            button13.Visible = true;
            timer1.Start();
        }
        private void button12_Click(object sender, EventArgs e)///雙人
        {
            isO = true;
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i].Text = "";
                btn[i].BackColor = Color.LightBlue;
                btn[i].Font = new System.Drawing.Font("Arial", 50, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)1));
                btn[i].Visible = true;
            }
            solo = false;
            button12.Visible = false;
            button1.Visible = false;
            button13.Visible = true;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            again();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initButtons();
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (!isO && btn[i].Text == "")
                {
                    btn[i].Text = "X";
                    btn[i].BackColor = Color.LightPink;
                    isO = !isO;
                    timer1.Stop();
                }
            }
            bool[] GameStatus = CheckWinGroup(btn);
            if (GameStatus[0])
            {
                DialogResult dr = MessageBox.Show("遊戲結束....\r\nO獲勝\r\n是否重新開始遊戲", "遊戲結束", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    again();
                }
                else
                    this.Close();
                return;
            }
            if (GameStatus[1])
            {
                DialogResult dr = MessageBox.Show("遊戲結束....\r\nX獲勝\r\n是否重新開始遊戲", "遊戲結束", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    again();
                }
                else
                    this.Close();
                return;
            }
            if (GameStatus[2])
            {
                DialogResult dr = MessageBox.Show("遊戲結束....\r\n和局\r\n是否重新開始遊戲", "遊戲結束", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                    again();
                else
                    this.Close();
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            Button tmpButton = (Button)sender;
            if (tmpButton.Text != "")
            {
                MessageBox.Show("請勿重複選擇", "提示", MessageBoxButtons.OK);
                return;
            }
            if (isO)
            {
                tmpButton.Text = "O";
                tmpButton.BackColor = Color.LightGreen;
            }
            else
            {
                tmpButton.Text = "X";
                tmpButton.BackColor = Color.LightPink;
            }
            isO = !isO;

            bool[] GameStatus = CheckWinGroup(btn);
            if (GameStatus[0])
            {
                DialogResult dr = MessageBox.Show("遊戲結束....\r\nO獲勝\r\n是否重新開始遊戲", "遊戲結束", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    again();
                }
                else
                    this.Close();
                return;
            }
            if (GameStatus[1])
            {
                DialogResult dr = MessageBox.Show("遊戲結束....\r\nX獲勝\r\n是否重新開始遊戲", "遊戲結束", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    again();
                }
                else
                    this.Close();
                return;
            }
            if (GameStatus[2])
            {
                DialogResult dr = MessageBox.Show("遊戲結束....\r\n和局\r\n是否重新開始遊戲", "遊戲結束", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                    again();
                else
                    this.Close();
                return;
            }
            if (solo)
            {
                timer1.Start();
            }
        }
        private bool[] CheckWinGroup(Button[] myControls)
        {
            bool[] gameWinOver = new bool[3] { false, false ,false};
            int btnIsUse = 1;
            for (int i = 0; i < 8; i++)
            {
                int a = WinGroup[i, 0];
                int b = WinGroup[i, 1];
                int c = WinGroup[i, 2];
                Button b1 = myControls[a];
                Button b2 = myControls[b];
                Button b3 = myControls[c];

                if (b1.Text == "" || b2.Text == "" || b3.Text == "")
                    continue;

                if (b1.Text == b2.Text && b2.Text == b3.Text && b1.Text=="O" )
                {
                    b1.BackColor = b2.BackColor = b3.BackColor = Color.LightCoral;
                    b1.Font = b2.Font = b3.Font = new System.Drawing.Font("Times New Roman", 35, System.Drawing.FontStyle.Italic & System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)1));
                    gameWinOver[0] = true;
                    break;
                }

                if (b1.Text == b2.Text && b2.Text == b3.Text && b1.Text == "X")
                {
                    b1.BackColor = b2.BackColor = b3.BackColor = Color.LightCoral;
                    b1.Font = b2.Font = b3.Font = new System.Drawing.Font("Times New Roman", 35, System.Drawing.FontStyle.Italic & System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)1));
                    gameWinOver[1] = true;
                    break;
                }

                if (myControls[i].Text != "")
                {
                    btnIsUse++;
                    if (btnIsUse == 9) gameWinOver[2] = true;
                }
            }
            return gameWinOver;
        }
    }
}