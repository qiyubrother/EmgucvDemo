using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Threading;

namespace emgucvDemo
{
    public partial class Form1 : Form
    {

        Emgu.CV.VideoCapture capt;
        Image frame;
        VideoWriter videowriter;//
        int VideoFps;//视频帧率

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            try
            {
                if (capt == null)
                    capt = new Emgu.CV.VideoCapture();
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
            if (capt != null)
            {
                Application.Idle += new EventHandler(GetFrame);
            }

            base.OnShown(e);
        }

        private void GetFrame(object sender, EventArgs e)
        {
            Image<Bgr, Byte> frame = capt.QueryFrame().ToImage<Bgr, Byte>();
            imageBox1.Image = frame;
            if (sw)
            {
                if (!System.IO.Directory.Exists(System.IO.Path.Combine(Application.StartupPath, "Images")))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Application.StartupPath, "Images"));
                    }
                    catch { }
                }
                var fn = System.IO.Path.Combine(Application.StartupPath, "Images", 
                    DateTime.Now.Hour.ToString().PadLeft(2, '0') 
                    + DateTime.Now.Minute.ToString().PadLeft(2, '0') 
                    + DateTime.Now.Second.ToString().PadLeft(2, '0') 
                    + DateTime.Now.Millisecond.ToString() + ".jpg");
                frame.Save(fn);
            }
        }
        bool sw = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sw = !sw;
        }
    }
}
