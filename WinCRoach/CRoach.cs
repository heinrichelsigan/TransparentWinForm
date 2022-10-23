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
    public partial class CRoach : Form
    {
        protected Image winDeskImg = null;
        volatile int speachCnt = 0;
        volatile bool locChangedOff = false;
        object spinLock = new object();
        DateTime lastCapture = DateTime.Now;
        DateTime lastSay = DateTime.Now;
        int scrX = -1;
        int scrY = -1;

        public CRoach()
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
                winDesktopImg = (Image)System.AppDomain.CurrentDomain.GetData("DesktopImage");
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

        public void SetRoachBG()
        {            
            winDeskImg = GetDesktopImage();
            Graphics g = Graphics.FromImage(winDeskImg);
            Form f = this.FindForm();
            Image bgImg = Crop(winDeskImg, DesktopBounds.Size.Width, DesktopBounds.Size.Height, f.DesktopBounds.Location.X, f.DesktopBounds.Location.Y);
            this.BackgroundImage = bgImg;
            this.pictureBoxRoach.Image = WinCRoach.Properties.Resources.CRoach;
            this.pictureBoxRoach.BackgroundImage = bgImg;
        }


        private void OnLoad(object sender, EventArgs e)
        {                        
            SetRoachBG();

            System.Timers.Timer tRoachMove = new System.Timers.Timer { Interval = 500 };
            tRoachMove.Elapsed += (s, en) =>
            {
                this.Invoke(new Action(() =>
                {
                    RoachMove();
                }));
                tRoachMove.Stop(); // Stop the timer(otherwise keeps on calling)
            };
            tRoachMove.Start();            
        }

        private void RoachMove()
        {
            while (true)
            {
                Form f = this.FindForm();
                scrX = this.DesktopLocation.X - 2;  // f.DesktopBounds.Location.X - 2;
                scrY = this.DesktopLocation.Y - 2; // f.DesktopBounds.Location.Y - 2;
                if (scrX < 0)
                    scrX = winDeskImg.Width - 48;
                if (scrY < 0)
                {
                    scrY = winDeskImg.Height - 48;
                    scrY = winDeskImg.Width - 48;
                }

                this.SetRoachBG();
                this.Location = new Point(scrX, scrY);
                this.SetDesktopLocation(scrX, scrY);                
                System.Threading.Thread.Sleep(750);
            }
        }

    }
}
