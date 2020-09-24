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
using Emgu.CV.Cvb;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.UI;
//using Emgu.CV.VideoSurveillance;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        VideoCapture cap = new VideoCapture();
        int fps; 
        private static Emgu.CV.Cvb.CvBlobDetector blobDetector;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            blobDetector = new CvBlobDetector();
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
            // 讀取彩色影像
            Image<Bgr, byte> image01 = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            imageBox1.Image = image01;

            var image2 = image01.InRange(new Bgr(10, 1, 13), new Bgr(76, 144, 240));
            var image2_not = image2.Not();
            using (CvBlobs blobs = new CvBlobs())
            {
                blobDetector.Detect(image2, blobs);
                var image3 = image01.Copy();
                foreach (var pair in blobs)
                {
                    CvBlob b = pair.Value;
                    CvInvoke.Rectangle(image3, b.BoundingBox, new MCvScalar(255.255, 255, 0), 5);
                }
                imageBox1.Image = image01;
                imageBox2.Image = image2;
                imageBox3.Image = image3;
                imageBox4.Image = image2_not;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //開啟檔案
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP4文件|*.mp4|AVI文件|*.avi|RMVB文件|*.rmvb|WMV文件|*.wmv|MKV文件|*.mkv|所有文件|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.Idle += Application_Idle;
                cap = new VideoCapture(openFileDialog.FileName);
                fps = (int)cap.GetCaptureProperty(CapProp.Fps);
            }


           


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }
    }
}
