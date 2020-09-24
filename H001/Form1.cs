using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace H001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Left = 300;
            pictureBox1.Top = 300;
            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            pictureBox1.Left = pictureBox1.Left - 6;
            if (pictureBox1.Left <= 0)
            {
                timer1.Stop();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
