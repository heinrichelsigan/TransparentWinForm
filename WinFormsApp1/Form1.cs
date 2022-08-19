using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        protected Image? winDeskImg = null;
        volatile bool locChangedOff = false;

        public Form1()
        {
            InitializeComponent();
        }


        public void SetTransBG()
        {
            if (winDeskImg == null)
                winDeskImg = GetDesktopImage();
             
            Graphics g = Graphics.FromImage(winDeskImg);
            Form f = this.FindForm();
            Image? bgImg = Crop(winDeskImg, DesktopBounds.Size.Width, DesktopBounds.Size.Height, f.DesktopBounds.Location.X + 8, f.DesktopBounds.Location.Y + 32);
            if (bgImg != null)
                this.BackgroundImage = bgImg;
        }

        public Image? Crop(Image image, int width, int height, int x, int y)
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


        public Image GetDesktopImage()
        {
            locChangedOff = true;
            this.WindowState = FormWindowState.Minimized;
            ScreenCapture sc = new ScreenCapture();
            Image winDeskImg = sc.CaptureScreen();
            this.WindowState = FormWindowState.Normal;
            locChangedOff = false;
            return winDeskImg;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (this.winDeskImg == null)
                SetTransBG();
        }

        private void OnResizeEnd(object sender, EventArgs e)
        {
            if (!locChangedOff)
                SetTransBG();
        }

        private void OnLocationChanged(object sender, EventArgs e)
        {
            if (!locChangedOff)
                SetTransBG();
        }
    }
}