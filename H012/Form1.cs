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
using Emgu.CV.OCR;

namespace emgucvocr01
{
    public partial class Form1 : Form
    {
        private Mat _frame;
        private Tesseract _ocr;
        VideoCapture cap = new VideoCapture();
               int fps;
        private void ProcessFrame(object sender, EventArgs e)
        {
       
            
        }
         public Form1()
        {
            InitializeComponent();
            _ocr = new Tesseract("", "eng", OcrEngineMode.TesseractLstmCombined);
            _ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

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

            if (a != null)
            {
                textBox1.Text = "";
                pictureBox1.Image = imageBox1.Image.Bitmap;
                Bgr drawColor = new Bgr(Color.Blue);
                try
                {
                    Image<Bgr, Byte> image = new Image<Bgr, byte>(new Bitmap(pictureBox1.Image));
                    Image<Gray, byte> gray = image.Convert<Gray, Byte>();
                    CvInvoke.GaussianBlur(gray, gray, new Size(3, 3), 1);
                    gray._EqualizeHist();//均衡化

                    using (gray)
                    {
                        _ocr.SetImage(gray);
                        _ocr.Recognize();
                        Tesseract.Character[] charactors = _ocr.GetCharacters();
                        foreach (Tesseract.Character c in charactors)
                        {
                            image.Draw(c.Region, drawColor, 1);
                        }

                        imageBox1.Image = image;

                        String text = _ocr.GetUTF8Text();
                        label1.Text = text;
                        for (int i = 0; i < charactors.Length; i++)
                        { this.textBox1.Text += charactors[i].Text; }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}
