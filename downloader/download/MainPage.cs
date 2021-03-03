using AltoHttp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using YoutubeExtractor;


namespace download
{

    public partial class MainPage : Form
    {
        private Rectangle label1rect;
        private Rectangle button1rect;
        private Rectangle textrect;
        private Size msize;


        public MainPage()
        {

            InitializeComponent();
            
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            msize = this.Size;
            label1rect = new Rectangle(label1.Location.X,label1.Location.Y,label1.Width,label1.Height);
            textrect = new Rectangle(textBox1.Location.X,textBox1.Location.Y,textBox1.Width,textBox1.Height);
            button1rect = new Rectangle(Startbutton.Location.X,Startbutton.Location.Y,Startbutton.Width,Startbutton.Height);

            textBox1.ForeColor=Color.FromArgb(100, 0, 0, 0);
            //textBox1.BackColor = Color.FromArgb(10, 1, 0, 0);

            SetStyle(ControlStyles.SupportsTransparentBackColor |
                       ControlStyles.OptimizedDoubleBuffer |
                       ControlStyles.AllPaintingInWmPaint |
                       ControlStyles.ResizeRedraw |
                       ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;

        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog= MessageBox.Show("are you sure you want to exit.","Exit",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialog==DialogResult.No)
            {
                e.Cancel = true;
            }else if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void resize()
        {
            resizecontrol(label1rect, label1);
            resizecontrol(textrect, textBox1);
            resizecontrol(button1rect, Startbutton);
            

        }
        private void resizecontrol(Rectangle originalrect,Control control)
        {
            float xRatio = (float)(this.Width) / (float)(msize.Width);
            float yRatio = (float)(this.Height) /(float)( msize.Height);

            int newX = (int)(originalrect.X*xRatio);
            int newY = (int)(originalrect.Y * yRatio);

            int newW = (int)(originalrect.Width*xRatio);
            int newH = (int)(originalrect.Height*yRatio);

            control.Location = new Point(newX,newY);
            control.Size = new Size(newW, newH);
        }
        

        private void MainPage_Resize(object sender, EventArgs e)
        {
            resize();
        }
        HttpDownloader httpDownloader;

        private void Startbutton_Click(object sender, EventArgs e)
        {
            /*ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            httpDownloader = new HttpDownloader(textBox1.Text, $"{Application.StartupPath}\\{Path.GetFileName(textBox1.Text)}");
             httpDownloader.DownloadCompleted += httpDownloader_DownloadCompleted;
             httpDownloader.ProgressChanged += httpDownloader_progreschanged;
             httpDownloader.Start();*/
           download();

        }
        private void httpDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate{
                label2.Text = "finished !";
                label4.Text = "100 %";

            });
        }
        private void httpDownloader_progreschanged(object sender, AltoHttp.ProgressChangedEventArgs e)
        {
            progressBar1.Value = (int)e.Progress;
            label4.Text=$"{e.Progress.ToString("0.00")}%";
            label7.Text = string.Format("{0} MB/s",(e.SpeedInBytes/1024/1024).ToString("0.00"));
            label5.Text = string.Format("{0} MB/s", (httpDownloader.TotalBytesReceived / 1024 / 1024).ToString("0.00"));
            label2.Text = "Downloading...";
        }

        private void Pausebutton_Click(object sender, EventArgs e)
        {
            if (httpDownloader!=null)
                httpDownloader.Pause();

        }

        private void Resumebutton_Click(object sender, EventArgs e)
        {
            if (httpDownloader != null)
                httpDownloader.Resume();
        }
        void download()
        {
            IEnumerable<VideoInfo> videos = DownloadUrlResolver.GetDownloadUrls(textBox1.Text);
            VideoInfo vi = videos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == Convert.ToInt32(360));
            if (vi.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(vi);


            }
            var videodownload = new VideoDownloader(vi, @"C:\Users\hamma\OneDrive\Desktop\downloads"+vi.Title+vi.VideoExtension);
            videodownload.DownloadFinished += download_finished;
            videodownload.Execute();

        }
        void download_finished(object s,EventArgs e)
        {
            MessageBox.Show("done");

        }
    }
    
}
