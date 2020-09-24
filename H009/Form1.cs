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
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.UI;


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
        public static PointF[] PointToPointF(Point[] pf)
        {
            PointF[] aaa = new PointF[pf.Length];
            int num = 0;
            foreach (var point in pf)
            {
                aaa[num].X = (int)point.X;
                aaa[num++].Y = (int)point.Y;
            }
            return aaa;
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
            /*
            Image<Bgr, byte> colorImage = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            //取得灰階影像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(colorImage.Bitmap);
            imageBox2.Image = grayImage;


            //二值化的閥值
            Gray thresholdValue = new Gray(55);
            //取得二值化影像
            Image<Gray, byte> thresholdImage = grayImage.ThresholdBinary(thresholdValue, new Gray(255));
            //thresholdImage.Save("threshold.bmp");
            imageBox3.Image = thresholdImage;*/
            if (a!=null)
            {
                Image<Bgr, byte> colorImage = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            Image<Gray, byte> b = new Image<Gray, byte>(a.Width, a.Height);
            Image<Gray, byte> c = new Image<Gray, byte>(a.Width, a.Height);
            Image<Bgr, byte> d = new Image<Bgr, byte>(a.Width, a.Height);
            Image<Gray, byte> abc = new Image<Gray, byte>(a.Width, a.Height);
            CvInvoke.Canny(colorImage, b, 100, 60);
            VectorOfVectorOfPoint con = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(b, con, c, RetrType.Ccomp, ChainApproxMethod.ChainApproxSimple);
            Point[][] con1 = con.ToArrayOfArray();
            PointF[][] con2 = Array.ConvertAll<Point[], PointF[]>(con1, new Converter<Point[], PointF[]>(PointToPointF));
            for (int i = 0; i < con.Size; i++)
            {
                PointF[] hull = CvInvoke.ConvexHull(con2[i], true);
                for (int j = 0; j < hull.Length; j++)
                {
                    Point p1 = new Point((int)(hull[j].X + 0.5), (int)(hull[j].Y + 0.5));
                    Point p2;
                    if (j == hull.Length - 1)
                        p2 = new Point((int)(hull[0].X + 0.5), (int)(hull[0].Y + 0.5));
                    else
                        p2 = new Point((int)(hull[j + 1].X + 0.5), (int)(hull[j + 1].Y + 0.5));
                     CvInvoke.Circle(abc, p1, 3, new MCvScalar(0, 255, 255, 255), 6);
                     CvInvoke.Line(abc, p1, p2, new MCvScalar(255, 255, 0, 255), 3);
                }
            }
            for (int i = 0; i < con.Size; i++)
                CvInvoke.DrawContours(d, con, i, new MCvScalar(255, 0, 255, 255), 2);
            imageBox2.Image = d;
            imageBox3.Image = abc;
            }
            

            

            
        }
    }
}
