using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.Util;
using Emgu.CV.Structure;


namespace _008
{
    public partial class Form1 : Form
    {
        VideoCapture cap = new VideoCapture();
        int fps;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP4文件|*.mp4|AVI文件|*.avi|RMVB文件|*.rmvb|WMV文件|*.wmv|MKV文件|*.mkv|所有文件|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.Idle += Application_Idle;
                cap = new VideoCapture(openFileDialog.FileName);
                fps = (int)cap.GetCaptureProperty(CapProp.Fps);
            }
        }
        private void Application_Idle(object sender, EventArgs e)
        {
            Mat a = cap.QueryFrame();
            if (a != null)
            {
                System.Threading.Thread.Sleep((int)(1000.0 / fps - 5));
                imageBox1.Image = a;
                GC.Collect();
            }
            Image<Bgr, byte> colorImage = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            //取得灰階影像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(colorImage.Bitmap);
            imageBox2.Image = grayImage;


            //二值化的閥值
            Gray thresholdValue = new Gray(55);
            //取得二值化影像
            Image<Gray, byte> thresholdImage = grayImage.ThresholdBinary(thresholdValue, new Gray(255));
            //thresholdImage.Save("threshold.bmp");
            imageBox3.Image = thresholdImage;

        }
    }
}
