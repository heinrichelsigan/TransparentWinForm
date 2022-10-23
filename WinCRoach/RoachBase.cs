using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCRoach
{
    public partial class RoachBase : Form
    {
        protected Image winDeskImg = null;
        volatile int speachCnt = 0;
        volatile bool locChangedOff = false;
        object spinLock = new object();
        DateTime lastCapture = DateTime.Now;
        DateTime lastSay = DateTime.Now;
        CRoach cRoach;
        int scrX = -1;
        int scrY = -1;

        string[] setences = {"Twenty", "Atou Marriage Fourty", "close down", "last beat winner", "Thank you and enough",
            "I change with Jack", "Last but not least", "Hey Mister", "Hey misses"};

        string[] schnapserlm = { "Und Zwanzig", "Vierzig", "Danke und genug hab I", "I drah zua", "Habeas tibi",
            "Tausch gegen den Buam aus", "Letzter fertzter", "Na oida" };


        public RoachBase()
        {
            InitializeComponent();
        }

        public Image GetDesktopImage()
        {
            locChangedOff = true;
            spinLock = new object();

            Image winDesktopImg;
            lock (spinLock)
            {
                this.WindowState = FormWindowState.Minimized;
                ScreenCapture sc = new ScreenCapture();
                winDesktopImg = sc.CaptureScreen();
                lastCapture = DateTime.Now;
                locChangedOff = false;
            }
            return winDesktopImg;
        }

        public Image Crop(Image image, int width, int height, int x, int y)
        {
            try
            {
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                g.Dispose();

                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void PersistDesktopImage()
        {
            winDeskImg = GetDesktopImage();
            System.AppDomain.CurrentDomain.SetData("DesktopImage", winDeskImg);
        }

        public void RotateSay()
        {
            spinLock = new object();
            lock (spinLock)
            {
                if (++speachCnt >= setences.Length)
                    speachCnt = 0;
                lastSay = DateTime.Now;
            }
            SaySpeach.Say(setences[speachCnt]);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (this.winDeskImg == null)
            {
                PersistDesktopImage();
            }

            cRoach = new CRoach();
            cRoach.ShowDialog();
                        
            System.Timers.Timer tLoad0 = new System.Timers.Timer { Interval = 250 };
            tLoad0.Elapsed += (s, en) =>
            {
                this.Invoke(new Action(() =>
                {
                    PollDesktopImage();
                }));
                tLoad0.Stop(); // Stop the timer(otherwise keeps on calling)
            };
            tLoad0.Start();           
        }

        private void PollDesktopImage()
        {
            while(true)
            {
                PersistDesktopImage();
                RoachMove();                
                System.Threading.Thread.Sleep(1500);
            }
        }

        private void RoachMove()
        {
            Form f = cRoach.FindForm();
            scrX = f.DesktopBounds.Location.X - 2;
            scrY = f.DesktopBounds.Location.Y - 2;
            scrX = cRoach.DesktopLocation.X - 2;
            scrY = cRoach.DesktopLocation.Y - 2;
            if (scrX < 0)
                scrX = winDeskImg.Width - 48;
            if (scrY < 0)
                scrY = winDeskImg.Height - 48;

            cRoach.Location = new Point(scrX, scrY);
            cRoach.SetDesktopLocation(scrX, scrY);
            
            cRoach.SetRoachBG();
            cRoach.BringToFront();
            cRoach.Show();            
            // System.Threading.Thread.Sleep(333);
        }

    }
}
